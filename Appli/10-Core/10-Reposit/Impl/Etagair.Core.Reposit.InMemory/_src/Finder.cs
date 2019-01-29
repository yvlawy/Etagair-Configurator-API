using Etagair.Core.Reposit.Contract;
using Etagair.Core.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.Reposit.InMemory
{
    public class Finder : IFinder
    {
        SysData _sysData;

        public Finder(SysData sysData)
        {
            _sysData= sysData;
        }

        public ICatalog GetCatalog()
        {
            return _sysData.Catalog;
        }

        public LanguageDef FindLanguageDefByCode(LanguageCode code)
        {
            return _sysData.ListLanguageDef.Find(ld => ld.LanguageCode == code);
        }

        //public bool ExistsMainLanguageCodeByCode(MainLanguageCode mainLanguageCode)
        //{
        //    return _sysData.ListMainLanguageCode.Contains(mainLanguageCode);
        //}

        public string GetCurrLanguageId()
        {
            return _sysData.CurrLanguageId;
        }

        public Language FindLanguageById(string languageId)
        {
            return _sysData.ListLanguage.Find(ld => ld.Id == languageId);
        }

        public Language FindLanguageByCode(LanguageCode code)
        {
            return _sysData.ListLanguage.Find(ld => ld.LanguageCode == code);
        }

        public TextCode FindTextCodeById(string id)
        {
            return _sysData.ListTextCode.Find(tc => tc.Id.Equals(id));
        }

        /// <summary>
        /// Find a textCode by the code.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public TextCode FindTextCodeByCode(string code)
        {
            return _sysData.ListTextCode.Find(tc => tc.Code.Equals(code));
        }

        //public TextLocalMain FindTextLocalMain(MainLanguageCode mainLanguageCode, string textCodeId)
        //{
        //    return _sysData.ListTextLocalMain.Find(ld => ld.MainLanguageCode== mainLanguageCode && ld.TextCodeId== textCodeId);
        //}

        public TextLocalModel FindTextLocal(LanguageCode languageCode, string textCodeId)
        {
            return _sysData.ListTextLocal.Find(ld => ld.LanguageCode == languageCode && ld.TextCodeId == textCodeId);
        }

        public Folder GetRootFolder()
        {
            return _sysData.RootFolder;
        }

        public Folder FindFolderById(string folderId)
        {
            return _sysData.ListFolder.Find(f => f.Id.Equals(folderId));
        }

        /// <summary>
        /// Find a folder by the name, under the parent and 
        /// recursivly in childs if inChilds is true.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="inChilds"></param>
        /// <returns></returns>
        public Folder FindFolder(Folder folderParent, string name, bool inChilds)
        {
            // get the root folder
            if(folderParent==null)
            {
                folderParent = _sysData.RootFolder;
            }

            // scan objects of the current folder
            foreach (var entry in folderParent.ListChildId)
            {
                string objBaseId = entry.Key;

                // is it a folder?
                if (entry.Value != ObjectType.Folder)
                    // not a folder
                    continue;

                // load the folder object
                Folder folder = _sysData.ListFolder.Find(f => f.Id==objBaseId && f.Name.Equals(name));
                if (folder != null)
                    // ok, found, bye
                    return folder;
            }

            if (!inChilds)
                // not found, and serach only in direct childs
                return null;

            // not found in direct childs, try in folders childs
            foreach (var entry in folderParent.ListChildId)
            {
                string objBaseId = entry.Key;

                // is it a folder?
                if (entry.Value != ObjectType.Folder)
                    // not a folder
                    continue;

                // get it if its a folder
                Folder folder = _sysData.ListFolder.Find(f => f.Id == objBaseId);
                if (folder != null)
                {
                    // go inside this folder child
                    Folder folderFound = FindFolder(folder, name, true);
                    if (folderFound != null)
                        return folderFound;
                }
            }

            // not found
            return null;
        }

        public Entity FindEntityById(string entityId)
        {
            // get it if its a folder
            Entity entity = _sysData.ListEntity.Find(e => e.Id == entityId);
            return entity;
        }


        #region Privates methods
        #endregion
    }
}
