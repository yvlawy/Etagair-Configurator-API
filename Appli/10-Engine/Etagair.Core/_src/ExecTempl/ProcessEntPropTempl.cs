using Etagair.Core.Reposit.Contract;
using Etagair.Core.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core
{
    /// <summary>
    /// Process the entity properties from the template.
    /// </summary>
    public class ProcessEntPropTempl
    {
        IEtagairReposit _reposit;

        /// <summary>
        /// Process entity propTempl rules. (on group and on final).
        /// </summary>
        ProcessEntPropTemplRules _processEntPropTemplRules;

        public ProcessEntPropTempl(IEtagairReposit reposit)
        {
            _reposit = reposit;
            _processEntPropTemplRules = new ProcessEntPropTemplRules(_reposit);
        }

        #region Public methods

        /// <summary>
        /// Set the prop root to an entity, from a template.
        /// the key is a string (like the root key from the template). 
        /// </summary>
        /// <param name="entityTempl"></param>
        /// <param name="entity"></param>
        public void SetEntPropertyRootFromTempl(EntityTempl entityTempl, Entity entity)
        {
            // TODO: always a string?
            PropKeyTemplString propKeyTemplString = entityTempl.PropertyRoot.Key as PropKeyTemplString;

            // set a key to the property root
            PropertyKeyString key = new PropertyKeyString();
            key.Key = propKeyTemplString.Key; // CoreDef.DefaultPropertyRootName;
            entity.PropertyRoot.Key = key;
            entity.PropertyRoot.PropGroupTemplId = entityTempl.PropertyRoot.Id;
        }

        /// <summary>
        /// Instantiate all properties childs of the group, from the template.
        /// </summary>
        /// <param name="entityTempl"></param>
        /// <param name="propToProcess"></param>
        /// <param name="entity"></param>
        /// <param name="property"></param>
        /// <param name="listRuleWithoutAction"></param>
        /// <returns></returns>
        public bool CreateAllPropGroupChildsFromTempl(EntityTempl entityTempl, PropGroupTempl propGroupTempl, Entity entity, PropertyGroup propGroupParent, List<PropTemplRuleActionBase> listAction, out List<PropTemplRuleBase> listRuleWithoutAction)
        {
            // get the rules of the group properties
            // propGroupTemplToProcess.ListRule
            // TODO: strategie??  OneOf, Several,...

            listRuleWithoutAction = new List<PropTemplRuleBase>();

            List<PropGroupTempl> listPropGroupTemplChilds = new List<PropGroupTempl>();

            // scan property childs of the template
            foreach (PropTemplBase propTemplBase in propGroupTempl.ListProperty)
            {
                // is it a group templ?
                PropGroupTempl propGroupTemplChild = propTemplBase as PropGroupTempl;
                if (propGroupTemplChild != null)
                {
                    // saved, to be process after all direct finals properties
                    listPropGroupTemplChilds.Add(propGroupTemplChild);
                    continue;
                }

                // is it propTempl (final)?
                PropTempl propTemplChild = propTemplBase as PropTempl;
                if (propTemplChild != null)
                {
                    CreatePropFromTempl(propTemplChild, propGroupParent, listAction, listRuleWithoutAction);
                }
            }

            // there are rules without action, need external actions, stops here
            if (listRuleWithoutAction.Count > 0)
                return true;

            // now process all prop group templ childs
            foreach (PropGroupTempl propGroupTemplChild in listPropGroupTemplChilds)
            {
                PropertyGroup propGroupChild;

                // create first the propGroup from the template, is empty (no child)
                CreatePropGroupFromTempl(propGroupTemplChild, propGroupParent, listAction, listRuleWithoutAction, out propGroupChild);

                // save/update the entity, will create missing id
                _reposit.Builder.UpdateEntity(entity);

                // then create the prop childs of the group, from the template
                List<PropTemplRuleBase> listRuleWithoutActionChilds;
                CreateAllPropGroupChildsFromTempl(entityTempl, propGroupTemplChild, entity, propGroupChild, listAction, out listRuleWithoutActionChilds);
                listRuleWithoutAction.AddRange(listRuleWithoutActionChilds);
            }

            return true;
        }
        #endregion

        #region Public Consistency methods

        /// <summary>
        /// Check the consistency of the template: check properties.
        /// Return the number of problems.
        /// </summary>
        /// <param name="entityTempl"></param>
        /// <returns></returns>
        public int CheckConsistencyEntityTempl(EntityTempl entityTempl, List<PropTemplRuleBase> listRuleToExec)
        {
            // check on the entity level
            // TODO:

            // check the properties
            return CheckConsistencyPropTempl(entityTempl, entityTempl.PropertyRoot, listRuleToExec);
        }

        #endregion

        #region Private Consistency methods

        /// <summary>
        /// Check the consistency of the property template.
        /// Return the number of problems.
        /// cases: 
        ///   -key prop must exists (not null),
        ///   -a null prop value -> a rule PropValueSetOnInstance must exists.
        /// </summary>
        /// <param name="entityTempl"></param>
        /// <param name="propTemplBase"></param>
        /// <returns></returns>
        private int CheckConsistencyPropTempl(EntityTempl entityTempl, PropTemplBase propTemplBase, List<PropTemplRuleBase> listRuleToExec)
        {
            int pbCount = 0;

            // is the property a group?
            PropGroupTempl propGroupTempl = propTemplBase as PropGroupTempl;
            if (propGroupTempl != null)
            {
                // get rules
                // TODO:  strategie instancier child: Single, Several,...

                // check properties childs
                foreach (PropTemplBase propTemplChild in propGroupTempl.ListProperty)
                {
                    pbCount += CheckConsistencyPropTempl(entityTempl, propTemplChild, listRuleToExec);
                }
                return pbCount;
            }

            // is the property a group?
            PropTempl propTempl = propTemplBase as PropTempl;
            if (propTempl != null)
            {
                // check the key
                if (propTempl.Key == null)
                    // the key must exists!
                    pbCount++;

                // check the prop value is null
                if (propTempl.Value.IsNull)
                {
                    // a rule must exists
                    PropTemplRuleBase rule = propTempl.ListRule.Find(r => r.Type == PropTemplRuleType.PropValueSetOnInstance);
                    if (rule == null)
                        // no rule found!
                        pbCount++;
                    else
                        listRuleToExec.Add(rule);
                }
                return pbCount;
            }

            throw new Exception("PropTempl type not implemented!");
        }


        #endregion

        #region Privates methods

        /// <summary>
        /// Create a property Template (final) from the template.
        /// process:
        ///   1- has no rule -> create the key and the value by copy from the template
        ///   2- has rule(s), scan rules:
        ///      2.1- is PropValueSetOnInstance
        ///         2.1.1- Create the key by copy from the template
        ///         2.1.2- has no action -> exit, need action
        ///         2.1.2- has action
        ///             2.1.2.1- Create the value by provided in the action
        /// </summary>
        /// <param name="propTempl"></param>
        /// <param name="propGroupParent"></param>
        /// <param name="listAction"></param>
        /// <param name="listRulesNeedActions"></param>
        /// <returns></returns>
        private bool CreatePropFromTempl(PropTempl propTempl, PropertyGroup propGroupParent, List<PropTemplRuleActionBase> listAction, List<PropTemplRuleBase> listRulesNeedActions)
        {
            // 1- has no rule -> create the key and the value by copy from the template
            if (propTempl.ListRule.Count == 0)
            {
                // create the key by copy from the template
                PropertyKeyBase propKey = _processEntPropTemplRules.CreatePropKeyFromTempl(propTempl);

                // create the value by copy from the template
                PropertyValueBase propValue = _processEntPropTemplRules.CreatePropValueFromTempl(propTempl.Value);

                Property property = new Property();
                property.PropGroupParentId = propGroupParent.Id;
                property.SetKeyValue(propKey, propValue);
                propGroupParent.AddProperty(property);
                return true;
            }

            // 2- has rule(s), scan each one
            return _processEntPropTemplRules.ExecRules(propTempl, propGroupParent, listAction, listRulesNeedActions);
        }

        /// <summary>
        /// create the propGroup from the template, is empty (no child).
        /// Childs will be created after that.
        /// </summary>
        /// <param name="propTempl"></param>
        /// <param name="propGroupParent"></param>
        /// <returns></returns>
        private bool CreatePropGroupFromTempl(PropGroupTempl propGroupTempl, PropertyGroup propGroupParent, List<PropTemplRuleActionBase> listAction, List<PropTemplRuleBase> listRulesNeedActions, out PropertyGroup propGroupChild)
        {
            // create the propGroup based on the template, add it under the propGroupParent
            propGroupChild = new PropertyGroup();
            propGroupChild.PropGroupTemplId = propGroupTempl.Id;
            propGroupChild.PropGroupParentId = propGroupParent.Id;

            // create the key by copy from the template
            PropertyKeyBase propKey = _processEntPropTemplRules.CreatePropKeyFromTempl(propGroupTempl);
            propGroupChild.Key = propKey;

            // save it (will create id)
            // TODO: besoin?
            //_reposit.Builder.UpdateEntity();

            // add it under the propGroupParent
            propGroupParent.AddProperty(propGroupChild);

            return true;

        }

        #endregion
    }
}
