using Etagair.Core.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.Reposit.InMemory
{
    public class SysData
    {
        public SysData()
        {
            //ListMainLanguageCode = new List<MainLanguageCode>();
            ListLanguageDef = new List<LanguageDef>();
            ListLanguage = new List<Language>();

            ListTextCode = new List<TextCode>();
            ListTextLocal = new List<TextLocalModel>();
            //ListTextLocalMain = new List<TextLocalMain>();

            ListFolder = new List<Folder>();
            ListEntity = new List<Entity>();

            ListFolderTempl = new List<FolderTempl>();
            ListEntityTempl = new List<EntityTempl>();

        }

        public ICatalog Catalog { get; set; }

        /// <summary>
        /// List of all available/existing languages.
        /// </summary>
        public List<LanguageDef> ListLanguageDef { get; private set; }

        /// <summary>
        /// The current language id.
        /// Corresponding to a languageCode.
        /// </summary>
        public string CurrLanguageId;

        /// <summary>
        /// List of main language used/defined in the catalog.
        /// </summary>
        //public List<MainLanguageCode> ListMainLanguageCode { get; private set; }


        /// <summary>
        /// List of managed language.
        /// </summary>
        public List<Language> ListLanguage { get; private set; }

        public List<TextCode> ListTextCode { get; private set; }

        public List<TextLocalModel> ListTextLocal { get; private set; }

        //public List<TextLocalMain> ListTextLocalMain { get; private set; }

        public Folder RootFolder { get; set; }

        public List<Folder> ListFolder { get; set; }
        public List<Entity> ListEntity { get; set; }

        public List<FolderTempl> ListFolderTempl { get; set; }
        public List<EntityTempl> ListEntityTempl { get; set; }

    }
}
