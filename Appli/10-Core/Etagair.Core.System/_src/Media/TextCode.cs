using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    //public class TextCodeParam
    //{
    //    // name/position
    //    // type
    //}

    public class TextCode
    {
        public TextCode()
        {
            ParamsCount = 0;
        }

        public string Id
        { get; set; }

        public string Code { get; set; }

        public int ParamsCount { get; set; }        
        // parameters definition: list and type
        // ParamsDef
    }
}
