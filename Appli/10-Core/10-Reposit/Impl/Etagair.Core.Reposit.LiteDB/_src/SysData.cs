using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.Reposit.LiteDB
{
    public class SysData
    {
        LiteRepository _dbEngine;

        public SysData()
        {
        }

        public LiteRepository DBEngine
        {
            get { return _dbEngine; }
        }

        public void SetDBEngine(LiteRepository dbEngine)
        {
            _dbEngine = dbEngine;
        }

    }
}
