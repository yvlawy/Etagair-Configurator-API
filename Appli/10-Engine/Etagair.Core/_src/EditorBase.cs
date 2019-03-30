using Etagair.Core.Reposit.Contract;
using Etagair.Core.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core
{
    public abstract class EditorBase
    {
        protected IEtagairReposit _reposit;

        protected Searcher _searcher;

        public EditorBase(IEtagairReposit reposit, Searcher searcher)
        {
            _reposit = reposit;
            _searcher = searcher;
        }

        #region Public Language

        /// <summary>
        /// Define a language to use in the current catalog.
        /// Used to translate text, image, ...
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool DefineLanguage(LanguageCode code)
        {
            // find the system definition of the language, and the main language code
            LanguageDef languageDef = _reposit.Finder.FindLanguageDefByCode(code);
            if (languageDef == null)
                return false;

            // check if the language is not already defined for the catalog
            if (_reposit.Finder.FindLanguageByCode(code) != null)
                // already defined, nothing to do
                return false;

            // create a language defined for the catalog
            Language lang = new Language();
            lang.MainLanguageCode = languageDef.MainLanguageCode;
            lang.LanguageCode = code;

            // add the language in the list of used/managed language
            // TODO: save aussi le Main lang en meme temps?
            if (!_reposit.Builder.SaveLanguage(lang))
                return false;

            // not yet default current/default languageCode defined?
            string languageId = _reposit.Finder.GetCurrLanguageId();
            if (languageId == null)
            {
                // becomes the current language
                _reposit.Builder.SaveCurrLanguageId(lang.Id);
            }
            return true;
        }
        #endregion

        #region Public Create Text Methods.

        public TextCode CreateTextCode(string textCode)
        {
            return CreateTextCode(textCode, 0);
        }

        public TextCode CreateTextCode(string textCode, int paramsCount)
        {
            if (string.IsNullOrWhiteSpace(textCode))
                return null;

            // check the syntax of the code
            // TODO: définir regles!  pas de car spéciaux, ;;;

            // check if the textCode is not already used
            if (_reposit.Finder.FindTextCodeByCode(textCode) != null)
                return null;

            if (paramsCount < 0)
                paramsCount = 0;

            TextCode tc = new TextCode();
            tc.Code = textCode;
            tc.ParamsCount = paramsCount;
            _reposit.Builder.SaveTextCode(tc);
            return tc;
        }


        /// <summary>
        /// Create a localized text for a final language.
        /// exp: en_GB, en_US, fr_FR,...
        /// </summary>
        /// <param name="languageCode"></param>
        /// <param name="tc"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public TextLocalModel CreateTextLocalModel(LanguageCode languageCode, TextCode tc, string text)
        {
            // the language must exists
            Language lang = _reposit.Finder.FindLanguageByCode(languageCode);
            if (lang == null)
                // not defined
                return null;

            // check the textCode
            // TODO:

            // check the text
            if (text == null)
                return null;

            // check that not already exists: same languageCode and textCode
            if (_reposit.Finder.FindTextLocal(languageCode, tc.Id) != null)
                return null;

            // create the localize text
            TextLocalModel textLocal = new TextLocalModel();
            textLocal.TextCodeId = tc.Id;
            textLocal.LanguageCode = languageCode;
            textLocal.Text = text;

            // save it
            _reposit.Builder.SaveTextLocal(textLocal);
            return textLocal;
        }
        #endregion

        #region Public TextLocal generate/build methods.
        /// <summary>
        /// Return the TextLocal object corresponding to the textCode and the current/default languageCode.
        /// </summary>
        /// <param name="textCode"></param>
        /// <returns></returns>
        public TextLocal GenerateTextLocal(TextCode textCode)
        {
            if (textCode == null)
                return null;

            // get the current language
            Language lang = _searcher.GetCurrentLanguage();
            if (lang == null)
                return null;

            return GenerateTextLocal(textCode, lang, null);
        }

        /// <summary>
        /// Return the TextLocal object corresponding to the textCode and the current/default languageCode.
        /// </summary>
        /// <param name="textCode"></param>
        /// <returns></returns>
        public TextLocal GenerateTextLocal(TextCode textCode, List<string> listParams)
        {
            if (textCode == null)
                return null;

            // get the current language
            Language lang = _searcher.GetCurrentLanguage();
            if (lang == null)
                return null;

            return GenerateTextLocal(textCode, lang, listParams);
        }

        public TextLocal GenerateTextLocal(TextCode textCode, LanguageCode langCode)
        {
            return GenerateTextLocal(textCode, langCode, null);

        }

        public TextLocal GenerateTextLocal(TextCode textCode, LanguageCode langCode, List<string> listParams)
        {
            if (textCode == null)
                return null;

            // get the current language
            Language lang = _reposit.Finder.FindLanguageByCode(langCode);
            if (lang == null)
                return null;

            return GenerateTextLocal(textCode, lang, listParams);
        }

        public TextLocal GenerateTextLocal(TextCode textCode, Language lang, List<string> listParams)
        {
            TextLocal textLocal;
            if (textCode == null)
                return null;

            // a textLocal exists for this languageCode?
            TextLocalModel textLocalModel = _reposit.Finder.FindTextLocal(lang.LanguageCode, textCode.Id);
            if (textLocalModel != null)
            {
                textLocal = GenerateTextLocal(textCode, textLocalModel, listParams);
                return textLocal;
            }

            // no textLocal found, get the mainLanguageCode of the languageCode
            MainLanguageCode mainLanguageCode = lang.MainLanguageCode;

            // convert the mainLanguageCode to a languageCode
            LanguageCode languageCode = LanguageDef.ToLanguageCode(mainLanguageCode);

            // load the textLocal
            textLocalModel = _reposit.Finder.FindTextLocal(languageCode, textCode.Id);

            textLocal = GenerateTextLocal(textCode, textLocalModel, listParams);
            return textLocal;
        }

        #endregion

        #region Public Folder/entity methods.

        /// <summary>
        /// Return the root folder.
        /// Containing: entities, subfolders and template (entity and folder).
        /// </summary>
        /// <returns></returns>
        public Folder GetRootFolder()
        {
            return _reposit.Finder.GetRootFolder();
        }


        /// <summary>
        /// Create a folder, under a folder parent.
        /// (null for  the root folder)
        /// </summary>
        /// <param name="folderParent"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public Folder CreateFolder(Folder folderParent, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            // check the syntax
            // TODO: no special character,...

            // parent folder is null? its the root
            if (folderParent == null)
            {
                folderParent = _reposit.Finder.GetRootFolder();
            }
            else
            {
                // check that the name is not used by a folder in the folder parent scope
                if (_reposit.Finder.FindFolder(folderParent, name, false) != null)
                    return null;
            }

            // create a folder, attach it the parent
            Folder folder = new Folder();
            folder.Name = name;
            folder.ParentFolderId = folderParent.Id;

            // save it
            if (!_reposit.Builder.SaveFolder(folder))
                return null;

            folderParent.AddChild(folder);

            // update the parent foder, has a new child
            if (!_reposit.Builder.UpdateFolder(folderParent))
                return null;

            return folder;
        }

        /// <summary>
        /// Create an entity, under a folder parent.
        /// </summary>
        /// <param name="folderParent"></param>
        /// <returns></returns>
        public Entity CreateEntity(Folder folderParent)
        {
            // parent folder is null? its the root
            if (folderParent == null)
            {
                folderParent = _reposit.Finder.GetRootFolder();
            }

            // create an entity, attach it under the parent
            Entity entity = new Entity();
            entity.ParentFolderId = folderParent.Id;

            // set a key to the property root
            PropertyKeyString key = new PropertyKeyString();
            key.Key = CoreDef.DefaultPropertyRootName;
            entity.PropertyRoot.Key = key;

            // save it
            if (!_reposit.Builder.SaveEntity(entity))
                return null;

            folderParent.AddChild(entity);

            // update the parent foder, has a new child
            if (!_reposit.Builder.UpdateFolder(folderParent))
                return null;

            return entity;
        }

        #region Public Create property methods.


        /// <summary>
        /// low-level 2.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyParent"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Property CreateProperty(Entity entity, PropertyGroup propertyParent, string key, IValue value)
        {
            // check the entity parent
            if (entity == null)
                return null;

            if (propertyParent == null)
                propertyParent = entity.PropertyRoot;

            if (string.IsNullOrWhiteSpace(key))
                return null;

            // check the key, not used by an existing property
            if (_searcher.FindPropertyByKey(entity, propertyParent, key, false) != null)
                return null;

            PropertyKeyString propertyKey = new PropertyKeyString();
            propertyKey.Key = key;

            return CreateProperty(entity, propertyParent, propertyKey, value);
        }

        /// <summary>
        /// low-level 2.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyParent"></param>
        /// <param name="tcKey"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Property CreateProperty(Entity entity, PropertyGroup propertyParent, TextCode tcKey, IValue value)
        {
            if (tcKey == null)
                return null;

            // check the key, not used by an existing property
            if (_searcher.FindPropertyByKey(entity, propertyParent, tcKey.Code, false) != null)
                return null;

            // create the property key
            PropertyKeyTextCode propertyKey = new PropertyKeyTextCode();
            propertyKey.TextCodeId = tcKey.Id;

            return CreateProperty(entity, propertyParent, propertyKey, value);
        }

        /// <summary>
        /// low-level method.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyParent"></param>
        /// <param name="propKey"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Property CreateProperty(Entity entity, PropertyGroup propertyParent, PropertyKeyBase propKey, IValue value)
        {
            // check the entity parent
            if (entity == null)
                return null;

            if (propertyParent == null)
                propertyParent = entity.PropertyRoot;

            if (propKey == null)
                return null;

            // create the property, set the key and the value
            Property property = new Property();
            property.PropGroupParentId = propertyParent.Id;

            property.SetKeyValue(propKey, value);

            // add the property under the root properties
            entity.AddProperty(propertyParent, property);

            // save the entity modification
            if (!_reposit.Builder.UpdateEntity(entity))
                return null;

            return property;
        }

        public PropertyGroup CreatePropertyGroup(Entity entity, PropertyGroup propertyParent, string key)
        {
            // check the entity parent
            if (entity == null)
                return null;

            if (propertyParent == null)
                propertyParent = entity.PropertyRoot;

            if (string.IsNullOrWhiteSpace(key))
                return null;

            // check the key, not used by an existing property
            if (_searcher.FindPropertyByKey(entity, propertyParent, key, false) != null)
                return null;

            PropertyKeyString propertyKey = new PropertyKeyString();
            propertyKey.Key = key;
            // create the property, set the key and the value
            PropertyGroup property = new PropertyGroup();
            property.Key = propertyKey;
            property.PropGroupParentId = propertyParent.Id;

            // add the property under the root properties
            entity.AddProperty(propertyParent, property);

            // save the entity modification
            if (!_reposit.Builder.UpdateEntity(entity))
                return null;

            return property;
        }


        public PropertyGroup CreatePropertyGroup(Entity entity, PropertyGroup propertyParent, TextCode tcKey)
        {
            // check the entity parent
            if (entity == null)
                return null;

            if (propertyParent == null)
                propertyParent = entity.PropertyRoot;

            if (tcKey == null)
                return null;

            // check the key, not used by an existing property
            if (_searcher.FindPropertyByKey(entity, propertyParent, tcKey.Code, false) != null)
                return null;

            PropertyKeyTextCode propertyKey = new PropertyKeyTextCode();
            propertyKey.TextCodeId = tcKey.Id;

            // create the property, set the key and the value
            PropertyGroup property = new PropertyGroup();
            property.PropGroupParentId = propertyParent.Id;
            property.Key = propertyKey;

            // add the property under the root properties
            entity.AddProperty(propertyParent, property);

            // save the entity modification
            if (!_reposit.Builder.UpdateEntity(entity))
                return null;

            return property;
        }

        #endregion --Create property methods
        #endregion

        #region Protected methods.

        protected TextLocal GenerateTextLocal(TextCode textCode, TextLocalModel textLocalModel, List<string> listParams)
        {
            TextLocal textLocal = new TextLocal();
            textLocal.TextLocalModelId = textLocalModel.Id;

            // insert params
            if (listParams != null)
            {
                // check the params count
                // todo:
                try
                {
                    textLocal.Text = string.Format(textLocalModel.Text, listParams.ToArray());
                }
                catch
                {
                    // error!
                    textLocal.Text = textLocalModel.Text;
                }
            }
            else
                textLocal.Text = textLocalModel.Text;

            return textLocal;
        }


        #endregion

    }
}
