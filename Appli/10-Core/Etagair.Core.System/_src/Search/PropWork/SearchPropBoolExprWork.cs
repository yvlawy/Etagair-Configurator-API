using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    public class SearchPropBoolExprWork: SearchPropBaseWork
    {
        public SearchPropBaseWork LeftOperand { get; set; }
        public SearchPropBaseWork RightOperand { get; set; }

        public SearchBoolOperator Operator { get; set; }

        public SearchPropBaseWork GetLeftOrRight(BoolExprOperand operand)
        {
            if (operand == BoolExprOperand.Left)
                return LeftOperand;

            return RightOperand;
        }

        public void SetLeftOrRight(BoolExprOperand operand, SearchPropBaseWork prop)
        {
            if (operand == BoolExprOperand.Left)
            {
                LeftOperand = prop;
                return;
            }

            RightOperand = prop;
        }

    }
}
