using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    /// <summary>
    /// Type of the rule.
    /// (same the hierarchical classes).
    /// </summary>
    public enum PropTemplRuleType
    {
        Undefined,

        /// <summary>
        /// The property value will be set on instantiation of the property
        /// (The key is defined).
        /// </summary>
        PropValueSetOnInstance,
    }

    /// <summary>
    /// A property template rule.
    /// Used when a property template is instantiated.
    /// </summary>
    public abstract class PropTemplRuleBase
    {
        public PropTemplRuleBase()
        {
        }

        public string Id { get; set; }

        public PropTemplRuleType Type { get; set; }

        public string EntityTemplId { get; set; }

        /// <summary>
        /// The property group parent
        /// </summary>
        public string PropGroupTemplId { get; set; }

        /// <summary>
        /// The key of the property template.
        /// Its a string or a textCode.
        /// (its a copy!)
        /// </summary>
        public PropKeyTemplBase PropKeyTempl { get; set; }
    }
}
