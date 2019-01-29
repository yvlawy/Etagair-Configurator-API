using Etagair.Core.Reposit.Contract;
using Etagair.Core.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.Reposit.LiteDB
{
    public class Finder : IFinder
    {
        SysData _sysData;

        public Finder(SysData sysData)
        {
            _sysData = sysData;
        }

        /// <summary>
        /// Return the unique catalog saved inthe repos.
        /// </summary>
        /// <returns></returns>
        public ICatalog GetCatalog()
        {
            return _sysData.DBEngine.Query<ICatalog>().FirstOrDefault();
        }

        /// <summary>
        /// Find the language in the list of managed languages.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public LanguageDef FindLanguageDefByCode(LanguageCode code)
        {
            return _sysData.DBEngine.Query<LanguageDef>()
                .Where(l => l.LanguageCode.Equals(code)).FirstOrDefault();
        }

        //public bool ExistsMainLanguageCodeByCode(MainLanguageCode mainLanguageCode)
        //{
        //    // use a class wrapper to save the enum.
        //    return (_sysData.DBEngine.Query<MainLanguageCodeClass>()
        //        .Where(l => l.MainLanguageCode ==mainLanguageCode).Count() >0);
        //}

        public string GetCurrLanguageId()
        {
            // get the root folder
            StructureDescriptor descriptor = _sysData.DBEngine.Query<StructureDescriptor>().FirstOrDefault();
            return descriptor.CurrLanguageId;

        }

        public Language FindLanguageById(string languageId)
        {
            return _sysData.DBEngine.Query<Language>()
                .Where(l => l.Id.Equals(languageId)).FirstOrDefault();
        }

        /// <summary>
        /// Find a managed language code.
        /// (means used in the catalog).
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Language FindLanguageByCode(LanguageCode code)
        {
            return _sysData.DBEngine.Query<Language>()
                .Where(l => l.LanguageCode.Equals(code)).FirstOrDefault();
        }

        public TextCode FindTextCodeById(string id)
        {
            return _sysData.DBEngine.Query<TextCode>()
                .Where(tc => tc.Id.Equals(id)).FirstOrDefault();
        }

        /// <summary>
        /// Find a textCode by the code.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public TextCode FindTextCodeByCode(string code)
        {
            return _sysData.DBEngine.Query<TextCode>()
                .Where(tc => tc.Code.Equals(code)).FirstOrDefault();
        }

        //public TextLocalMain FindTextLocalMain(MainLanguageCode mainLanguageCode, string textCodeId)
        //{
        //    return _sysData.DBEngine.Query<TextLocalMain>()
        //        .Where(l => l.MainLanguageCode == mainLanguageCode && l.TextCodeId== textCodeId).FirstOrDefault();
        //}

        public TextLocalModel FindTextLocal(LanguageCode languageCode, string textCodeId)
        {
            return _sysData.DBEngine.Query<TextLocalModel>()
                .Where(l => l.LanguageCode == languageCode && l.TextCodeId == textCodeId).FirstOrDefault();
        }

        public Folder GetRootFolder()
        {
            // get the root folder
            StructureDescriptor descriptor = _sysData.DBEngine.Query<StructureDescriptor>().FirstOrDefault();
            return FindFolderById(descriptor.RootFolderId);
        }

        public Folder FindFolderById(string folderId)
        {
            return _sysData.DBEngine.Query<Folder>().Where(f => f.Id.Equals(folderId)).FirstOrDefault();
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
            if (folderParent == null)
            {
                folderParent = GetRootFolder();
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
                Folder folder = _sysData.DBEngine.Query<Folder>().Where(f => f.Id == objBaseId && f.Name.Equals(name)).FirstOrDefault();
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
                Folder folder = _sysData.DBEngine.Query<Folder>().Where(f => f.Id == objBaseId).FirstOrDefault();
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
            return _sysData.DBEngine.Query<Entity>().Where(e => e.Id == entityId).FirstOrDefault();
        }
    }
}
