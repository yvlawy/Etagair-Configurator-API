using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    /// <summary>
    /// A folder, having objects and folders childs.
    /// </summary>
    public class Folder : ObjectBase
    {
        public Folder()
        {
            Name = "NotSet";
            ListChildId = new Dictionary<string, ObjectType>();
        }

        public string Name { get; set; }


        /// <summary>
        /// list of childs; can be fodler, object or selectOjects.
        /// </summary>
        public IDictionary<string, ObjectType> ListChildId { get; set; }

        public void AddChild(FolderTempl folderTempl)
        {
            if (folderTempl == null)
                return;
            if (folderTempl.Id == null)
                return;

            ListChildId.Add(folderTempl.Id, ObjectType.FolderTempl);
        }

        public void AddChild(EntityTempl entityTempl)
        {
            if (entityTempl == null)
                return;
            if (entityTempl.Id == null)
                return;

            ListChildId.Add(entityTempl.Id, ObjectType.EntityTempl);
        }

        public void AddChild(Folder folder)
        {
            if (folder == null)
                return;
            if (folder.Id == null)
                return;

            ListChildId.Add(folder.Id, ObjectType.Folder);
        }

        public void AddChild(Entity entity)
        {
            if (entity == null)
                return;
            if (entity.Id == null)
                return;

            ListChildId.Add(entity.Id, ObjectType.Entity);
        }

        public override string ToString()
        {
            if(Id==null)
                return "(null), Childs nb= " + ListChildId.Count;

            return Id + ", Childs nb= " + ListChildId.Count;
        }
    }
}
