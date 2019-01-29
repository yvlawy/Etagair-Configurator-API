using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    public enum TemplToInstStep
    {
        Starts,
        Ends,

        /// <summary>
        /// Need external action: set a value, ...
        /// </summary>
        NeedAction
    }

    public enum TemplToInstState
    {
        InProgress,
        Success,
        Failed,
    }
}
