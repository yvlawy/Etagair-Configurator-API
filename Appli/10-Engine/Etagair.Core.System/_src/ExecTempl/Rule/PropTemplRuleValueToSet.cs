using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    public enum PropValueType
    {
        String,
        // Integer, Double, Bool,.....

        TextCode
        // ImageCode,...
    }

    /// <summary>
    /// A property template rule:
    /// the property value have to be set on instantiation of the property.
    /// </summary>
    public class PropTemplRuleValueToSet : PropTemplRuleBase
    {
        public PropTemplRuleValueToSet()
        {
            Type = PropTemplRuleType.PropValueToSet;
        }

        /// <summary>
        /// Type of the value: string or textCode.
        /// </summary>
        public PropValueType ValueType { set; get; }
    }
}
