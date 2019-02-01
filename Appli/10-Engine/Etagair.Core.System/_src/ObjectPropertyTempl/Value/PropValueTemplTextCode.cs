using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    public class PropValueTemplTextCode : PropValueTemplBase
    {
        private string _textCodeId = null;

        public string TextCodeId
        {
            get { return _textCodeId; }
            set
            {
                _textCodeId = value;
                if (_textCodeId == null) IsNull = true;
                else IsNull = false;
            }
        }
    }
}
