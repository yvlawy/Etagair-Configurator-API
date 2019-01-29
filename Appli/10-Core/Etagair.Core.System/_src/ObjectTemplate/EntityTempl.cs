using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    public class EntityTempl : ObjectTemplBase
    {
        public EntityTempl()
        {
            PropertyRoot = new PropGroupTempl();
        }

        // InstanceExtensionOption: Sealed, Extendable/free.

        /// <summary>
        /// The root of properties
        /// </summary>
        public PropGroupTempl PropertyRoot { get; private set; }

        // add the property under the root properties
        public bool AddProperty(PropGroupTempl propertyParent, PropTemplBase property)
        {
            // no parent provided, its root
            if (propertyParent == null)
            {
                PropertyRoot.AddProperty(property);
                return true;
            }

            propertyParent.AddProperty(property);
            return true;
        }

    }
}
