using Etagair.Core.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.Tests
{
    [TestClass]
    public class TestFindEntity
    {
        public string RepositConfig = "InMemory";

        [TestInitialize]
        public void Init()
        {
        }

        /// <summary>
        ///  
        /// $$$Root\
        ///     computers\
        ///     
        /// </summary>
        [TestMethod]
        public void FindFolderById()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //---- create a folder, under the root            
            Folder foldComputers = core.Editor.CreateFolder(null, "computers");

            Folder foldComputersFound = core.Searcher.FindFolderById(foldComputers.Id);
            Assert.IsNotNull(foldComputersFound, "The folder should exists");
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void FindEntityById()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //---- create an entity, under the root            
            Entity entity = core.Editor.CreateEntity(null);

            Entity entityFound = core.Searcher.FindEntityById(entity.Id);
            Assert.IsNotNull(entityFound, "The entity should exists");
        }

        /// <summary>
        ///  
        /// $$$Root\
        ///     computers\
        ///         Ent: Name=Toshiba   
        ///     
        /// </summary>
        [TestMethod]
        public void SearchEntityInFolder()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //---- create a folder, under the root            
            Folder foldComputers = core.Editor.CreateFolder(null, "computers");

            //==== creates entities, with prop
            //----create entities
            Entity toshibaCoreI7 = core.Editor.CreateEntity(foldComputers);
            // Add a property to an object: key - value, both are textCode (will be displayed translated, depending on the language)
            core.Editor.CreateProperty(toshibaCoreI7, "Name", "Toshiba Satellite Core I7");
            core.Editor.CreateProperty(toshibaCoreI7, "TradeMark", "Toshiba");

            Entity toshibaCoreI7Found = core.Searcher.FindEntityById(toshibaCoreI7.Id);
            Assert.IsNotNull(toshibaCoreI7, "The entity should exists");
        }

        /// <summary>
        ///  
        /// $$$Root\
        ///     computers\
        ///         Ent: Name=Toshiba   
        ///         Ent: Name=Dell      
        ///     
        /// </summary>
        [TestMethod]
        public void SearchTwoEntityInFolder()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //---- create a folder, under the root            
            Folder foldComputers = core.Editor.CreateFolder(null, "computers");

            //==== creates entities, with prop
            //----create entities
            Entity toshibaCoreI7 = core.Editor.CreateEntity(foldComputers);
            // Add a property to an object: key - value, both are textCode (will be displayed translated, depending on the language)
            core.Editor.CreateProperty(toshibaCoreI7, "Name", "Toshiba Satellite Core I7");
            core.Editor.CreateProperty(toshibaCoreI7, "TradeMark", "Toshiba");

            Entity dellCoreI7 = core.Editor.CreateEntity(foldComputers);
            // Add a property to an object: key - value, both are textCode (will be displayed translated depending on the language)
            core.Editor.CreateProperty(dellCoreI7, "Name", "Dell Inspiron 15-5570-sku6");
            core.Editor.CreateProperty(dellCoreI7, "TradeMark", "Dell");

            //---check, find the entities
            Entity toshibaCoreI7Found = core.Searcher.FindEntityById(toshibaCoreI7.Id);
            Assert.IsNotNull(toshibaCoreI7Found, "The entity should exists");

            Entity dellCoreI7Found = core.Searcher.FindEntityById(dellCoreI7.Id);
            Assert.IsNotNull(dellCoreI7Found, "The entity dellCoreI7 should exists");
        }

    }
}