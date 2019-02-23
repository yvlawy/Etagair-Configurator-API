using Etagair.Core.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Etagair.Core.Tests
{
    /// <summary>
    /// Doesn't override a base class!!
    /// because benchmark is specific to an implementation.
    /// </summary>
    [TestClass, TestCategory("LongTest")]
    public class TestCreateManyEntities_LiteDB
    {
        public string RepositConfig;

        [TestInitialize]
        public void Init()
        {
            // for LiteDB usage: create a unique db file
            RepositConfig = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Create 100 empty entities takes 600 millisec with LiteDB.
        /// </summary>
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

            for (int i = 0; i < count; i++)
            {
                entity = core.Editor.CreateEntity();
            }

            // Stop timing.
            stopwatch.Stop();

            // check the entities count
            int countFound = core.Editor.GetRootFolder().ListChildId.Count;
            Assert.AreEqual(count, countFound, "Missing some entities!");

            Assert.IsTrue(stopwatch.Elapsed.Milliseconds < 700, "Takes too many times!");
        }

        /// <summary>
        /// Create 1000 empty entities takes 7-8 sec with LiteDB.
        /// =850Ko
        /// </summary>
        [TestMethod]
        public void Create_1000_Entities_Empty()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //==== creates entities, with prop
            //----create entities
            Entity entity;
            int count = 1000;

            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing.
            stopwatch.Start();

            for (int i = 0; i < count; i++)
            {
                entity = core.Editor.CreateEntity();
            }

            // Stop timing.
            stopwatch.Stop();

            // check the entities count
            int countFound = core.Editor.GetRootFolder().ListChildId.Count;
            Assert.AreEqual(count, countFound, "Missing some entities!");

            // should takes around 7-8 sec
            Assert.IsTrue(stopwatch.Elapsed.TotalSeconds < 12, "Takes too many times!");
        }

        /// <summary>
        /// Create 1000 empty entities takes 7-8 sec with LiteDB.
        /// On a std Hard Disk
        /// db size: 7,8Mo
        /// </summary>
        [TestMethod]
        public void Create_10000_Entities_Empty()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //==== creates entities, with prop
            //----create entities
            Entity entity;
            int count = 10000;

            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing.
            stopwatch.Start();

            for (int i = 0; i < count; i++)
            {
                entity = core.Editor.CreateEntity();
            }

            // Stop timing.
            stopwatch.Stop();

            // check the entities count
            int countFound = core.Editor.GetRootFolder().ListChildId.Count;
            Assert.AreEqual(count, countFound, "Missing some entities!");

            // should takes around 350 sec/4min
            Assert.IsTrue(stopwatch.Elapsed.TotalSeconds < 400, "Takes too many times!");
        }

    }
}
