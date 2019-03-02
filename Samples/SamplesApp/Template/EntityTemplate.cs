using Etagair.Core.System;
using Etagair.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace SamplesApp
{
    public class EntityTemplate : Common
    {
        /// <summary>
        /// Create an entity from a template (basic case).
        /// Has one property: key and value are string type.
        /// 
        /// ET: "TemplComputer"
        ///     P: K=Type, V=Computer
        /// 
        /// After processing the template:
        /// 
        /// E:
        ///     P: K=Type, V=Computer
        ///     
        /// </summary>
        public void BasicEntityTemplate()
        {
            EtagairEngine engine = CreateEngine();

            Console.WriteLine("Create an entity template.");

            //====Create an entity template
            EntityTempl templComputer = engine.EditorTempl.CreateEntityTempl("TemplComputer");

            // add property to the template, Key=Type, Value=Computer
            engine.EditorTempl.CreatePropTempl(templComputer, "Type", "Computer");

            //====Instanciate the template, create an entity, under the root folder
            EntityTemplToInst templToInst = engine.ProcessTempl.CreateEntity(templComputer);

            // the state should be Success
            Console.WriteLine("Create entity:");
            Console.WriteLine("  State: " + templToInst.State.ToString());
            // the nextStep should be: Ends
            Console.WriteLine("  NextStep: " + templToInst.NextStep.ToString());

            // displays the entity id
            Console.WriteLine("\n-----Created entity id: " + templToInst.Entity.Id);
            DisplayEntity(engine, templToInst.Entity,0, false);
        }

        /// <summary>
        /// Create an entity from a template (basic case).
        /// Has one property: key and value are string type.
        /// 
        /// ET: "TemplComputer"
        ///     P: K=Type, V=Computer
        /// 
        /// After processing the template:
        /// 
        /// E:
        ///     P: K=Type, V=Computer
        ///     
        /// </summary>
        public void EntityTemplate_PropKeyAndValue_TextCode()
        {
            EtagairEngine engine = CreateEngine();

            Console.WriteLine("Create an entity template.");

            //====Create an entity template
            EntityTempl templComputer = engine.EditorTempl.CreateEntityTempl("TemplComputer");

            TextCode tcKeyType = engine.Editor.CreateTextCode("Type");
            TextCode tcValueType = engine.Editor.CreateTextCode("Computer");

            // add property to the template, Key=Type, Value=Computer
            engine.EditorTempl.CreatePropTempl(templComputer, tcKeyType, tcValueType);

            //====Instanciate the template, create an entity, under the root folder
            EntityTemplToInst templToInst = engine.ProcessTempl.CreateEntity(templComputer);

            // the state should be Success
            Console.WriteLine("Create entity:");
            Console.WriteLine("  State: " + templToInst.State.ToString());
            // the nextStep should be: Ends
            Console.WriteLine("  NextStep: " + templToInst.NextStep.ToString());

            // displays the entity id
            Console.WriteLine("\n-----Created entity id: " + templToInst.Entity.Id);
            DisplayEntity(engine, templToInst.Entity, 0, false);
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
        public void EntityTemplate_Rule_PropValToSet()
        {
            EtagairEngine engine = CreateEngine();

            Console.WriteLine("Create an entity template with a rule.");


            // create an entity template to instanciate
            EntityTempl templComputer = engine.EditorTempl.CreateEntityTempl("TemplComputer");

            // create a property template without the value: will be created on the instantiation
            PropTempl propTempl = engine.EditorTempl.CreatePropTemplValueStringNull(templComputer, "Name");

            // Add Rule: add property, V=RULE:Toset, type= TextCode: to be set on instanciation
            PropTemplRuleValueToSet rule = new PropTemplRuleValueToSet();
            rule.ValueType = PropValueType.String;
            engine.EditorTempl.AddPropTemplRule(templComputer, propTempl, rule);

            //====Instantiate the template, create an entity, under the root folder
            EntityTemplToInst templToInst = engine.ProcessTempl.CreateEntity(templComputer);

            // the state should be InProgress/NeedAction
            Console.WriteLine("\n1. Create entity:");
            Console.WriteLine("  State: " + templToInst.State.ToString());
            Console.WriteLine("  NextStep: " + templToInst.NextStep.ToString());

            // provide an action to the rule (to execute it automatically): Property value set on instantiation
            PropTemplRuleActionValueToSet action = new PropTemplRuleActionValueToSet();
            action.SetRule(rule);
            action.SetValueString("Toshiba");

            // adds actions to rules and create the entity
            engine.ProcessTempl.AddActionsToCreateEntity(templToInst, action);

            // the state should be InProgress/NeedAction
            Console.WriteLine("\n2. Add Action to the rule: PropValue SetTo='Toshiba'");
            Console.WriteLine("  State: " + templToInst.State.ToString());
            // the nextStep should be: Ends
            Console.WriteLine("  NextStep: " + templToInst.NextStep.ToString());

            // create the entity, use action
            engine.ProcessTempl.CompleteCreateEntity(templToInst);

            // the state should be InProgress/NeedAction
            Console.WriteLine("\n3. Complete the creation:");
            Console.WriteLine("  State: " + templToInst.State.ToString());
            // the nextStep should be: Ends
            Console.WriteLine("  NextStep: " + templToInst.NextStep.ToString());

            // displays the entity id
            Console.WriteLine("\n-----Created entity id: " + templToInst.Entity.Id);
            DisplayEntity(engine, templToInst.Entity, 0, false);
        }
    }
}
