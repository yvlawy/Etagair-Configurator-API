using Etagair.Core.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core
{
    public class ValueTool
    {
        /// <summary>
        /// Clone the value. Createan object on the same and copy the value content.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IValue CloneValue(IValue value)
        {
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

    }
}
