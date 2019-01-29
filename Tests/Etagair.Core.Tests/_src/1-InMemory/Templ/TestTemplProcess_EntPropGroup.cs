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
    /// Entity template, with prop group, no rule.
    /// </summary>
    [TestClass]
    public class TestTemplProcess_EntPropGroup
    {
        /// <summary>
        /// one prop group having one prop, no rule.
        /// 
        /// EntTempl TemplComputer
        ///     PGrp: K="Core"
        ///         P: K="Type", V="Intel"
        ///     
        /// Ent 
        ///     PGrp: K="Core"
        ///         P: K="Type", V="Intel"
        /// 
        /// TestTemplProcess_EntPropGroup
        /// </summary>
        [TestMethod]
        public void Ent_PropGroup_Prop_KString_VString()
        {
            EtagairCore core = Common.CreateCoreInMemory();

            // create an entity template to instantiate
            EntityTempl templComputer = core.EditorTempl.CreateEntityTempl("TemplComputer");

            // create the group Core
            PropGroupTempl propGroupTemplCore = core.EditorTempl.CreatePropGroupTempl(templComputer, "Core");

            // under the propGroup Core, create the prop Type=Intel
            PropTempl propTemplType = core.EditorTempl.CreatePropTempl(templComputer, propGroupTemplCore, "Type", "Intel");

            //====Instantiate
            EntityTemplToInst templToInst = core.ProcessTempl.StartCreateEntity(templComputer);

            Assert.AreEqual(TemplToInstState.InProgress, templToInst.State, "the state should be InProgress");
            Assert.AreEqual(TemplToInstStep.Starts, templToInst.NextStep, "the next step should be Starts");

            // create the entity, use action
            core.ProcessTempl.CreateEntity(templToInst);

            // check that the execution finishes with success
            Assert.AreEqual(TemplToInstState.Success, templToInst.State, "the state should be sucess");
            Assert.AreEqual(TemplToInstStep.Ends, templToInst.NextStep, "the next step should be ends");

            //=====Check the creation

            //----check the prop group: Core
            PropertyBase propBase = core.Searcher.FindPropertyByKey(templToInst.Entity, templToInst.Entity.PropertyRoot, "Core", false);
            PropertyGroup propGroup = propBase as PropertyGroup;
            Assert.IsNotNull(propGroup, "the propgroup Core should exists");

            // Check the prop group key
            PropertyKeyString propGroupKey = propGroup.Key as PropertyKeyString;
            Assert.IsNotNull(propGroupKey, "the propgroup key Core should exists");
            Assert.AreEqual("Core", propGroupKey.Key, "the key should be Core");

            //----check the property child: Type=Computer
            propBase = core.Searcher.FindPropertyByKey(templToInst.Entity, propGroup, "Type", false);
            Property prop = propBase as Property;
            Assert.IsNotNull(prop, "the prop child Type should exists");

            // find the prop Intel (inside the group Core) from the root property
            PropertyBase propBaseTypeIntel = core.Searcher.FindPropertyByKey(templToInst.Entity, "Type", true);
            Property propTypeIntel = propBaseTypeIntel as Property;
            Assert.IsNotNull(propTypeIntel, "the prop child Type should exists (find from the root)");

            // check the prop key: Type, find the prop from the parent
            PropertyKeyString propKeyString = prop.Key as PropertyKeyString;
            Assert.IsNotNull(propKeyString, "the prop key string Type should exists");
            Assert.AreEqual("Type", propKeyString.Key, "the key should be Type");

            // check the prop value
            PropertyValueString propValueString = prop.Value as PropertyValueString;
            Assert.IsNotNull(propValueString, "the prop key string Type should exists");
            Assert.AreEqual("Intel", propValueString.Value, "the value should be Intel");
        }
    }
}
