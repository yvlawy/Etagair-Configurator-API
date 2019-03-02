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
            DisplayEntity(engine, templToInst.Entity,0, true);
        }
    }
 }
