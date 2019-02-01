using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    public enum SearchBoolOperator
    {
        And,
        Or,
    }

    public enum BoolExprOperand
    {
        Left,
        Right
    }
    /// <summary>
    /// Search properties, boolean expression.
    /// 2 operands, one operator.
    /// </summary>
    public class SearchPropBoolExpr: SearchPropBase
    {
        public SearchPropBase LeftOperand { get; set; }
        public SearchPropBase RightOperand { get; set; }

        public SearchBoolOperator Operator { get; set; }

        public SearchPropBase GetLeftOrRight(BoolExprOperand operand)
        {
            if (operand == BoolExprOperand.Left)
                return LeftOperand;

            return RightOperand;
        }

        public void SetLeftOrRight(BoolExprOperand operand, SearchPropBase prop)
        {
            if (operand == BoolExprOperand.Left)
            {
                LeftOperand = prop;
                return;
            }

            RightOperand= prop;
        }

    }
}
