using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    /// <summary>
    /// represents a catalog.
    /// Just a descriptor.
    /// Does not contains the content.
    /// </summary>
    public class Catalog : ICatalog
    {
        public Catalog()
        {
        }

        public string Id { get; set; }

        public string Name { get; set; }


        // ?? todo: autres données
    }
}
