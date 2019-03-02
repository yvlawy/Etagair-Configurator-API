using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    /// <summary>
    /// Action, corresponding to a rule.
    /// The value of the property is provided by the action.
    /// 
    /// PropTemplRuleActionValueToSet
    /// </summary>
    public class PropTemplRuleActionValueToSet : PropTemplRuleActionBase   
    {
        public PropTemplRuleActionValueToSet ()
        { }

        public void SetValueString(string value)
        {
            ValueString = value;
        }


        public void SetValueTextCodeId(string textCodeId)
        {
            ValueTextCodeId = textCodeId;
        }

        /// <summary>
        /// The prop value can be a string.
        /// </summary>
        public string ValueString { get; set; }

        /// <summary>
        /// The prop value can be a textCode.
        /// </summary>
        public string ValueTextCodeId { get; set; }
    }
}
