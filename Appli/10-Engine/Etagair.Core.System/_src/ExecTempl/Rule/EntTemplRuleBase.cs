using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    /// <summary>
    /// Type of the rule.
    /// (same the hierarchical classes).
    /// </summary>
    public enum EntTemplRuleType
    {
        Undefined,

        /// <summary>
        /// The property value will be set on instantiation of the property
        /// (The key is defined).
        /// </summary>
        //PropValueSetOnInstance,
    }

    public abstract class EntTemplRuleBase
    {
        public EntTemplRuleType Type { get; set; }
    }
}
