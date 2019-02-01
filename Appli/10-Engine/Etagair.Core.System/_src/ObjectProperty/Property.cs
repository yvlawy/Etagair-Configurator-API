using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    /// <summary>
    /// A final entity property: its a pair of key-value.
    /// The key is defined in the base class.
    /// </summary>
    public class Property: PropertyBase
    {
        public PropertyValueBase Value { get; set; }

        public void SetKeyValue(PropertyKeyBase key, PropertyValueBase value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Set the value.
        /// The key is already set.
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(PropertyValueBase value)
        {
            Value = value;
        }

    }
}
