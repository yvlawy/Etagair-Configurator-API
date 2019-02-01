using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    /// <summary>
    /// search entities definition.
    /// The main object of a search of entities.
    /// When executed, generate a result. can be executed several times.
    /// </summary>
    public class SearchEntity
    {
        public SearchEntity()
        {
            ListSearchEntitySrcFolder = new List<SearchEntitySrcFolder>();
        }

        /// <summary>
        /// Identify the object.
        /// Referenced by the results.
        /// </summary>
        public string Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// list of source folders.
        /// TODO: saver flag goInsideChilds!!
        /// SearchEntitySrcFolder
        /// </summary>
        public List<SearchEntitySrcFolder> ListSearchEntitySrcFolder { get; private set; }

        /// <summary>
        /// The root search properties object.
        /// can be one simple property or a boolean expression (OR/AND).
        /// </summary>
        public SearchPropBase SearchPropRoot { get; set; }
    

        /// <summary>
        /// Add a source folder to the search, and set the option: go inside folders childs or not.
        /// (only direct childs).
        /// </summary>
        /// <param name="searchEntitu"></param>
        /// <param name="sourceFolder"></param>
        /// <param name="goInsideFolderChilds"></param>
        /// <returns></returns>
        public bool AddSourceFolder(Folder folder, bool goInsideFolderChilds)
        {
            // check the folder, not yet added?
            if (ListSearchEntitySrcFolder.Exists(e=>e.FolderId.Equals(folder.Id)))
                return false;

            SearchEntitySrcFolder entry = new SearchEntitySrcFolder();
            entry.FolderId = folder.Id;
            entry.GoInsideChilds = goInsideFolderChilds;

            // add it to search object
            ListSearchEntitySrcFolder.Add(entry);
            return true;
        }
    }
}
