using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    public class FolderTempl : ObjectTemplBase
    {
        public FolderTempl()
        {
            Name = "NotSet";
            ListChildId = new Dictionary<string, ObjectTemplType>();
        }

        /// <summary>
        /// list of childs; can be folder, object or selectOjects.
        /// </summary>
        public IDictionary<string, ObjectTemplType> ListChildId { get; set; }

        public void AddChild(FolderTempl folderTempl)
        {
            if (folderTempl == null)
                return;
            if (folderTempl.Id == null)
                return;

            ListChildId.Add(folderTempl.Id, ObjectTemplType.FolderTempl);
        }

        public void AddChild(EntityTempl entityTempl)
        {
            if (entityTempl == null)
                return;
            if (entityTempl.Id == null)
                return;

            ListChildId.Add(entityTempl.Id, ObjectTemplType.EntityTempl);
        }

        public override string ToString()
        {
            if (Id == null)
                return "(null), Childs nb= " + ListChildId.Count;

            return Id + ", Childs nb= " + ListChildId.Count;
        }
    }
}
