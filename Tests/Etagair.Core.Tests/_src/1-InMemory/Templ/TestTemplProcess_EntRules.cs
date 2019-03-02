using Etagair.Core.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.Tests.TestTempl
{
    /// <summary>
    /// Test templating processsing
    /// Entity template, only final prop, with rule.
    /// </summary>
    [TestClass]
    public class TestTemplProcess_EntRules
    {
        public void EntOneProp_KString_VString_RULToSet()
        {
            // TODO:

        }

        /// <summary>
        /// Templ Entity, has a rule the property value: will be set on entity instantiation.
        /// 
        /// EntTempl TemplComputer
        ///    P: K="Name", V=RULE:ToSet
        /// 
        /// The creation of the entity need to provide the property value text.
        /// Ent 
        ///    P: K="Name", V="Toshiba"
        ///     
        /// </summary>
        [TestMethod]
        public void EntOneProp_KeyString_ValString_RULToSet()
        {
            EtagairCore core = Common.CreateCoreInMemory();

            // create an entity template to instanciate
            EntityTempl templComputer = core.EditorTempl.CreateEntityTempl("TemplComputer");

            // create a property template without the value: will be created on the instantiation
            PropTempl propTempl = core.EditorTempl.CreatePropTemplValueStringNull(templComputer, "Name");

            // Add Rule: add property, V=RULE:Toset, type= TextCode: to be set on instanciation
            PropTemplRuleValueToSet rule = new PropTemplRuleValueToSet();
            rule.ValueType = PropValueType.String;
            core.EditorTempl.AddPropTemplRule(templComputer, propTempl, rule);

            // provide an action to the rule (to execute it automatically): Property value set on instantiation
            PropTemplRuleActionValueToSet  action = new PropTemplRuleActionValueToSet ();
            action.SetRule(rule);
            action.SetValueString("Toshiba");

            //====Instantiate the template, create an entity, under the root folder
            EntityTemplToInst templToInst = core.ProcessTempl.CreateEntity(templComputer);

            Assert.AreEqual(TemplToInstState.InProgress, templToInst.State, "the state should be InProgress");
            Assert.AreEqual(TemplToInstStep.NeedAction, templToInst.NextStep, "the next step should be NeedAction");

            // adds actions to rules and create the entity
            core.ProcessTempl.AddActionsToCreateEntity(templToInst, action);
            Assert.AreEqual(TemplToInstState.InProgress, templToInst.State, "the state should be InProgress");
            Assert.AreEqual(TemplToInstStep.Starts, templToInst.NextStep, "the next step should be Starts");

            // create the entity, use action
            core.ProcessTempl.CompleteCreateEntity(templToInst);

            // check that the execution finishes with success
            Assert.AreEqual(TemplToInstState.Success, templToInst.State, "the state should be sucess");
            Assert.AreEqual(TemplToInstStep.Ends, templToInst.NextStep, "the next step should be ends");

            //====check, get the property: Name=Toshiba
            PropertyBase propBase = core.Searcher.FindPropertyByKey(templToInst.Entity, templToInst.Entity.PropertyRoot, "Name", false);
            Assert.IsNotNull(propBase, "the propBase Type=Computer should exists");
            Property prop = propBase as Property;
            Assert.IsNotNull(prop, "the prop Type=Computer should exists");

            //----check the prop key 
            PropertyKeyString propKeyString = prop.Key as PropertyKeyString;
            Assert.IsNotNull(propKeyString, "the prop key string Type should exists");            
            Assert.AreEqual("Name", propKeyString.Key, "the prop value should be Name");

            //----check the prop value 
            PropertyValueString propValueValue = prop.Value as PropertyValueString;
            Assert.IsNotNull(propValueValue, "the prop key string Typeshould exists");
            Assert.AreEqual("Toshiba", propValueValue.Value, "the prop value should be Toshiba");
        }

        /// <summary>
        /// Templ Entity, has a rule the property value: will be set on entity instantiation.
        /// 
        /// EntTempl TemplComputer
        ///    P: K=tc/"Name", V=RULE:tc/ToSet
        /// 
        /// The creation of the entity need to provide the property value text.
        /// Ent 
        ///    P: K=tc/"Name", V=tc/"Toshiba"
        ///     
        /// </summary>
        [TestMethod]
        public void EntOneProp_KeyTextCode_ValTextCode_RULToSet()
        {
            EtagairCore core = Common.CreateCoreInMemory();

            TextCode tcKeyName = core.Editor.CreateTextCode("Name");

            // create an entity template to instanciate
            EntityTempl templComputer = core.EditorTempl.CreateEntityTempl("TemplComputer");

            // create a property template without the value: will be created on the instantiation
            PropTempl propTempl = core.EditorTempl.CreatePropTemplValueTextCodeNull(templComputer, tcKeyName);


            // Add Rule: add property, V=RULE:Toset, type= TextCode: to be set on instanciation
            PropTemplRuleValueToSet rule = new PropTemplRuleValueToSet();
            rule.ValueType = PropValueType.TextCode;
            core.EditorTempl.AddPropTemplRule(templComputer, propTempl, rule);

            // provide an action to the rule (to execute it automatically): Property value set on instantiation
            TextCode tcNameValToshiba = core.Editor.CreateTextCode("Toshiba");
            PropTemplRuleActionValueToSet  action = new PropTemplRuleActionValueToSet ();
            action.SetRule(rule);
            action.SetValueTextCodeId(tcNameValToshiba.Id);

            //====Instantiate the template, create an entity, under the root folder
            EntityTemplToInst templToInst = core.ProcessTempl.CreateEntity(templComputer);

            Assert.AreEqual(TemplToInstState.InProgress, templToInst.State, "the state should be InProgress");
            Assert.AreEqual(TemplToInstStep.NeedAction, templToInst.NextStep, "the next step should be NeedAction");

            // adds actions to rules and create the entity
            core.ProcessTempl.AddActionsToCreateEntity(templToInst, action);
            Assert.AreEqual(TemplToInstState.InProgress, templToInst.State, "the state should be InProgress");
            Assert.AreEqual(TemplToInstStep.Starts, templToInst.NextStep, "the next step should be Starts");

            // create the entity, use action
            core.ProcessTempl.CompleteCreateEntity(templToInst);

            // check that the execution finishes with success
            Assert.AreEqual(TemplToInstState.Success, templToInst.State, "the state should be sucess");
            Assert.AreEqual(TemplToInstStep.Ends, templToInst.NextStep, "the next step should be ends");

            //====check, get the property: Name=Toshiba
            PropertyBase propBase = core.Searcher.FindPropertyByKey(templToInst.Entity, templToInst.Entity.PropertyRoot, "Name", false);
            Assert.IsNotNull(propBase, "the propBase Type=Computer should exists");
            Property prop = propBase as Property;
            Assert.IsNotNull(prop, "the prop Type=Computer should exists");

            //----check the prop key 
            PropertyKeyTextCode propKeyTextCode = prop.Key as PropertyKeyTextCode;
            Assert.IsNotNull(propKeyTextCode, "the prop key string Type should exists");
            Assert.AreEqual(tcKeyName.Id, propKeyTextCode.TextCodeId, "the prop value should be the textCode id of the text Name");

            //----check the prop value 
            PropertyValueTextCode propValueTextCode = prop.Value as PropertyValueTextCode;
            Assert.IsNotNull(propValueTextCode, "the prop key string Typeshould exists");
            Assert.AreEqual(tcNameValToshiba.Id, propValueTextCode.TextCodeId, "the prop value should be the textCode id of text Toshiba");
        }

    }
}