using Etagair.Core;
using Etagair.Core.System;
using Etagair.Engine;
using System;

namespace DevUseFromNugetApp
{
    /// <summary>
    /// Samples to put into doc/wiki,...
    /// 
    /// </summary>
    public class DevEtagairEngine
    {
        public void Run()
        {
            SampleBasicCreateEntity();
        }

        private void CreateEngine()
        {
            EtagairEngine engine = new EtagairEngine();

            // the path must exists, location where to put the database file
            string dbPath = @".\Data\";

            // create the database or reuse the existing one
            if (!engine.Init(dbPath))
            {
                Console.WriteLine("Db initialization Failed.");
                return;
            }

            // the database is created or reused and opened, ready to the execution
            Console.WriteLine("Db initialized with success.");
        }

        /// <summary>
        /// Very basic sample: create a folder and an entity with properties (key-value).
        /// 
        ///  F:computers\
        ///    E: 
        ///     "Name"= "Toshiba Satellite Core I7"
        ///     "Trademark"= "Toshiba"
        ///     
        /// </summary>
        private void SampleBasicCreateEntity()
        {
            EtagairEngine engine = new EtagairEngine();

            // the path must exists
            string dbPath = @".\Data\";
            if (!engine.Init(dbPath))
            {
                Console.WriteLine("Db initialization Failed.");
                return;
            }

            // create a folder, under the root
            Folder foldComputers = engine.Editor.CreateFolder(null, "computers");

            // create an entity, under the computers folder
            Entity toshibaCoreI7 = engine.Editor.CreateEntity(foldComputers);

            // Add 2 properties to the entity (key - value)
            engine.Editor.CreateProperty(toshibaCoreI7, "Name", "Toshiba Satellite Core I7");
            engine.Editor.CreateProperty(toshibaCoreI7, "Trademark", "Toshiba");
        }

        private void SampleCreateThreeEntitiesSearchPropKeyName()
        {
            EtagairEngine engine = new EtagairEngine();

            // the path must exists
            string dbPath = @".\Data\";
            if (!engine.Init(dbPath))
            {
                Console.WriteLine("Db initialization Failed.");
                return;
            }

        }
            // todo: create several computers entities, then search ...
        }
    }
