using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    public abstract class PropTemplBase
    {
        public PropTemplBase()
        {
            ListRule = new List<PropTemplRuleBase>();
            //ListRuleExecuted = new List<PropTemplRuleBase>();
        }

        /// <summary>
        /// The id property group parent.
        /// </summary>
        public string PropGroupTemplParentId { get; set; }

        /// <summary>
        /// The key can be a string or a TextCode.
        /// Can have rules to define the name, ...
        /// </summary>
        public PropKeyTemplBase Key { get; set; }

        public List<PropTemplRuleBase> ListRule { get; set; }

        //public List<PropTemplRuleBase> ListRuleExecuted { get; set; }

        public void SetKey(PropKeyTemplBase propKey)
        {
            Key = propKey;
        }
  
        public void AddRule(PropTemplRuleBase rule)
        {
            ListRule.Add(rule);
        }

    }
}
