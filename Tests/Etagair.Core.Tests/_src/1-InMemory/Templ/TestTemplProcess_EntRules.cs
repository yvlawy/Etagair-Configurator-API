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
        /// Templ Entity, CreatePropertyTempl( key, Value to set on instanciation)
        /// 
        /// EntTempl TemplComputer
        ///    P: K="Name", V=RULE:ToSet
        ///     
        /// Ent 
        ///    P: K="Name", V="Toshiba"
        ///     
        /// </summary>
        [TestMethod]
        public void EntOneProp_KTextCode_VTextCode_RULToSet()
        {
            EtagairCore core = Common.CreateCoreInMemory();

            TextCode tcKeyName = core.Editor.CreateTextCode("Name");

            // create an entity template to instanciate
            EntityTempl templComputer = core.EditorTempl.CreateEntityTempl("TemplComputer");

            // create a property template without the value: will be created on the instantiation
            PropTempl propTempl = core.EditorTempl.CreatePropTempl(templComputer, tcKeyName, (TextCode)null);

            // Add Rule: add property, V=RULE:Toset, type= TextCode: to be set on instanciation
            PropTemplRuleValueSetOnInst rule = new PropTemplRuleValueSetOnInst();
            rule.ValueType = PropValueType.TextCode;
            core.EditorTempl.AddPropTemplRule(templComputer, propTempl, rule);

            // provide an action to the rule (to execute it automatically): Property value set on instantiation
            TextCode tcNameValToshiba = core.Editor.CreateTextCode("Toshiba");
            PropTemplRuleValueSetOnInstAction action = new PropTemplRuleValueSetOnInstAction();
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