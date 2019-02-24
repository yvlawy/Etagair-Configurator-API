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
        /// Create an entity from a template.
        /// Has one property: key and value are string type.
        /// 
        /// ET: TemplComputer
        ///     P: K=Type, V=Computer
        /// 
        /// After processing the template:
        /// 
        /// E:
        ///     P: K=Type, V=Computer
        ///     
        /// </summary>
        public void CreateBasicEntityTemplate()
        {
            EtagairEngine engine = CreateEngine();

            Console.WriteLine("Create an entity template.");

            //====Create the template
            // create an entity template to instantiate
            EntityTempl templComputer = engine.EditorTempl.CreateEntityTempl("TemplComputer");

            // add property to the template, Key=Type, Value=Computer
            engine.EditorTempl.CreatePropTempl(templComputer, "Type", "Computer");

            //====Instanciate the template, create an entity, under the root folder

            //--1.Start/Init create
            EntityTemplToInst templToInst = engine.ProcessTempl.StartCreateEntity(templComputer);

            Console.WriteLine("1. Starts Create entity:");
            // the state should be InProgress
            Console.WriteLine("  State: " + templToInst.State.ToString());
            // the nextStep should be: Starts
            Console.WriteLine("  NextStep: " + templToInst.NextStep.ToString());

            //--2.Create the entity
            engine.ProcessTempl.CreateEntity(templToInst);

            // the state should be Success
            Console.WriteLine("2. Create entity:");
            Console.WriteLine("  State: " + templToInst.State.ToString());
            // the nextStep should be: Ends
            Console.WriteLine("  NextStep: " + templToInst.NextStep.ToString());

            // displays the entity id
            Console.WriteLine("\n-----Created entity id: " + templToInst.Entity.Id);
            //Console.WriteLine(" Prop Count: " + templToInst.Entity.PropertyRoot.ListProperty.Count);
            DisplayEntity(engine, templToInst.Entity,0);

            // displays all object (to check)
            //DisplayAllObjects(engine, engine.Editor.GetRootFolder());
        }
    }
 }
