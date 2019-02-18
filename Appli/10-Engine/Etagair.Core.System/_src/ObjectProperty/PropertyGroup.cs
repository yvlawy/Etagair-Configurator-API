using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    /// <summary>
    /// A property group, like a folder, containing properties childs (finals or group).
    /// Has a key like a final property.
    /// </summary>
    public class PropertyGroup : PropertyBase
    {
        public PropertyGroup()
        {
            ListProperty = new List<PropertyBase>();
        }

        /// <summary>
        /// Needed for building instance from template.
        /// Used to find property child.
        /// </summary>
        public string PropGroupTemplId { get; set; }

        /// <summary>
        /// Needed for building instance from template.
        /// Used to find property child.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// list of properties childs.
        /// </summary>
        public List<PropertyBase> ListProperty { get; set; }

        public void AddProperty(PropertyBase property)
        {
            ListProperty.Add(property);
        }
    }
}
