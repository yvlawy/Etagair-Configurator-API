using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    public enum ObjectType
    {
        Folder, 
        Entity,
        FolderTempl,
        EntityTempl
    }

    public enum ObjectTemplType
    {
        FolderTempl,
        EntityTempl
    }

    /// <summary>
    /// 2 way to build an instance:
    /// from scratch (manual) or from a template.
    /// </summary>
    public enum BuildFrom
    {
        Scratch,
        Template 
    }

    /// <summary>
    /// Folder or entity,..
    /// </summary>
    public abstract class ObjectBase
    {
        public ObjectBase()
        {
            BuildFrom = BuildFrom.Scratch;
            BuildFromScratchFinishedAuto = true;
            BuildFinished = true;
        }

        public string Id { get; set; }

        public string ParentFolderId { get; set; }

        public BuildFrom BuildFrom { get; set; }
        public bool BuildFromScratchFinishedAuto { get; set; }
        public bool BuildFinished { get; set; }
    }
}
