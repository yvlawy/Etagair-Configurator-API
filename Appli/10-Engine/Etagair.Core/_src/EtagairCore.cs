using Etagair.Core.Reposit.Contract;
using Etagair.Core.System;
using System;

namespace Etagair.Core
{
    /// <summary>
    /// The Etagair core API.
    /// Use an agnostic repository (not a concrete one), through an interface.
    /// </summary>
    public class EtagairCore
    {
        IEtagairReposit _reposit;

        public EtagairCore()
        {
            LastErrorMsg = "";
            _reposit = null;
            Searcher = null;
            Editor = null;
            EditorTempl = null;
            ProcessTempl = null;
        }

        /// <summary>
        /// The last error message occurs in the core.
        /// </summary>
        public string LastErrorMsg { get; private set; }

        /// <summary>
        /// To build final objects: folder, entity and selection.
        /// </summary>
        public Editor Editor { get; private set; }

        /// <summary>
        /// To build template objects: entity and folder.
        /// </summary>
        public EditorTempl EditorTempl { get; private set; }

        /// <summary>
        /// Process template object, build object: entities and folders from templates.
        /// </summary>
        public ProcessTempl ProcessTempl { get; private set; }

        /// <summary>
        /// To search and get objects (entity,...), by properties values.
        /// </summary>
        public Searcher Searcher { get; private set; }

        /// <summary>
        /// Initialize the core.
        /// Create the default and unique catalog in the repository.
        /// If it's already created, can't create a new one.
        /// </summary>
        /// <returns></returns>
        public bool Init(IEtagairReposit reposit)
        {
            try
            {
                _reposit = reposit;
                Searcher = new Searcher(reposit);
                Editor = new Editor(reposit, Searcher);
                EditorTempl = new EditorTempl(reposit);
                ProcessTempl = new ProcessTempl(reposit);

                if (!_reposit.IsCreated)
                {
                    return CreateDb();
                }

                return OpenDb();
            }catch(Exception e)
            {
                LastErrorMsg = e.ToString();
                return false;
            }
        }

        #region Privates methods

        private bool CreateDb()
        {
            // create the repository (with the structure descriptor)
            if (!_reposit.Create())
                return false;

            // create system data (initial data)
            CreateSystemData(_reposit);

            // create and save the default catalog
            ICatalog catalog = new Catalog();
            catalog.Name = CoreDef.DefaultCatalogName;
            _reposit.Builder.SaveCatalog(catalog);
            return true;
        }

        /// <summary>
        /// Open the existing default catalog in the repository
        /// </summary>
        /// <returns></returns>
        public bool OpenDb()
        {
            return _reposit.Open();
        }

        private bool CreateSystemData(IEtagairReposit reposit)
        {
            // create list of available languages
            CreateLanguageDef(MainLanguageCode.en, LanguageCode.en);
            CreateLanguageDef(MainLanguageCode.en, LanguageCode.en_GB);
            CreateLanguageDef(MainLanguageCode.en, LanguageCode.en_US);

            CreateLanguageDef(MainLanguageCode.fr, LanguageCode.fr);
            CreateLanguageDef(MainLanguageCode.fr, LanguageCode.fr_FR);

            // create the root folder
            Folder root = new Folder();
            root.Name = ObjectDef.RootFolderName;
            _reposit.Builder.SaveFolder(root);

            // save the root folder id
            _reposit.Builder.SaveStructDescriptorRootFolderId(root.Id);

            // TODO: others!!
            return true;
        }

        private void CreateLanguageDef(MainLanguageCode mainLanguageCode, LanguageCode languageCode)
        {
            LanguageDef lang = new LanguageDef();
            lang.MainLanguageCode = mainLanguageCode;
            lang.LanguageCode = languageCode;
            _reposit.Builder.SaveLanguageDef(lang);
        }
        #endregion

    }
}
