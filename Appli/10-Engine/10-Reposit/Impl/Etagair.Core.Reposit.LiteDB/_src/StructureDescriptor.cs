using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.Reposit.LiteDB
{
    /// <summary>
    /// A unique object: the structure descriptor.
    /// </summary>
    public class StructureDescriptor
    {
        public StructureDescriptor()
        {
            DTCreation = DateTime.Now;
        }

        public string Id { get; set; }

        public DateTime DTCreation { get; set; }

        public string RootFolderId { get; set; }

        /// <summary>
        /// The current/default language id, corresponding to a language code.
        /// used by the search to get localized media (textLocal,...)
        /// </summary>
        public string CurrLanguageId { get; set; }
    }
}
