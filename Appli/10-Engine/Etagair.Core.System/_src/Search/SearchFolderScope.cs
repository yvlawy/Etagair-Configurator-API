using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    /// <summary>
    /// Search folder(s) scope.
    /// </summary>
    public enum SearchFolderScope
    {
        /// <summary>
        /// All folders, from the root and in all subfolders childs.
        /// </summary>
        All,

        /// <summary>
        /// Scan only the root folder. Doesn't go in subfolders.
        /// </summary>
        RootOnly,

        /// <summary>
        /// Scan defined folders.
        /// Add one by one, for each, defined to go or not inside subfolders.
        /// </summary>
        Defined,
    }
}
