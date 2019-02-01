using Etagair.Core.Reposit.Contract;
using Etagair.Core.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core
{
    /// <summary>
    /// Execute propTempl rules.
    /// when executing (instantiate)  an entity from a template (EntTempl).
    /// </summary>
    public class ProcessEntPropTemplRules
    {
        IEtagairReposit _reposit;

        public ProcessEntPropTemplRules(IEtagairReposit reposit)
        {
            _reposit = reposit;
        }

        /// <summary>
        /// Execute rules of the propTempl.
        /// 
        ///   2- has rule(s), scan rules:
        ///      2.1- is PropValueSetOnInstance
        ///         2.1.1- Create the key by copy from the template
        ///         2.1.2- has no action -> exit, need action
        ///         2.1.2- has action
        ///             2.1.2.1- Create the value by provided in the action
        /// </summary>
        /// <param name="propTempl"></param>
        /// <returns></returns>
        public bool ExecRules(PropTempl propTempl, PropertyGroup propGroupParent, List<PropTemplRuleActionBase> listAction, List<PropTemplRuleBase> listRulesNeedActions)
        {
            bool res = true;

            // 2- has rule(s), scan each one
            foreach (PropTemplRuleBase rule in propTempl.ListRule)
            {
                // 2.1- is rule PropValueSetOnInstance?
                PropTemplRuleValueSetOnInst ruleValueSetOnInst = rule as PropTemplRuleValueSetOnInst;
                if (ruleValueSetOnInst != null)
                {
                    res &= ExecPropTemplRuleValueSetOnInst(propTempl, propGroupParent, ruleValueSetOnInst, listAction, listRulesNeedActions);
                }

                // other rule type?
                // not implemented
            }

            return res;
        }

        /// <summary>
        /// Create a property key, based on the template.
        /// </summary>
        /// <param name="propTempl"></param>
        /// <returns></returns>
        public PropertyKeyBase CreatePropKeyFromTempl(PropTemplBase propTempl)
        {
            // create the key of the property, from the template
            PropKeyTemplTextCode templKeyTextCode = propTempl.Key as PropKeyTemplTextCode;
            if (templKeyTextCode != null)
            {
                PropertyKeyTextCode propKey = new PropertyKeyTextCode();
                propKey.TextCodeId = templKeyTextCode.TextCodeId;
                return propKey;
            }

            PropKeyTemplString templKeyString = propTempl.Key as PropKeyTemplString;
            if (templKeyString != null)
            {
                PropertyKeyString propKey = new PropertyKeyString();
                propKey.Key = templKeyString.Key;
                return propKey;
            }

            throw new Exception("Property Key templ not yet implemented!");
        }

        public PropertyValueBase CreatePropValueFromTempl(PropValueTemplBase propTemplValue)
        {
            PropValueTemplString propValueTemplString = propTemplValue as PropValueTemplString;
            if (propValueTemplString != null)
            {
                PropertyValueString propValueString = new PropertyValueString();
                propValueString.Value = propValueTemplString.Value;
                return propValueString;
            }

            PropValueTemplTextCode propValueTemplTextCode = propTemplValue as PropValueTemplTextCode;
            if (propValueTemplTextCode != null)
            {
                PropertyValueTextCode propValueTextCode = new PropertyValueTextCode();
                propValueTextCode.TextCodeId = propValueTemplTextCode.TextCodeId;
                return propValueTextCode;
            }

            throw new Exception("property Value type not yet implemented!");
        }

        #region Private methods

        /// <summary>
        /// 1- Create the key by copy from the template
        /// 2- has no action -> exit, need action
        /// 3- has action
        ///   3.1- Create the value by provided in the action
        /// 
        /// </summary>
        /// <param name="propTempl"></param>
        /// <param name="ruleValueSetOnInst"></param>
        /// <returns></returns>
        private bool ExecPropTemplRuleValueSetOnInst(PropTempl propTempl, PropertyGroup propGroupParent, PropTemplRuleValueSetOnInst ruleValueSetOnInst, List<PropTemplRuleActionBase> listAction, List<PropTemplRuleBase> listRulesNeedActions)
        {

            Property property = new Property();
            property.PropGroupParentId = propGroupParent.Id;
            propGroupParent.AddProperty(property);

            // 1- create the key by copy from the template
            PropertyKeyBase propKey = CreatePropKeyFromTempl(propTempl);
            property.SetKeyValue(propKey, null);

            // an action on this rule is provided?
            PropTemplRuleActionBase action = listAction.Find(a => a.RuleId.Equals(ruleValueSetOnInst.Id));
            if (action == null)
            {
                // no action provided for this rule!, need an action on this rule
                listRulesNeedActions.Add(ruleValueSetOnInst);
                // stops
                return true;
            }

            // check the type of the action, must match the rule type!
            PropTemplRuleValueSetOnInstAction actionSetOnInst = action as PropTemplRuleValueSetOnInstAction;
            if(actionSetOnInst==null)
            {
                // error! action type is wrong
                return false;
            }

            // move the rule in the list of rules executed/done
            //propTempl.MoveRuleToExecuted(ruleValueSetOnInst);

            // execute the action: create the prop Value 
            PropertyValueBase propValue = CreatePropValueFromAction(actionSetOnInst, ruleValueSetOnInst);

            // set the prop value, the key is set before
            property.SetValue(propValue);
            return true;
        }

        /// <summary>
        /// create the value by copy from the template.
        /// </summary>
        /// <param name="actionSetOnInst"></param>
        /// <param name="ruleValueSetOnInst"></param>
        /// <returns></returns>
        private PropertyValueBase  CreatePropValueFromAction(PropTemplRuleValueSetOnInstAction actionSetOnInst, PropTemplRuleValueSetOnInst ruleValueSetOnInst)
        {
            if (ruleValueSetOnInst.ValueType == PropValueType.String)
            {
                PropertyValueString propValueString = new PropertyValueString();
                propValueString.Value = actionSetOnInst.ValueString;
                return propValueString;
            }

            if (ruleValueSetOnInst.ValueType == PropValueType.TextCode)
            {
                PropertyValueTextCode propValueTextCode = new PropertyValueTextCode();
                propValueTextCode.TextCodeId = actionSetOnInst.ValueTextCodeId;
                return propValueTextCode;
            }

            throw new Exception("property Value type not yet implemented!");

        }

        #endregion
    }
}
