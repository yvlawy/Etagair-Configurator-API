using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Etagair.Core.Tests.TestTempl
{
    [TestClass]
    public class TestTemplEdit_Ent_LiteDB: TestTemplEdit_Ent
    {
        [TestInitialize]
        public new void Init()
        {
            // for LiteDB usage: create a unique db file
            RepositConfig = Guid.NewGuid().ToString();
        }

        [TestCleanup]
        public void Cleanup()
        {
            // remove the db file
            //Common.RemoveLiteDBFile(RepositConfig);
            // TODO: pb files are used by another process.
        }
    }
}
