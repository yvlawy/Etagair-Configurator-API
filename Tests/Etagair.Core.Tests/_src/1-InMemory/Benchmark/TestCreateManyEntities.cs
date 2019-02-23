using Etagair.Core.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Etagair.Core.Tests
{
    [TestClass]
    public class TestCreateManyEntities
    {
        public string RepositConfig = "InMemory";

        [TestInitialize]
        public void Init()
        { }

        [TestMethod]
        public void Create_100_Entities_Empty()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //==== creates entities, with prop
            //----create entities
            Entity entity;
            int count = 100;

            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing.
            stopwatch.Start();

            for (int i = 0; i < count;i++)
            {
                entity = core.Editor.CreateEntity();
            }
            
            // Stop timing.
            stopwatch.Stop();

            // check the entities count
            int countFound = core.Editor.GetRootFolder().ListChildId.Count;
            Assert.AreEqual(count, countFound, "Missing some entities!");
            
            Assert.IsTrue(stopwatch.Elapsed.TotalMilliseconds < 10, "Should takes less than 10 millisec");
        }
    }
}
