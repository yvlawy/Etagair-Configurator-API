using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.Tests
{
    [TestClass]
    public class TestLanguage_LiteDB : TestLanguage
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
