using Etagair.Core.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.Tests
{
    [TestClass]
    public class TestCreateEntityPropGroup
    {
        public string RepositConfig = "InMemory";

        [TestInitialize]
        public void Init()
        {
        }

        /// <summary>
        ///  
        /// $$$Root\
        ///    Ent: 
        ///      PG: "Processor"\
        ///         P: "Constructor"= "Intel"
        ///         P: "Model"= "i7"
        ///     
        /// </summary>
        [TestMethod]
        public void CreateEntityPropGroup()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            // create an ent
            Entity toshibaCoreI7 = core.Editor.CreateEntity();

            // add a properties group (folder inside object) in the object
            PropertyGroup propGrpProc = core.Editor.CreatePropertyGroup(toshibaCoreI7, "Processor");
            Assert.IsNotNull(propGrpProc, " The PG Processor should exists");

            // get the prop group
            PropertyGroup propGrpProcGet= toshibaCoreI7.PropertyRoot.ListProperty[0] as PropertyGroup;
            Assert.IsNotNull(propGrpProcGet, " The PG Processor should exists");
            Assert.AreEqual("Processor", Common.GetPropertyKeyContent(propGrpProcGet.Key), "The Processor key text is wrong");

            PropertyGroup propGrpProc2 = core.Editor.CreatePropertyGroup(toshibaCoreI7, "Processor");
            Assert.IsNull(propGrpProc2, " This second PG Processor should not exists");

            // create prop childs into the prop group
            core.Editor.CreateProperty(toshibaCoreI7, propGrpProc, "Constructor", "Intel");
            Assert.AreEqual(1, propGrpProc.ListProperty.Count, "The prop group should have one prop childs");
            Property propContructorIntel = propGrpProc.ListProperty[0] as Property;
            Assert.AreEqual("Constructor", Common.GetPropertyKeyContent(propContructorIntel.Key), "the prop child key should be: Constructor");
            Assert.AreEqual("Intel", Common.GetPropertyValueContent(propContructorIntel.Value), "the prop child key should be: Constructor");

            // create another prop childs into the prop group
            core.Editor.CreateProperty(toshibaCoreI7, propGrpProc, "Model", "i7");
            Assert.AreEqual(2, propGrpProc.ListProperty.Count, "The prop group should have 2 prop childs");
        }
    }
    }
