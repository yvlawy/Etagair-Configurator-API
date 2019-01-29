using Etagair.Core.Reposit.Contract;
using Etagair.Core.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Etagair.Core.Reposit.InMemory
{
    public class Builder : IBuilder
    {
        SysData _sysData;

        public Builder(SysData sysData)
        {
            _sysData = sysData;
        }

        public bool SaveStructDescriptorRootFolderId(string id)
        {            
            _sysData.RootFolder = _sysData.ListFolder.Find(f => f.Id.Equals(id));
            return true;
        }

        public bool SaveCatalog(ICatalog catalog)
        {
            if (_sysData.Catalog != null)
                return false;

            catalog.Id = Guid.NewGuid().ToString();
            _sysData.Catalog = catalog;
            return true;
        }

        public bool UpdateCatalog(ICatalog catalog)
        {
            // nothing to do in memory
            return true;
        }

        //bool DeleteCatalog(ICatalog catalog);

        // todo: pas utile! on retrouve par les LanguageDef
        //public bool SaveMainLanguageCode(MainLanguageCode mainLangCode)
        //{
        //    if (_sysData.ListMainLanguageCode.Contains(mainLangCode))
        //        return false;

        //    _sysData.ListMainLanguageCode.Add(mainLangCode);
        //    return true;
        //}


        public bool SaveLanguageDef(LanguageDef lang)
        {
            if (_sysData.ListLanguageDef.Find(ld => ld.LanguageCode == lang.LanguageCode) != null)
                return false;

            lang.Id = Guid.NewGuid().ToString();
            _sysData.ListLanguageDef.Add(lang);
            return true;
        }

        public bool SaveCurrLanguageId(string languageId)
        {
            _sysData.CurrLanguageId = languageId;
            return true;
        }

        public bool SaveLanguage(Language lang)
        {
            if (_sysData.ListLanguage.Find(ld => ld.LanguageCode == lang.LanguageCode) != null)
                return false;

            lang.Id = Guid.NewGuid().ToString();
            _sysData.ListLanguage.Add(lang);
            return true;

        }

        public bool SaveTextCode(TextCode tc)
        {
            // check that the code is not already used
            // TODO:

            tc.Id = Guid.NewGuid().ToString();

            _sysData.ListTextCode.Add(tc);
            return true;
        }

        //public bool SaveTextLocalMain(TextLocalMain textLocalMain)
        //{
        //    textLocalMain.Id = Guid.NewGuid().ToString();

        //    _sysData.ListTextLocalMain.Add(textLocalMain);
        //    return true;
        //}


        public bool SaveTextLocal(TextLocalModel textLocal)
        {
            // check that the code is not already used
            // TODO:

            textLocal.Id = Guid.NewGuid().ToString();

            _sysData.ListTextLocal.Add(textLocal);
            return true;
        }

        //public bool SaveRootFolder(Folder folder)
        //{
        //    folder.Id = Guid.NewGuid().ToString();
        //    _sysData.RootFolder = folder;
        //    return true;
        //}

        public bool SaveFolder(Folder folder)
        {
            // check if the folder does not exists
            // TODO:

            folder.Id = Guid.NewGuid().ToString();
            _sysData.ListFolder.Add(folder);
            return true;
        }

        public bool UpdateFolder(Folder folder)
        {
            if (folder == null)
                return false;
            if (folder.Id == null)
                return false;

            // nothing more to do!
            return true;
        }

        public bool SaveEntity(Entity entity)
        {
            // must have a property root (a group)
            // TODO:

            entity.Id = Guid.NewGuid().ToString();

            // set an id for the group property
            entity.PropertyRoot.Id= Guid.NewGuid().ToString();

            _sysData.ListEntity.Add(entity);
            return true;
        }

        public bool UpdateEntity(Entity entity)
        {
            if (entity == null)
                return false;
            if (entity.Id == null)
                return false;

            // set all missing properties id 
            SetPropAllMissingId(entity.PropertyRoot);

            // nothing more to do!
            return true;
        }

        #region Template objects.

        public bool SaveEntityTempl(EntityTempl entityTempl)
        {
            entityTempl.Id = Guid.NewGuid().ToString();

            // set an id to the property root
            entityTempl.PropertyRoot.Id = Guid.NewGuid().ToString();

            // save it
            _sysData.ListEntityTempl.Add(entityTempl);
            return true;
        }

        public bool UpdateEntityTempl(EntityTempl entityTempl)
        {
            // check for new added inner object: missing id
            SetPropTemplAllMissingId(entityTempl.PropertyRoot);

            // nothing more to do!
            return true;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// check for new added inner object: missing id.
        /// property.
        /// </summary>
        /// <param name="propGroupTempl"></param>
        /// <returns></returns>
        private void SetPropAllMissingId(PropertyGroup propGroup)
        {
            if (propGroup.Id == null)
                propGroup.Id = Guid.NewGuid().ToString();

            // check for childs
            foreach (PropertyBase propBase in propGroup.ListProperty)
            {
                // the child is a group? 
                PropertyGroup propGroupChild = propBase as PropertyGroup;
                if (propGroupChild != null)
                    // yes, so set id on childs
                    SetPropAllMissingId(propGroupChild);
            }

        }

        /// <summary>
        /// check for new added inner object: missing id.
        /// property and rules.
        /// SetPropTemplAllMissingId
        /// </summary>
        /// <param name="propGroupTempl"></param>
        /// <returns></returns>
        private void SetPropTemplAllMissingId(PropGroupTempl propGroupTempl)
        {
            if(propGroupTempl.Id==null)
                propGroupTempl.Id = Guid.NewGuid().ToString();

            // check rules 
            SetRulesId(propGroupTempl);

            // check for childs
            foreach (PropTemplBase propTemplBase in propGroupTempl.ListProperty)
            {
                // check rules 
                SetRulesId(propTemplBase);

                // the child is a group? 
                PropGroupTempl propGroupTemplChild = propTemplBase as PropGroupTempl;
                if (propGroupTemplChild != null)
                    // yes, so set id on childs
                    SetPropTemplAllMissingId(propGroupTemplChild);
            }

        }

        /// <summary>
        /// create missing rules id of the property. 
        /// </summary>
        /// <param name="propTemplBase"></param>
        private void SetRulesId(PropTemplBase propTemplBase)
        {
            foreach(PropTemplRuleBase rule in propTemplBase.ListRule)
            {
                if(rule.Id==null)
                    rule.Id = Guid.NewGuid().ToString();
            }
        }

        #endregion
    }
}
