using Etagair.Core.Reposit.Contract;
using Etagair.Core.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core
{
    /// <summary>
    /// EntitySearch, search property key text matching.
    /// (string or TextCode).
    /// </summary>
    public class EntitySearcherPropKeyText
    {
        IEtagairReposit _reposit;

        public EntitySearcherPropKeyText(IEtagairReposit reposit)
        {
            _reposit = reposit;
        }

        /// <summary>
        /// Process the final criterion: property key text is matching?
        /// </summary>
        /// <param name="propCritKeyTextWork"></param>
        /// <param name="entity"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool AnalyzeEntityOnCritKeyText(SearchPropCriterionKeyTextWork propCritKeyTextWork, Entity entity, SearchEntityResult result)
        {
            // scan all properties of the entity to match the criterion: key name
            AnalyzeEntityOnCritKeyText_PropGroup(entity.PropertyRoot, propCritKeyTextWork);
            // todo: rajouter un flag dans le work: Match: yes,no, NotSet

            // todo: si match=true, alors stocker l'entité (l'id) dans le result
            if(propCritKeyTextWork.PropertyMatch== PropertyMatch.Yes)
            {
                result.AddEntity(entity);
            }
            return true;
        }

        #region Private methods

        /// <summary>
        /// Analyze the entity group property, process childs for a keyText criterion.
        /// </summary>
        /// <param name="propertyGroup"></param>
        /// <param name="propCritKeyTextWork"></param>
        /// <returns></returns>
        private bool AnalyzeEntityOnCritKeyText_PropGroup(PropertyGroup  propertyGroup, SearchPropCriterionKeyTextWork propCritKeyTextWork)
        {
            // scan childs properties
            foreach(PropertyBase propertyBase in propertyGroup.ListProperty)
            {
                // is the prop child a group?
                PropertyGroup propertyGroupChild = propertyBase as PropertyGroup;
                if(propertyGroupChild!=null)
                {
                    // analyze childs,if a property match, stops the process
                    AnalyzeEntityOnCritKeyText_PropGroup(propertyGroupChild, propCritKeyTextWork);
                    // a property matching the criterion has been found!
                    if (propCritKeyTextWork.PropertyMatch == PropertyMatch.Yes)
                        return true;

                    // process the property child
                    continue;
                }

                // the property child is a final property
                Property propertyChild = propertyBase as Property;
                AnalyzeEntityOnCritKeyText_Prop(propertyChild, propCritKeyTextWork);
                // a property matching the criterion has been found!
                if (propCritKeyTextWork.PropertyMatch == PropertyMatch.Yes)
                    return true;
            }
            // no error
            return true;
        }


        /// <summary>
        /// match the property with the criterion
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="propCritKeyTextWork"></param>
        /// <returns></returns>
        private bool AnalyzeEntityOnCritKeyText_Prop(Property property, SearchPropCriterionKeyTextWork propCritKeyTextWork)
        {
            string keyText;

            // filter according to the key type: string, TextCode or All
            if (!FilterOnKeyType(property.Key, propCritKeyTextWork))
                // not selected
                return true;

            // the key can be a string or a textCode
            if (!GetKeyText(property.Key, out keyText))
                // error
                return false;

            //----case TextMatchExact
            if (propCritKeyTextWork.TextMatch == CritOptionTextMatch.TextMatchExact)
            {
                return AnalyzeTextMatchExact(propCritKeyTextWork, keyText);
            }

            //----case contains
            //if (propCritKeyTextWork.TextMatch == CritOptionTextMatch.TextMatchContains)
            //{
            //    // todo: +tard
            //    throw new Exception("AnalyzeEntityOnCritKeyText_Prop failure, typeMatch=TextMatchContains not yet implemented");
            //}

            //----case regex
            //if (propCritKeyTextWork.TextMatch == CritOptionTextMatch.TextMatchRegex)
            //{
            //    // todo: +tard
            //    throw new Exception("AnalyzeEntityOnCritKeyText_Prop failure, typeMatch=TextMatchRegex not yet implemented");
            //}

            // error
            throw new Exception("AnalyzeEntityOnCritKeyText_Prop failure, type match not yet implemented.");
        }

        /// <summary>
        /// filter according to the key type: string, TextCode or All
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        private bool FilterOnKeyType(PropertyKeyBase propertyKeyBase, SearchPropCriterionKeyTextWork propCritKeyTextWork)
        {
            // match all key type: string and TextCode
            if (propCritKeyTextWork.PropKeyTextType == CritOptionPropKeyTextType.AllKeyType)
                return true;

            // get the type of the prop key
            PropertyKeyString propKeyString = propertyKeyBase as PropertyKeyString;
            if (propKeyString != null)
            {
                if (propCritKeyTextWork.PropKeyTextType == CritOptionPropKeyTextType.KeyStringOnly)
                    return true;

                return false;
            }

            // the prop key type is TextCode
            if (propCritKeyTextWork.PropKeyTextType == CritOptionPropKeyTextType.KeyTextCodeOnly)
                return true;

            return false;
        }

        /// <summary>
        /// Analyze the CritOptionTextMatch = MatchExact.
        /// </summary>
        /// <param name="propCritKeyTextWork"></param>
        /// <param name="keyText"></param>
        /// <returns></returns>
        private bool AnalyzeTextMatchExact(SearchPropCriterionKeyTextWork propCritKeyTextWork, string keyText)
        {
            if (propCritKeyTextWork.TextSensitive == CritOptionTextSensitive.No)
            {
                if (propCritKeyTextWork.KeyText.Equals(keyText))
                    // ok, match
                    propCritKeyTextWork.PropertyMatch = PropertyMatch.Yes;
                else
                    propCritKeyTextWork.PropertyMatch = PropertyMatch.No;

                return true;
            }

            if (propCritKeyTextWork.KeyText.Equals(keyText, StringComparison.InvariantCultureIgnoreCase))
                // ok, match
                propCritKeyTextWork.PropertyMatch = PropertyMatch.Yes;
            else
                propCritKeyTextWork.PropertyMatch = PropertyMatch.No;

            return true;           
        }

        /// <summary>
        /// The property key text can be a string or a textCode.
        /// If its a textCode, go in the repository to read the textCode by the id.
        /// </summary>
        /// <param name="propCritKeyTextWork"></param>
        /// <param name="propertyKeyBase"></param>
        /// <returns></returns>
        private bool GetKeyText(PropertyKeyBase propertyKeyBase, out string keyText)
        {
            PropertyKeyString propKeyString = propertyKeyBase as PropertyKeyString;
            if (propKeyString != null)
            {
                keyText = propKeyString.Key;
                // ok
                return true;
            }

            keyText = "";
            PropertyKeyTextCode propKeyTextCode = propertyKeyBase as PropertyKeyTextCode;
            if (propKeyTextCode == null)
                // error!
                return false;

            // load the textCode
            TextCode textCode = _reposit.Finder.FindTextCodeById(propKeyTextCode.TextCodeId);
            if (textCode == null)
                // error!
                return false;

            keyText = textCode.Code;
            return true;
        }

        #endregion
    }
}
