using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    public class PropGroupTempl : PropTemplBase
    {
        public PropGroupTempl()
        {
            ListProperty = new List<PropTemplBase>();
        }

        /// <summary>
        /// Needed for building instance.
        /// Used to find property child.
        /// </summary>
        public string Id { get; set; }

        // list of properties childs
        public List<PropTemplBase> ListProperty { get; set; }

        // Rules instanciation of childs
        // TODO:

        public void AddProperty(PropTemplBase property)
        {
            ListProperty.Add(property);
        }

    }
}
