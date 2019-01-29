using Etagair.Core.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.Tests.TestTempl
{
    /// <summary>
    /// Test templating editing (not templating).
    /// 
    /// Entity template, no prop group (only final prop), no rule.
    /// </summary>
    [TestClass]
    public class TestTemplEdit_Ent
    {
        public string RepositConfig = "InMemory";

        [TestInitialize]
        public void Init()
        {
        }

        [TestMethod]
        public void CreateEntTempl()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            // create an entity template to instantiate
            EntityTempl entityTempl = core.EditorTempl.CreateEntityTempl("entityTempl");
            Assert.IsNotNull(entityTempl.Id, "the id should be set");
            Assert.AreEqual("entityTempl", entityTempl.Name, "the name is wrong");
            Assert.IsNotNull(entityTempl.ParentFolderId, "the parent folder id should exists, its the root folder)");
        }

        // check the root prop
        // TODO:

        [TestMethod]
        public void EntOneProp_KString_VString()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            // create an entity template to instantiate
            EntityTempl templComputer = core.EditorTempl.CreateEntityTempl("TemplComputer");

            // add property
            PropTempl propTempl = core.EditorTempl.CreatePropTempl(templComputer, "Type", "Computer");

            // check the property key (type and value)
            PropKeyTemplString  propKeyString = propTempl.Key as PropKeyTemplString;
            Assert.IsNotNull(propKeyString, "the key should be a string");
            Assert.AreEqual("Type", propKeyString.Key, "the key should be 'Type'");

            // check the property value (type and value)
            PropValueTemplString propValueString = propTempl.Value as PropValueTemplString;
            Assert.IsNotNull(propValueString, "the value should be a string");
            Assert.AreEqual("Computer", propValueString.Value, "the key should be 'Computer'");
        }

        // public void EntOneProp_KTextCode_VString()

        // public void EntOneProp_KString_VTextCode()

        // public void EntOneProp_KTextCode_VTextCode()
    }
}
