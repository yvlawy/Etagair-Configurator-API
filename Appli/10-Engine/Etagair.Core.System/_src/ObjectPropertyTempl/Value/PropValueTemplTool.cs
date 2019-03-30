using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    public class PropValueTemplTool
    {
        public static PropValueTempl CreatePropValueTemplFromValue(bool value)
        {
            ValBool valBool = new ValBool();
            valBool.Value = value;

            return CreatePropValueTemplFromValue(valBool);
        }

        public static PropValueTempl CreatePropValueTemplFromValue(int value)
        {
            ValInt valInt = new ValInt();
            valInt.Value = value;

            return CreatePropValueTemplFromValue(valInt);
        }

        public static PropValueTempl CreatePropValueTemplFromValue(double value)
        {
            ValDouble valDouble = new ValDouble();
            valDouble.Value = value;
            
            return CreatePropValueTemplFromValue(valDouble);
        }

        public static PropValueTempl CreatePropValueTemplFromValue(string value)
        {
            
            ValString valString = null;

            if(value!=null)
            {
                valString = new ValString();
                valString.Value = value;
            }
            return CreatePropValueTemplFromValue(valString);
        }

        public static PropValueTempl CreatePropValueTemplFromValue(IValue value)
        {
            PropValueTempl propValue = new PropValueTempl();
            propValue.Value = value;

            return propValue;
        }
    }
}
