using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    /// <summary>
    /// Type of the rule.
    /// (same the hierarchical classes).
    /// </summary>
    public enum FoldTemplRuleType
    {
        Undefined,

    }

    public abstract class FoldTemplRuleBase
    {
        public FoldTemplRuleType Type { get; set; }
    }
}
