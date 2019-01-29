using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    /// <summary>
    /// Action, matching a rule.
    /// </summary>
    public abstract class PropTemplRuleActionBase
    {
        public void SetRule(PropTemplRuleBase rule)
        {
            HasBeenExecuted = false;
            RuleId = rule.Id;
        }

        /// <summary>
        /// the action has been executed on creating an instance from the template.
        /// </summary>
        public bool HasBeenExecuted { get; set; }

        /// <summary>
        /// The rule Id, (is saved in the proprety in the entity).
        /// </summary>
        public string RuleId { get; set; }
    }
}
