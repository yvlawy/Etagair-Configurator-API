using Etagair.Core.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.Tests.TestTempl
{
    /// <summary>
    /// Test templating processsing
    /// 
    /// Entity template, no prop group (only final prop), no rule.
    /// </summary>
    [TestClass]
    public class TestTemplProcess_Ent
    {
        public string RepositConfig = "InMemory";

        /// <summary>
        /// Create an entity from a template.
        /// 
        /// Has one prop, no rule.
        /// 
        /// EntTmpl: TemplComputer
        ///     P: K=Type, V=Computer
        ///     
        /// Ent
        ///     P: K=Type, V=Computer
        /// </summary>
        /// <param name="core"></param>
        [TestMethod]
        public void EntOneProp_KString_VString()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            // create an entity template to instantiate
            EntityTempl templComputer = core.EditorTempl.CreateEntityTempl("TemplComputer");

            // add property
            core.EditorTempl.CreatePropTempl(templComputer, "Type", "Computer");

            //====Instanciate the template, create an entity, under the root folder
            EntityTemplToInst templToInst = core.ProcessTempl.CreateEntity(templComputer);

            //====check that the execution finishes with success
            Assert.AreEqual(TemplToInstState.Success, templToInst.State, "the state should be sucess");
            Assert.AreEqual(TemplToInstStep.Ends, templToInst.NextStep, "the next step should be ends");

            // check, get the property: Type=Computer
            PropertyBase propBase = core.Searcher.FindPropertyByKey(templToInst.Entity, templToInst.Entity.PropertyRoot, "Type", false);
            Assert.IsNotNull(propBase, "the propBase Type=Computer should exists");
            Property prop = propBase as Property;
            Assert.IsNotNull(prop, "the prop Type=Computer should exists");

            // check the key prop
            PropertyKeyString propKeyString = prop.Key as PropertyKeyString;
            Assert.IsNotNull(propKeyString, "the prop key string Type should exists");
            Assert.AreEqual("Type", propKeyString.Key, "the key should be Type");

            // check the prop value
            //PropertyValueString propValueString = prop.Value as PropertyValueString;
            ValString propValueString = prop.Value as ValString;
            Assert.IsNotNull(propValueString, "the prop key string Typeshould exists");
            Assert.AreEqual("Computer", propValueString.Value, "the value should be Computer");
        }

        /// <summary>
        /// Create an entity from a template.
        /// 
        /// Has one prop, no rule.
        /// 
        /// EntTmpl: TemplComputer
        ///     P: K=tc/Type, V=tc/Computer
        ///     
        /// Ent
        ///     P: K=tc/Type, V=tc/Computer
        /// </summary>
        /// <param name="core"></param>
        [TestMethod]
        public void EntOneProp_KTextCode_VTextCode()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            TextCode tcKeyType = core.Editor.CreateTextCode("Type");
            TextCode tcValueType = core.Editor.CreateTextCode("Computer");

            // create an entity template to instantiate
            EntityTempl templComputer = core.EditorTempl.CreateEntityTempl("TemplComputer");

            // add property
            core.EditorTempl.CreatePropTempl(templComputer, tcKeyType, tcValueType);

            //====Instanciate the template, create an entity, under the root folder
            EntityTemplToInst templToInst = core.ProcessTempl.CreateEntity(templComputer);

            // check that the execution finishes with success
            Assert.AreEqual(TemplToInstState.Success, templToInst.State, "the state should be sucess");
            Assert.AreEqual(TemplToInstStep.Ends, templToInst.NextStep, "the next step should be ends");

            // check, get the property: Type=Computer
            PropertyBase propBase = core.Searcher.FindPropertyByKey(templToInst.Entity, templToInst.Entity.PropertyRoot, "Type", false);
            Assert.IsNotNull(propBase, "the propBase Type=Computer should exists");
            Property prop = propBase as Property;
            Assert.IsNotNull(prop, "the prop Type=Computer should exists");

            //----check the prop key 
            PropertyKeyTextCode propKeyTextCode = prop.Key as PropertyKeyTextCode;
            Assert.IsNotNull(propKeyTextCode, "the prop key string Type should exists");
            Assert.AreEqual(tcKeyType.Id, propKeyTextCode.TextCodeId, "the prop value should be the textCode id of the text Name");

            //----check the prop value 
            //PropertyValueTextCode propValueTextCode = prop.Value as PropertyValueTextCode;
            ValTextCodeId propValueTextCode = prop.Value as ValTextCodeId;
            Assert.IsNotNull(propValueTextCode, "the prop key string Typeshould exists");
            Assert.AreEqual(tcValueType.Id, propValueTextCode.TextCodeId, "the prop value should be the textCode id of text Toshiba");

        }
    }
}
