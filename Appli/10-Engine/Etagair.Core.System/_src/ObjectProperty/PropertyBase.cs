using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    /// <summary>
    /// Base of all properties.
    /// Has a key and a value.
    /// </summary>
    public abstract class PropertyBase
    {
        /// <summary>
        /// The id property group parent.
        /// </summary>
        public string PropGroupParentId { get; set; }


        /// <summary>
        /// The key can be a string or a TextCode.
        /// </summary>
        public PropertyKeyBase Key { get; set; }

    }
}
