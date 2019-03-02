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
    /// Entity template, with prop group, with rule.
    /// </summary>
    [TestClass]
    public class TestTemplProcess_EntPropGroupRules
    {
        /// <summary>
        /// one prop group having one prop, one rule.
        /// 
        /// EntTempl TemplComputer
        ///     PGrp: K="Core"
        ///         + P: K="Type", V=RULE:ToSet
        ///     
        /// Ent 
        ///     PGrp: K="Core"
        ///         + P: K="Type", V="Intel"
        ///         
        /// </summary>
        [TestMethod]
        public void Ent_PropGroup_Prop_KString_VString_RULToSet()
        {
            EtagairCore core = Common.CreateCoreInMemory();

            // create an entity template to instantiate
            EntityTempl entTemplComputer = core.EditorTempl.CreateEntityTempl("TemplComputer");

            // create the group Core
            PropGroupTempl propGroupTemplCore = core.EditorTempl.CreatePropGroupTempl(entTemplComputer, "Core");

            // create a property template without the value: will be created on the instantiation
            PropTempl propTemplType = core.EditorTempl.CreatePropTempl(entTemplComputer, propGroupTemplCore, "Type", null);

            // On prop type, Add Rule: add property, V=RULE:ToSet, type= string
            PropTemplRuleValueSetOnInst rule = new PropTemplRuleValueSetOnInst();
            rule.ValueType = PropValueType.String;
            core.EditorTempl.AddPropTemplRule(entTemplComputer, propTemplType, rule);

            //====Instantiate
            EntityTemplToInst templToInst = core.ProcessTempl.CreateEntity(entTemplComputer);

            Assert.AreEqual(TemplToInstState.InProgress, templToInst.State, "the state should be InProgress");
            Assert.AreEqual(TemplToInstStep.NeedAction, templToInst.NextStep, "the next step should be NeedAction");

            //---provide an action to the rule (to execute it automatically): Property value set on instantiation
            PropTemplRuleValueSetOnInstAction action = new PropTemplRuleValueSetOnInstAction();
            action.SetRule(rule);
            action.SetValueString("Intel");

            // adds actions to rules and create the entity
            core.ProcessTempl.AddActionsToCreateEntity(templToInst, action);

            Assert.AreEqual(TemplToInstState.InProgress, templToInst.State, "the state should be InProgress");
            Assert.AreEqual(TemplToInstStep.Starts, templToInst.NextStep, "the next step should be NeedAction");

            // create the entity, use action
            core.ProcessTempl.CompleteCreateEntity(templToInst);

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
