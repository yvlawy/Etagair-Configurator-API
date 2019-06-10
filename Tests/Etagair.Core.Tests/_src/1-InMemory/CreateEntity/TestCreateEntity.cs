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
        public void CreateEntity()
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
        /// create a property, the key and the value are textCode.
        /// 
        /// ----Data:
        ///   E: tc:"Name"= tc:"Toshiba"
        ///     
        /// </summary>
        [TestMethod]
        public void CreateEntity_Prop_KeyTextCode_ValTextCode()
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
        /// create a property, under the entity root property.
        ///  
        /// ----Data:
        ///  E: "Name"= "Toshiba"   
        ///     
        /// </summary>
        [TestMethod]
        public void CreateEntity_Prop_KeyString_ValString()
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
        /// create a property, under the entity root property.
        ///  
        /// ----Data:
        ///  E: "Year"= 2019   
        ///     
        /// </summary>
        [TestMethod]
        public void CreateEntity_Prop_KeyString_ValInt()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            // create an ent
            Entity toshibaCoreI7 = core.Editor.CreateEntity();

            // Add a property to an object: key - value
            Property propName = core.Editor.CreateProperty(toshibaCoreI7, "Year", 2019);

            // check the property key (type and value)
            PropertyKeyString propKeyString = propName.Key as PropertyKeyString;
            Assert.IsNotNull(propKeyString, "the key should be a string");
            Assert.AreEqual("Year", propKeyString.Key, "the key should be 'Year'");

            // check the property value (type and value)
            ValInt propValueInt = propName.Value as ValInt;
            Assert.IsNotNull(propValueInt, "the value should be an int");
            Assert.AreEqual(2019, propValueInt.Value, "the key should be an int");
        }

        /// <summary>
        /// create a property, under the entity root property.
        ///  
        /// ----Data:
        ///  E: "Value"= 12.5
        ///     
        /// </summary>
        [TestMethod]
        public void CreateEntity_Prop_KeyString_ValDouble()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            // create an ent
            Entity toshibaCoreI7 = core.Editor.CreateEntity();

            // Add a property to an object: key - value
            Property propName = core.Editor.CreateProperty(toshibaCoreI7, "Value", 12.5);

            // check the property key (type and value)
            PropertyKeyString propKeyString = propName.Key as PropertyKeyString;
            Assert.IsNotNull(propKeyString, "the key should be a string");
            Assert.AreEqual("Value", propKeyString.Key, "the key should be 'Value'");

            // check the property value (type and value)
            ValDouble propValueDouble = propName.Value as ValDouble;
            Assert.IsNotNull(propValueDouble, "the value should be a  double");
            Assert.AreEqual(12.5, propValueDouble.Value, "the key should be 12.5");
        }

        /// <summary>
        /// create a property, under the entity root property.
        ///  
        /// ----Data:
        ///  E: "Done"= true
        ///     
        /// </summary>
        [TestMethod]
        public void CreateEntity_Prop_KeyString_ValBool()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            // create an ent
            Entity toshibaCoreI7 = core.Editor.CreateEntity();

            // Add a property to an object: key - value
            Property propName = core.Editor.CreateProperty(toshibaCoreI7, "Done", true);

            // check the property key (type and value)
            PropertyKeyString propKeyString = propName.Key as PropertyKeyString;
            Assert.IsNotNull(propKeyString, "the key should be a string");
            Assert.AreEqual("Done", propKeyString.Key, "the key should be 'Value'");

            // check the property value (type and value)
            ValBool propValueBool = propName.Value as ValBool;
            Assert.IsNotNull(propValueBool, "the value should be a bool");
            Assert.AreEqual(true, propValueBool.Value, "the key should be true");
        }

        /// <summary>
        /// create a property, the key and the value are textCode.
        /// 
        /// ----Data:
        ///   E: tc:"Power" =  12.0
        ///     
        /// </summary>
        [TestMethod]
        public void CreateEntity_Prop_KeyTextCode_ValDouble()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            TextCode tcName = core.Editor.CreateTextCode("Power");

            // create an ent
            Entity toshibaCoreI7 = core.Editor.CreateEntity();

            // Add a property to an object: key - value
            Property propName = core.Editor.CreateProperty(toshibaCoreI7, tcName, 12.0);

            // check the property key (type and value)
            PropertyKeyTextCode propKeyTextCode = propName.Key as PropertyKeyTextCode;
            Assert.IsNotNull(propKeyTextCode, "the key should be a TextCode");
            Assert.AreEqual(tcName.Id, propKeyTextCode.TextCodeId, "the key should be 'Name'");

            // check the property value (type and value)
            ValDouble propValueDouble = propName.Value as ValDouble;
            Assert.IsNotNull(propValueDouble, "the value should be a double");
            Assert.AreEqual(12, propValueDouble.Value, "the value should be 12.0");
        }

        /// <summary>
        /// create a property, the key and the value are textCode.
        /// 
        /// ----Data:
        ///   E: tc:"Level" =  15
        ///     
        /// </summary>
        [TestMethod]
        public void CreateEntity_Prop_KeyTextCode_ValInt()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            TextCode tcName = core.Editor.CreateTextCode("Level");

            // create an ent
            Entity toshibaCoreI7 = core.Editor.CreateEntity();

            // Add a property to an object: key - value
            Property propName = core.Editor.CreateProperty(toshibaCoreI7, tcName, 15);

            // check the property key (type and value)
            PropertyKeyTextCode propKeyTextCode = propName.Key as PropertyKeyTextCode;
            Assert.IsNotNull(propKeyTextCode, "the key should be a TextCode");
            Assert.AreEqual(tcName.Id, propKeyTextCode.TextCodeId, "the key should be 'Name'");

            // check the property value (type and value)
            ValInt propValueInt = propName.Value as ValInt;
            Assert.IsNotNull(propValueInt, "the value should be an int");
            Assert.AreEqual(15, propValueInt.Value, "the value should be 15");
        }

        /// <summary>
        /// create a property, the key and the value are textCode.
        /// 
        /// ----Data:
        ///   E: tc:"On" =  true
        ///     
        /// </summary>
        [TestMethod]
        public void CreateEntity_Prop_KeyTextCode_ValBool()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            TextCode tcName = core.Editor.CreateTextCode("On");

            // create an ent
            Entity toshibaCoreI7 = core.Editor.CreateEntity();

            // Add a property to an object: key - value
            Property propName = core.Editor.CreateProperty(toshibaCoreI7, tcName, true);

            // check the property key (type and value)
            PropertyKeyTextCode propKeyTextCode = propName.Key as PropertyKeyTextCode;
            Assert.IsNotNull(propKeyTextCode, "the key should be a TextCode");
            Assert.AreEqual(tcName.Id, propKeyTextCode.TextCodeId, "the key should be 'Name'");

            // check the property value (type and value)
            ValBool propValueBool = propName.Value as ValBool;
            Assert.IsNotNull(propValueBool, "the value should be a bool");
            Assert.AreEqual(true, propValueBool.Value, "the value should be 15");
        }

        /// <summary>
        ///  
        /// F: computers\
        ///   E: "Name"= "Toshiba"   
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
