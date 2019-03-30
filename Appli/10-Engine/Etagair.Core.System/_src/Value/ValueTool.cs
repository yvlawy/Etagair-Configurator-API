using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    /// <summary>
    /// Low-level value tool.
    /// </summary>
    public class ValueTool
    {
        /// <summary>
        /// Clone the value. Createan object on the same and copy the value content.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IValue CloneValue(IValue value)
        {
            if (value == null)
                return null;

            ValString valString = value as ValString;
            if (valString != null)
            {
                ValString valStringRes = new ValString();
                valStringRes.Value = valString.Value;
                return valStringRes;
            }

            ValDouble valDouble = value as ValDouble;
            if (valDouble != null)
            {
                ValDouble valDoubleRes = new ValDouble();
                valDoubleRes.Value = valDouble.Value;
                return valDoubleRes;
            }

            ValTextCodeId valTextCodeId = value as ValTextCodeId;
            if (valTextCodeId != null)
            {
                ValTextCodeId valTextCodeIdRes = new ValTextCodeId();
                valTextCodeIdRes.TextCodeId = valTextCodeId.TextCodeId;
                return valTextCodeIdRes;
            }

            // TODO: others types: int, bool, ImageCode,...
            throw new Exception("Value type not yet implemented!");
        }

        public static IValue CreateValueFromTempl(PropValueTempl propValueTempl)
        {
            return CloneValue(propValueTempl.Value);
        }

        public static bool ValueMemberIsNull(IValue value)
        {
            ValString valString = value as ValString;
            if (valString != null)
            {
                if (valString.Value == null)
                    return true;
                return false;
            }

            ValDouble valDouble = value as ValDouble;
            if (valDouble != null)
            {
                return true;
            }

            ValTextCodeId valTextCodeId = value as ValTextCodeId;
            if (valTextCodeId != null)
            {
                if(valTextCodeId.TextCodeId== null)
                    return true;
                return false;
            }

            // TODO: others types: int, bool, ImageCode,...
            throw new Exception("Value type not yet implemented!");
        }
    }
}
