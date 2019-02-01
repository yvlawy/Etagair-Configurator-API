using Etagair.Core.Reposit.Contract;
using Etagair.Core.System;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.Reposit.LiteDB
{
    public class Builder : IBuilder
    {
        SysData _sysData;

        public Builder(SysData sysData)
        {
            _sysData = sysData;
        }

        #region Public methods

        public bool SaveStructDescriptorRootFolderId(string id)
        {
            try
            {
                // get the structore descriptor object
                StructureDescriptor descriptor = LoadStructureDescriptor();

                // modify and save the descriptor
                descriptor.RootFolderId = id;

                // save the descriptor
                UpdateStructureDescriptor(descriptor);

                return true;
            }
            catch
            {
                throw new Exception("Error, Can't save the RootFolder Id.");
                //return false;
            }
        }

        public bool SaveCurrLanguageId(string languageId)
        {
            try
            {
                // get the structore descriptor object
                StructureDescriptor descriptor = LoadStructureDescriptor();

                // modify and save the descriptor
                descriptor.CurrLanguageId = languageId;

                // save the descriptor
                UpdateStructureDescriptor(descriptor);
                return true;
            }
            catch
            {
                throw new Exception("Error, Can't save CurrLanguage Id.");
                //return false;
            }
        }

        public bool SaveCatalog(ICatalog catalog)
        {
            try
            {
                catalog.Id = ObjectId.NewObjectId().ToString();
                _sysData.DBEngine.Insert(catalog);
                return true;
            }
            catch
            {
                throw new Exception("Error, Can't save the Catalog.");
                //return false;
            }
        }

        public bool UpdateCatalog(ICatalog catalog)
        {
            try
            {
                if (!_sysData.DBEngine.Update(catalog))
                    return false;
                return true;
            }
            catch
            {
                //return false;
                throw new Exception("Error!");
            }
        }

        public bool SaveLanguageDef(LanguageDef langDef)
        {
            try
            {
                langDef.Id = ObjectId.NewObjectId().ToString();
                _sysData.DBEngine.Insert(langDef);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error, Can't save the LanguageDef, err= " + e.Message);
                //return false;
            }
        }

        // todo: pas utile! on retrouve par les LanguageDef
        //public bool SaveMainLanguageCode(MainLanguageCode mainLangCode)
        //{
        //    try
        //    {
        //        // need a class wrapper to save the enum
        //        var mainLanguageCodeClass = new MainLanguageCodeClass();
        //        mainLanguageCodeClass.Id = ObjectId.NewObjectId().ToString();
        //        mainLanguageCodeClass.MainLanguageCode = mainLangCode;
        //        _sysData.DBEngine.Insert(mainLanguageCodeClass);
        //        return true;
        //    }
        //    catch(Exception e)
        //    {
        //        throw new Exception("Error, Can't save the MainLanguageCode, err= " + e.Message);
        //        //return false;
        //    }
        //}


        public bool SaveLanguage(Language lang)
        {
            // check that the code is not already used
            // TODO:

            try
            {
                lang.Id = ObjectId.NewObjectId().ToString();
                _sysData.DBEngine.Insert(lang);
                return true;
            }
            catch
            {
                throw new Exception("Error, Can't save the Language.");
                //return false;
            }
        }


        public bool SaveTextCode(TextCode tc)
        {
            // check that the code is not already used
            // TODO:

            try
            {
                tc.Id = ObjectId.NewObjectId().ToString();
                _sysData.DBEngine.Insert(tc);
                return true;
            }
            catch
            {
                throw new Exception("Error, Can't save the TextCode.");
                //return false;
            }
        }

        //public bool SaveTextLocalMain(TextLocalMain textLocalMain)
        //{
        //    try
        //    {
        //        textLocalMain.Id = ObjectId.NewObjectId().ToString();
        //        _sysData.DBEngine.Insert<TextLocalMain>(textLocalMain);
        //        return true;
        //    }
        //    catch
        //    {
        //        throw new Exception("Error, Can't save the TextLocalMain.");
        //        //return false;
        //    }
        //}

        public bool SaveTextLocal(TextLocalModel textLocal)
        {
            try
            {
                textLocal.Id = ObjectId.NewObjectId().ToString();
                _sysData.DBEngine.Insert<TextLocalModel>(textLocal);
                return true;
            }
            catch
            {
                throw new Exception("Error, Can't save the TextLocal.");
                //return false;
            }
        }

        //public bool SaveRootFolder(Folder folder)
        //{
        //    try
        //    {
        //        folder.Id = ObjectId.NewObjectId().ToString();

        //        // modify and save the descriptor
        //        StructureDescriptor descriptor= _sysData.DBEngine.Query<StructureDescriptor>().FirstOrDefault();
        //        descriptor.RootFolderId = folder.Id;
        //        _sysData.DBEngine.Update<StructureDescriptor>(descriptor);

        //        // save the folder
        //        _sysData.DBEngine.Insert<Folder>(folder);
        //        return true;
        //    }
        //    catch
        //    {
        //        throw new Exception("Error, Can't save the RootFolder.");
        //        //return false;
        //    }

        //}

        public bool SaveFolder(Folder folder)
        {
            try
            {
                folder.Id = ObjectId.NewObjectId().ToString();

                // save the folder
                _sysData.DBEngine.Insert<Folder>(folder);
                return true;
            }
            catch
            {
                throw new Exception("Error, Can't save the Folder.");
                //return false;
            }
        }

        public bool UpdateFolder(Folder folder)
        {
            if (folder == null)
                return false;
            if (folder.Id == null)
                return false;
            try
            {
                _sysData.DBEngine.Update<Folder>(folder);
                return true;
            }
            catch
            {
                throw new Exception("Error, Can't update the Folder.");
            }
        }

        public bool SaveEntity(Entity entity)
        {
            try
            {
                // must have a property root (a group)
                // TODO:

                entity.Id = NewObjectId();

                // set an id for the group property
                entity.PropertyRoot.Id = NewObjectId();

                // save the folder
                _sysData.DBEngine.Insert<Entity>(entity);
                return true;
            }
            catch
            {
                throw new Exception("Error, Can't save the entity.");
                //return false;
            }
        }

        public bool UpdateEntity(Entity entity)
        {
            if (entity == null)
                return false;
            if (entity.Id == null)
                return false;
            try
            {
                // set all missing properties id 
                SetPropAllMissingId(entity.PropertyRoot);

                _sysData.DBEngine.Update<Entity>(entity);
                return true;
            }
            catch
            {
                throw new Exception("Error, Can't update the Entity.");
            }
        }
        #endregion

        #region Template objects.

        public bool SaveEntityTempl(EntityTempl entityTempl)
        {
            try
            {
                // must have a property root (a group)
                // TODO:

                entityTempl.Id = ObjectId.NewObjectId().ToString();

                // set an id for the group property
                entityTempl.PropertyRoot.Id = ObjectId.NewObjectId().ToString();

                // save the object
                _sysData.DBEngine.Insert<EntityTempl>(entityTempl);
                return true;
            }
            catch
            {
                throw new Exception("Error, Can't save the EntityTempl.");
                //return false;
            }

        }

        public bool UpdateEntityTempl(EntityTempl entityTempl)
        {
            if (entityTempl == null)
                return false;
            if (entityTempl.Id == null)
                return false;

            // check for new added inner object: missing id
            SetPropTemplAllMissingId(entityTempl.PropertyRoot);

            try
            {
                _sysData.DBEngine.Update<EntityTempl>(entityTempl);
                return true;
            }
            catch
            {
                throw new Exception("Error, Can't update the EntityTempl.");
            }
        }

        #endregion

        #region Private methods

        private string NewObjectId()
        {
            return ObjectId.NewObjectId().ToString();
        }

        /// <summary>
        /// check for new added inner object: missing id.
        /// property.
        /// </summary>
        /// <param name="propGroupTempl"></param>
        /// <returns></returns>
        private void SetPropAllMissingId(PropertyGroup propGroup)
        {
            if (propGroup.Id == null)
                propGroup.Id = NewObjectId();

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
        /// </summary>
        /// <param name="propGroupTempl"></param>
        /// <returns></returns>
        private void SetPropTemplAllMissingId(PropGroupTempl propGroupTempl)
        {
            if (propGroupTempl.Id == null)
                propGroupTempl.Id = NewObjectId();

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
            foreach (PropTemplRuleBase rule in propTemplBase.ListRule)
            {
                if (rule.Id == null)
                    rule.Id = NewObjectId();
            }
        }

        private StructureDescriptor LoadStructureDescriptor()
        {
            try
            {
                // get the structore descriptor object
                return _sysData.DBEngine.Query<StructureDescriptor>().FirstOrDefault();
            }
            catch
            {
                throw new Exception("Error, Can't load the StructureDescriptor.");
                //return false;
            }
        }

        private bool UpdateStructureDescriptor(StructureDescriptor structureDescriptor)
        {
            try {
                _sysData.DBEngine.Update<StructureDescriptor>(structureDescriptor);

                return true;
            }
            catch
            {
                throw new Exception("Error, Can't save the structureDescriptor.");
                //return false;
            }

            #endregion
        }
    }
}
