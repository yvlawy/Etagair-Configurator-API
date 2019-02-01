using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    public enum ErrorLevel
    {
        Warning,
        Error,
        FatalError
    }

    /// <summary>
    /// Base of all error class.
    /// </summary>
    public abstract class ErrorBase
    {
        public string Code;

        public ErrorLevel Level = ErrorLevel.Warning;

        // child errors?

        // properties code-value

        // properties value(param)
        public string Param = "";

        public string Param2 = "";

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="code"></param>
        public ErrorBase(string code)
        {
            Code = code;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="code"></param>
        public ErrorBase(string code, string param)
        {
            Code = code;
            Param = param;
        }

    }
}
