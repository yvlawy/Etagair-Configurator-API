using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    /// <summary>
    /// To manage the instantiation of an entity from a template,
    /// done in several steps.
    /// </summary>
    public class EntityTemplToInst: TemplToInstBase
    {
        public EntityTemplToInst()
        {
            ListRule = new List<PropTemplRuleBase>();
            ListActionMatches = new List<PropTemplRuleActionBase>();

            ListRuleToExecWithoutAction = new List<PropTemplRuleBase>();
        }

        /// <summary>
        /// The template to build the entity
        /// </summary>
        public EntityTempl EntityTempl { get; set; }

        /// <summary>
        /// The properties rules to build properties of the entity.
        /// </summary>
        public List<PropTemplRuleBase> ListRule { get; set; }
        public List<PropTemplRuleBase> ListRuleToExecWithoutAction { get; set; }
        

        /// <summary>
        /// actions matching rules.
        /// </summary>
        public List<PropTemplRuleActionBase> ListActionMatches { get; set; }

        /// <summary>
        /// The final built entity, from the template
        /// </summary>
        public Entity Entity { get; set; }

        // save rules
        public void SetNewListRule(List<PropTemplRuleBase> listRuleToExec)
        {
            // clear previous content
            ListRule.Clear();
            ListRule.AddRange(listRuleToExec);
        }

        public void AddRulesWithoutAction(List<PropTemplRuleBase> listRuleToExecWithoutAction)
        {
            ListRuleToExecWithoutAction.AddRange(listRuleToExecWithoutAction);
        }

        public void AddActions(List<PropTemplRuleActionBase> listActionMatches)
        {
            ListActionMatches.AddRange(listActionMatches);
        }

    }
}
