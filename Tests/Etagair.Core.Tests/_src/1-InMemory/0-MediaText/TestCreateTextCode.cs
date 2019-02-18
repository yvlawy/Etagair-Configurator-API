using Etagair.Core.Reposit.Contract;
using Etagair.Core.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.Tests
{
    [TestClass]
    public class TestCreateTextCode
    {
        public string RepositConfig = "InMemory";

        [TestInitialize]
        public void Init()
        {
        }

        [TestMethod]
        public void CreateTextCode()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            TextCode tcName = core.Editor.CreateTextCode("Name");
            Assert.IsNotNull(tcName.Id, "id should be set");
        }

        [TestMethod]
        public void CreateTextCode_2times_failed()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            TextCode tcName = core.Editor.CreateTextCode("Name");
            Assert.IsNotNull(tcName.Id, "id should be set");

            TextCode tcName2 = core.Editor.CreateTextCode("Name");
            Assert.IsNull(tcName2, "can't be created");
        }

        [TestMethod]
        public void CreateTextCode_codeNull_Failed()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            TextCode tcName = core.Editor.CreateTextCode("");
            Assert.IsNull(tcName, "can't be created");
        }

        [TestMethod]
        public void CreateTextCode_Find_Ok()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            TextCode tcName = core.Editor.CreateTextCode("Name");

            TextCode tcNameFind = core.Searcher.FindTextCodeById(tcName.Id);
            Assert.AreEqual(tcName.Code, tcNameFind.Code, "Both Code should match");
        }
    }
}
