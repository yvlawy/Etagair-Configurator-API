using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    /// <summary>
    /// Base of template objects.
    /// TODO: heriter de ObjectBase?
    /// </summary>
    public abstract class ObjectTemplBase 
    {
        public string Id { get; set; }

        /// <summary>
        /// All template objects have a name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Parent template folder Id.
        /// The root template object has a folder as parent.
        /// For inside object in complex template, the parent is a FolderTempl.
        /// </summary>
        public string ParentFolderId { get; set; }
    }
}
