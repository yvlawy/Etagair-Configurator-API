using Etagair.Core.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.Tests
{
    [TestClass]
    public class TestCreateEntity
    {
        public string RepositConfig = "InMemory";

        [TestInitialize]
        public void Init()
        {
        }

        /// <summary>
        /// Create one empty entity under the root.
        /// </summary>
        [TestMethod]
        public void CreateEntityUnderRoot()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            // create an ent
            Entity toshibaCoreI7 = core.Editor.CreateEntity();
            Assert.IsNotNull(toshibaCoreI7.Id, "id should be set");
            Assert.IsNotNull(toshibaCoreI7.PropertyRoot, "the prop root should exists");
            Assert.IsNotNull(toshibaCoreI7.ParentFolderId, "the parent folder should exists (its the root folder)");

            Assert.IsNull(toshibaCoreI7.EntityTemplId, "no entity template id is expected");
            Assert.AreEqual(BuildFrom.Scratch, toshibaCoreI7.BuildFrom, "build from scratch, not from a template");
            Assert.AreEqual(true, toshibaCoreI7.BuildFromScratchFinishedAuto, "BuildFromScratchFinishedAuto should be true");
            Assert.AreEqual(true, toshibaCoreI7.BuildFinished, "BuildFinished should be true");
        }


        /// <summary>
        ///  
        /// $$$Root\
        ///    E: "Name"= "Toshiba"   
        ///     
        /// </summary>
        [TestMethod]
        public void CreateEntityUnderRoot_Prop_KeyString_ValString()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            // create an ent
            Entity toshibaCoreI7 = core.Editor.CreateEntity();

            // Add a property to an object: key - value
            Property propName= core.Editor.CreateProperty(toshibaCoreI7, "Name", "Toshiba");

            // check the property key (type and value)
            PropertyKeyString propKeyString = propName.Key as PropertyKeyString;
            Assert.IsNotNull(propKeyString, "the key should be a string");
            Assert.AreEqual("Name", propKeyString.Key, "the key should be 'Name'");

            // check the property value (type and value)
            //PropertyValueString propValueString = propName.Value as PropertyValueString;
            ValString propValueString = propName.Value as ValString;
            Assert.IsNotNull(propValueString, "the value should be a string");
            Assert.AreEqual("Toshiba", propValueString.Value, "the key should be 'Toshiba'");
        }

        /// <summary>
        /// create a property, the key and the value are textCode.
        /// 
        /// $$$Root\
        ///    E: tc:"Name"= tc:"Toshiba"
        ///     
        /// </summary>
        [TestMethod]
        public void CreateEntityUnderRoot_Prop_KeyTextCode_ValTextCode()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            TextCode tcName = core.Editor.CreateTextCode("Name");
            TextCode tcToshiba = core.Editor.CreateTextCode("Toshiba");

            // create an ent
            Entity toshibaCoreI7 = core.Editor.CreateEntity();

            // Add a property to an object: key - value
            Property propName = core.Editor.CreateProperty(toshibaCoreI7, tcName, tcToshiba);

            // check the property key (type and value)
            PropertyKeyTextCode propKeyTextCode = propName.Key as PropertyKeyTextCode;
            Assert.IsNotNull(propKeyTextCode, "the key should be a TextCode");
            Assert.AreEqual(tcName.Id, propKeyTextCode.TextCodeId, "the key should be 'Name'");

            // check the property value (type and value)
            ValTextCodeId propValueTextCode = propName.Value as ValTextCodeId;
            Assert.IsNotNull(propValueTextCode, "the value should be a TextCode");
            Assert.AreEqual(tcToshiba.Id, propValueTextCode.TextCodeId, "the key should be 'Toshiba'");
        }

        /// <summary>
        ///  
        /// $$$Root\
        ///     computers\
        ///         E: "Name"= "Toshiba"   
        ///     
        /// </summary>
        [TestMethod]
        public void CreateEntityInFolder()
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

            //---check that the folder has an entity child
            Assert.AreEqual(1, foldComputers.ListChildId.Count, "The folder should have one entity child");
            Assert.IsTrue(foldComputers.ListChildId.ContainsKey(toshibaCoreI7.Id), "The folder should have the defined entity child");
        }

        // check root prop of the entity
        // TODO:

        // propGroup and childs
    }
}
