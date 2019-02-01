using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    public abstract class PropValueTemplBase
    {
        public PropValueTemplBase()
        {
            IsNull = true;
        }

        public bool IsNull { get; set; }
    }
}
