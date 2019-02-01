using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    public class Error : ErrorBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="code"></param>
        public Error(string code) : base(code) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="code"></param>
        public Error(string code, string param) : base(code, param) { }
    }
}
