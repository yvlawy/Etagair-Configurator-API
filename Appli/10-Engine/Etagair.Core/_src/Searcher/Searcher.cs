using Etagair.Core.Reposit.Contract;
using Etagair.Core.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core
{
    /// <summary>
    /// To search objects (entity,...), by properties values.
    /// </summary>
    public class Searcher
    {
        IEtagairReposit _reposit;

        // search entities manager
        EntitySearcher _entitySearcher;

        //Language _currLang = null;
        //LanguageCode _currLanguageCode = LanguageCode.en;

        public Searcher(IEtagairReposit reposit)
        {
            _reposit = reposit;
            _entitySearcher = new EntitySearcher(reposit);
            ListSearchEntity = new List<SearchEntity>();
        }

        public bool SetCurrentLanguage(LanguageCode langCode)
        {
            // must be defined by the user
            Language language= _reposit.Finder.FindLanguageByCode(langCode);
            if(language == null)
                return false;

            // save it
            _reposit.Builder.SaveCurrLanguageId(language.Id);

            return true;
        }

        public Language GetCurrentLanguage()
        {
            string languageId = _reposit.Finder.GetCurrLanguageId();
            if (languageId == null)
                return null;

            // load the language object
            return _reposit.Finder.FindLanguageById(languageId);            
        }

            #region Public Find Text Methods

        public TextCode FindTextCodeById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            // load the textCode of the key, if exists
            return _reposit.Finder.FindTextCodeById(id);
        }

        ///// <summary>
        ///// Return the TextLocal object corresponding to the textCode and the current/default languageCode.
        ///// </summary>
        ///// <param name="textCode"></param>
        ///// <returns></returns>
        //public TextLocal GenerateTextLocal(TextCode textCode)
        //{
        //    if (textCode == null)
        //        return null;

        //    // get the current language
        //    Language lang = GetCurrentLanguage();
        //    if (lang == null)
        //        return null;

        //    return GenerateTextLocal(textCode, lang);
        //}

        //public TextLocal GenerateTextLocal(TextCode textCode, LanguageCode langCode)
        //{
        //    if (textCode == null)
        //        return null;

        //    // get the current language
        //    Language lang = _reposit.Finder.FindLanguageByCode(langCode);
        //    if (lang == null)
        //        return null;

        //    return GenerateTextLocal(textCode, lang);
        //}

        //public TextLocal GenerateTextLocal(TextCode textCode, Language lang)
        //{
        //    TextLocal textLocal;
        //    if (textCode == null)
        //        return null;

        //    // a textLocal exists for this languageCode?
        //    TextLocalModel textLocalModel = _reposit.Finder.FindTextLocal(lang.LanguageCode, textCode.Id);
        //    if (textLocalModel != null)
        //    {
        //        textLocal = new TextLocal();
        //        textLocal.TextLocalModelId = textLocalModel.Id;
        //        textLocal.Text = textLocalModel.Text;
        //        return textLocal;
        //    }

        //    // no textLocal found, get the mainLanguageCode of the languageCode
        //    MainLanguageCode mainLanguageCode = lang.MainLanguageCode;

        //    // convert the mainLanguageCode to a languageCode
        //    LanguageCode languageCode = LanguageDef.ToLanguageCode(mainLanguageCode);

        //    // load the textLocal
        //    textLocalModel= _reposit.Finder.FindTextLocal(languageCode, textCode.Id);
        //    textLocal = new TextLocal();
        //    textLocal.TextLocalModelId = textLocalModel.Id;
        //    textLocal.Text = textLocalModel.Text;
        //    return textLocal;

        //}

        #endregion

        #region Public Find methods

        public Folder FindFolderById(string folderId)
        {
            if (string.IsNullOrWhiteSpace(folderId))
                return null;

            return _reposit.Finder.FindFolderById(folderId);
        }

        public Entity FindEntityById(string entityId)
        {
            if (string.IsNullOrWhiteSpace(entityId))
                return null;

            return _reposit.Finder.FindEntityById(entityId);
        }

        #endregion

        #region Public find entity property methods
        /// <summary>
        /// Find a property by the name, from the root.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public PropertyBase FindPropertyByKey(Entity entity, string key, bool goInsideChilds)
        {
            return FindPropertyByKey(entity, entity.PropertyRoot, key, goInsideChilds);
        }

        /// <summary>
        /// Find a property by the key "raw" string (can be a textCode).
        /// From a property parent.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public PropertyBase FindPropertyByKey(Entity entity, PropertyGroup propertyParent, string key, bool goInsideChilds)
        {
            if (entity == null)
                return null;
            if (propertyParent == null)
                return null;
            if (string.IsNullOrWhiteSpace(key))
                return null;

            // load the textCode of the key, if exists
            TextCode tcKey = _reposit.Finder.FindTextCodeByCode(key);

            List<PropertyGroup> listPropGroup = new List<PropertyGroup>();

            // the key can be a string or a textCode
            foreach (var propertyBase in propertyParent.ListProperty)
            {
                // is it a final property?
                Property property = propertyBase as Property;
                if (property != null)
                {
                    // is the property key a textCode?
                    if (IsKeyMatchProperty(property, key, tcKey))
                        return property;

                    // next property
                    continue;
                }

                // is it a group property?
                PropertyGroup propertyGroup = propertyBase as PropertyGroup;
                if (propertyGroup != null)
                {
                    // is the property key a textCode?
                    if (IsKeyMatchProperty(propertyGroup, key, tcKey))
                        return propertyGroup;

                    // save the prop Group
                    listPropGroup.Add(propertyGroup);

                    // next property
                    continue;
                }
            }

            if (!goInsideChilds)
                // not found
                return null;

            // now scan props in childs
            foreach (PropertyGroup propertyGroup in listPropGroup)
            {
                PropertyBase propFound = FindPropertyByKey(entity, propertyGroup, key, goInsideChilds);
                if (propFound != null)
                    return propFound;
            }

            // not found
            return null;

        }

        /// <summary>
        /// Find all property by the key "raw" string (can be a textCode).
        /// From a property parent.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<PropertyBase> FindAllPropertyByKey(Entity entity, PropertyGroup propertyParent, string key, bool goInsideChilds)
        {
            if (entity == null)
                return null;
            if (propertyParent == null)
                return null;
            if (string.IsNullOrWhiteSpace(key))
                return null;

            List<PropertyBase> listPropFound = new List<PropertyBase>();

            // load the textCode of the key, if exists
            TextCode tcKey = _reposit.Finder.FindTextCodeByCode(key);

            List<PropertyGroup> listPropGroup = new List<PropertyGroup>();

            // the key can be a string or a textCode
            foreach (var propertyBase in propertyParent.ListProperty)
            {
                // is it a final property?
                Property property = propertyBase as Property;
                if (property != null)
                {
                    // is the property key a textCode?
                    if (IsKeyMatchProperty(property, key, tcKey))
                        listPropFound.Add(property);

                    // next property
                    continue;
                }

                // is it a group property?
                PropertyGroup propertyGroup = propertyBase as PropertyGroup;
                if (propertyGroup != null)
                {
                    // is the property key a textCode?
                    if (IsKeyMatchProperty(propertyGroup, key, tcKey))
                        // yes!
                        listPropFound.Add(propertyGroup);

                    // save the prop Group
                    listPropGroup.Add(propertyGroup);

                    // next property
                    continue;
                }
            }

            // don't go inside prop group childs
            if (!goInsideChilds)
                // return the list of property found, by the key
                return listPropFound;

            // now scan props in childs
            foreach (PropertyGroup propertyGroup in listPropGroup)
            {
                List<PropertyBase> listPropChildsFound = FindAllPropertyByKey(entity, propertyGroup, key, goInsideChilds);
                listPropFound.AddRange(listPropChildsFound);
            }

            // return the list of property found, by the key
            return listPropFound;
        }

        #endregion

        #region Public Create/Set/Define methods

        public List<SearchEntity> ListSearchEntity { get; set; }

        /// <summary>
        /// Create a new searchEntity object.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SearchEntity CreateSearchEntity(SearchFolderScope scope)
        {
            return CreateSearchEntity("search-" + Guid.NewGuid().ToString(), scope);
        }

        /// <summary>
        /// Create a new searchEntity object.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SearchEntity CreateSearchEntity(string name, SearchFolderScope scope)
        {
            // check the name
            if (string.IsNullOrWhiteSpace(name))
                return null;
            if(ListSearchEntity.Find(se =>se.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))!=null)
                return null;

            SearchEntity search = new SearchEntity();
            search.Name = name;
            search.SearchFolderScope = scope;

            // save the search object
            ListSearchEntity.Add(search);

            return search;
        }

        /// <summary>
        /// Add a source folder to the search, and set the option: go inside folders childs or not.
        /// (only direct childs).
        /// </summary>
        /// <param name="searchEntitu"></param>
        /// <param name="sourceFolder"></param>
        /// <param name="goInsideFolderChilds"></param>
        /// <returns></returns>
        public bool AddSourceFolder(SearchEntity searchEntity, Folder sourceFolder, bool goInsideFolderChilds)
        {
            // only if the scope is ok
            if (searchEntity.SearchFolderScope != SearchFolderScope.Defined)
                return false;

            // check the folder
            // TODO: notNull, pas déjà ajouté, lien entre dossier!

            // add it to search object
            return searchEntity.AddSourceFolder(sourceFolder, goInsideFolderChilds);
        }

        /// <summary>
        /// create a bool expr as the root expr of the search unit.
        /// </summary>
        /// <param name="searchEntityUnit"></param>
        /// <param name="searchPropParent"></param>
        /// <returns></returns>
        public SearchPropBoolExpr AddSearchPropBoolExpr(SearchEntity searchEntity, SearchBoolOperator oper)
        {
            if (searchEntity == null)
                return null;

            // check that the criterion does not already exists
            if (searchEntity.SearchPropRoot != null)
                // a criterion already exists!
                return null;

            SearchPropBoolExpr expr = new SearchPropBoolExpr();
            expr.Operator = oper;

            searchEntity.SearchPropRoot = expr;
            return expr;
        }

        /// <summary>
        /// create a boolean expression.
        /// </summary>
        /// <param name="searchEntityUnit"></param>
        /// <param name="searchPropParent"></param>
        /// <returns></returns>
        public SearchPropBoolExpr AddSearchPropBoolExpr(SearchEntity searchEntity, SearchPropBoolExpr boolExprParent, BoolExprOperand leftOrRight, SearchBoolOperator oper)
        {
            if (searchEntity == null)
                return null;
            if (boolExprParent == null)
                return null;

            // check that the criterion does not already exists
            if (searchEntity.SearchPropRoot != null)
                // a criterion already exists!
                return null;

            // check that the criterion does not already exists
            if (boolExprParent.GetLeftOrRight(leftOrRight) != null)
                return null;

            SearchPropBoolExpr expr = new SearchPropBoolExpr();
            expr.Operator = oper;

            // set the prop operand to the left or to the right
            boolExprParent.SetLeftOrRight(leftOrRight, expr);
            return expr;
        }

        /// <summary>
        /// add it as the root criterion of the unit.
        /// </summary>
        /// <param name="searchEntityUnit"></param>
        /// <param name="keyText"></param>
        /// <returns></returns>
        public SearchPropCriterionKeyText AddCritPropKeyText(SearchEntity searchEntity, string keyText)
        {
            if (searchEntity == null)
                return null;

            // check the text
            if (string.IsNullOrWhiteSpace(keyText))
                return null;

            // check that a root criterion does not already exists
            if (searchEntity.SearchPropRoot != null)
                // a criterion already exists!
                return null;

            SearchPropCriterionKeyText criterionPropKeyText = CreateSearchPropCriterionPropKeyText(keyText);
            searchEntity.SearchPropRoot = criterionPropKeyText;
            return criterionPropKeyText;
        }

        /// <summary>
        /// Add a property criterion to a search unit.
        /// set to the left or to the right operand of the expression.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="keyText"></param>
        /// <returns></returns>
        public SearchPropCriterionKeyText AddCritPropKeyText(SearchEntity searchEntity, SearchPropBoolExpr boolExprParent, BoolExprOperand leftOrRight, string keyText)
        {
            if (searchEntity == null)
                return null;
            if (boolExprParent == null)
                return null;

            // check the text
            if (string.IsNullOrWhiteSpace(keyText))
                return null;

            // check that the criterion does not already exists
            if (boolExprParent.GetLeftOrRight(leftOrRight) != null)
                return null;

            SearchPropCriterionKeyText criterionPropKeyText = CreateSearchPropCriterionPropKeyText(keyText);

            // set the prop operand to the left or to the right
            boolExprParent.SetLeftOrRight(leftOrRight, criterionPropKeyText);
            return criterionPropKeyText;
        }
        #endregion

        #region Public exec methods

        /// <summary>
        /// Execute the search entity definition.
        /// </summary>
        public SearchEntityResult ExecSearchEntity(SearchEntity searchEntity)
        {
            return _entitySearcher.Process(searchEntity);
        }

        #endregion

        #region Private methods

        // is the property key a textCode?
        private bool IsKeyMatchProperty(PropertyBase property, string key, TextCode tcKey)
        {
            PropertyKeyTextCode keyTextCode = property.Key as PropertyKeyTextCode;
            if (keyTextCode != null)
            {
                if (tcKey != null)
                {
                    // the key to find is a TextCode
                    if (keyTextCode.TextCodeId.Equals(tcKey.Id))
                        return true;
                }
                else
                {
                    // the property key is a textCode, load it to get the code, because the key is a string
                    TextCode tcPropKey = _reposit.Finder.FindTextCodeById(keyTextCode.TextCodeId);
                    if (tcPropKey.Code.Equals(key))
                        return true;
                }
            }

            // is the property key a string?
            PropertyKeyString keyString = property.Key as PropertyKeyString;
            if (keyString != null)
            {
                // compare strings
                if (keyString.Key.Equals(key))
                    return true;
            }

            // the property doesn't match the key (string or TextCode) 
            return false;
        }


        private SearchPropCriterionKeyText CreateSearchPropCriterionPropKeyText(string keyText)
        {
            SearchPropCriterionKeyText criterionPropKeyText = new SearchPropCriterionKeyText();
            criterionPropKeyText.KeyText = keyText;
            criterionPropKeyText.PropKeyTextType = CritOptionPropKeyTextType.AllKeyType;

            // key can be a string or textCode
            criterionPropKeyText.PropKeyType = CritOptionPropKeyType.All;

            criterionPropKeyText.TextMatch = CritOptionTextMatch.TextMatchExact;
            criterionPropKeyText.TextSensitive = CritOptionTextSensitive.Yes;

            // go (or not) inside prop group childs if exists
            // TODO: FindFirst, FindAll
            criterionPropKeyText.PropChildsScan = CritOptionPropChildsScan.GoInsidePropGroupChilds;

            return criterionPropKeyText;
        }
        #endregion
    }
}
