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
    [TestClass]
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

            Assert.IsTrue(stopwatch.Elapsed.Milliseconds < 700, "Takes too many times!");
        }

        /// <summary>
        /// Create 100 empty entities takes 600 millisec with LiteDB.
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


            Assert.IsTrue(stopwatch.Elapsed.TotalSeconds < 12, "Takes too many times!");
        }

    }
}
