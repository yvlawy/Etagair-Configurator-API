using Etagair.Core.System;
using Etagair.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SamplesApp
{
    public class CreateEntityFolder:Common
    {

        /// <summary>
        /// Very basic sample: create a folder and an entity with properties (key-value).
        /// 
        ///  F:computers\
        ///    E: 
        ///     "Name"= "Toshiba Satellite Core I7"
        ///     "Trademark"= "Toshiba"
        ///     
        /// </summary>
        public void CreateFolderWithinEntity()
        {
            EtagairEngine engine = CreateEngine();

            Console.WriteLine("Create a folder within an entity.");
            Console.WriteLine("F: computers\\");
            Console.WriteLine("  E: ");
            Console.WriteLine("  \"Name\"= \"Toshiba Satellite Core I7\"");
            Console.WriteLine("  \"Trademark\" = \"Toshiba\"");
            Console.WriteLine("  \"Year\" = 2019");

            // create a folder, under the root
            Folder foldComputers = engine.Editor.CreateFolder(null, "computers");

            // create an entity, under the computers folder
            Entity toshibaCoreI7 = engine.Editor.CreateEntity(foldComputers);

            // Add 2 properties to the entity (key - value)
            engine.Editor.CreateProperty(toshibaCoreI7, "Name", "Toshiba Satellite Core I7");
            engine.Editor.CreateProperty(toshibaCoreI7, "Trademark", "Toshiba");
            //engine.Editor.CreateProperty(toshibaCoreI7, "Year", 2019);
        }

        /// <summary>
        ///  Search entities by the key property.
        ///  Create 3 entities, select entities having a property key equals to 'Name'.
        ///  
        ///     computers\
        ///         Ent: Name=Toshiba   Ok, selected
        ///         Ent: Name=Dell      Ok, selected
        ///         Ent: Company=HP     --NOT selected
        /// 
        /// -------output:
        /// Db initialized with success.
        /// -Search result: nb= 2
        /// Ent, id: 5c6822abdd125603e885d8d4
        /// Ent, id: 5c6822c1dd125603e885d8d6
        /// </summary>
        public void CreateThreeEntitiesSearchPropKeyName()
        {
            EtagairEngine engine = CreateEngine();

            //---- create a folder, under the root
            Folder foldComputers = engine.Editor.CreateFolder(null, "computers");

            //==== creates entities, with prop

            //----create entity
            Entity toshibaCoreI7 = engine.Editor.CreateEntity(foldComputers);
            engine.Editor.CreateProperty(toshibaCoreI7, "Name", "Toshiba Satellite Core I7");
            engine.Editor.CreateProperty(toshibaCoreI7, "TradeMark", "Toshiba");

            //----create entity
            Entity dellCoreI7 = engine.Editor.CreateEntity(foldComputers);
            engine.Editor.CreateProperty(dellCoreI7, "Name", "Dell Inspiron 15-5570-sku6");
            engine.Editor.CreateProperty(dellCoreI7, "TradeMark", "Dell");

            //----create entity
            Entity HPCoreI7 = engine.Editor.CreateEntity(foldComputers);
            engine.Editor.CreateProperty(HPCoreI7, "Company", "HP Core i7");
            engine.Editor.CreateProperty(HPCoreI7, "TradeMark", "HP");

            //==== define the search: from the root folder, select all entities having a key prop called 'Name'
            SearchEntity searchEntities = engine.Searcher.CreateSearchEntity("EntitiesHavingPropName", SearchFolderScope.Defined);

            //--Add sources folders, set option: go inside folders childs
            // (only one for now is managed!!)
            engine.Searcher.AddSourceFolder(searchEntities, foldComputers, true);

            //--Add criteria: (a boolean expression)
            SearchPropCriterionKeyText criterion = engine.Searcher.AddCritPropKeyText(searchEntities, "Name");
            criterion.TextMatch = CritOptionTextMatch.TextMatchExact;
            criterion.PropKeyTextType = CritOptionPropKeyTextType.AllKeyType;

            //==== execute the search, get the result: list of found entities
            SearchEntityResult result = engine.Searcher.ExecSearchEntity(searchEntities);

            // displays found entities
            Console.WriteLine("-Search result: nb=" + result.ListEntityId.Count);
            foreach (string entityId in result.ListEntityId)
            {
                // displays the id of the entity
                Console.WriteLine("Ent, id: " + entityId);

                // load the entity and display it
                Entity entity = engine.Searcher.FindEntityById(entityId);
                // display the properties of the entity....
            }
        }

        /// <summary>
        /// Create an entity with a property group.
        /// 
        /// Ent:
        ///   P: tc:tcName= tc:tcNameToshiba
        ///   P: "RAM"= "8"
        ///   PG: "Processor"\
        ///         P: "Constructor"= "Intel"
        ///         P: "Model"= "i7"
        /// </summary>
        public void CreateEntityWithPropGroup()
        {
            EtagairEngine engine = CreateEngine();


            //----set defined (activate) language codes in the application
            engine.Editor.DefineLanguage(LanguageCode.en);
            engine.Editor.DefineLanguage(LanguageCode.fr);

            // create localized text for main languages managed in the application
            TextCode tcName = engine.Editor.CreateTextCode("Name");
            engine.Editor.CreateTextLocalModel(LanguageCode.en, tcName, "Name");
            engine.Editor.CreateTextLocalModel(LanguageCode.fr, tcName, "Nom");

            TextCode tcValueToshiba = engine.Editor.CreateTextCode("ValueToshiba");
            engine.Editor.CreateTextLocalModel(LanguageCode.en, tcValueToshiba, "Laptop Toshiba Core i7 RAM 8Go HD 1To Win10");
            engine.Editor.CreateTextLocalModel(LanguageCode.fr, tcValueToshiba, "Ordinateur portable Toshiba Core i7 RAM 8Go DD 1To Win10");

            // create an entity with one property: key and value are TextCode
            Entity toshibaCoreI7 = engine.Editor.CreateEntity();
            // Add a property to an object: key - value, both are textCode (will be displayed translated depending on the language)
            engine.Editor.CreateProperty(toshibaCoreI7, tcName, tcValueToshiba);

            // create property: RAM: 8 Go(Gb)
            engine.Editor.CreateProperty(toshibaCoreI7, "RAM", "8");

            // add a properties group (folder inside object) in the object
            PropertyGroup propGrpProc = engine.Editor.CreatePropertyGroup(toshibaCoreI7, "Processor");

            engine.Editor.CreateProperty(toshibaCoreI7, propGrpProc, "Constructor", "Intel");
            engine.Editor.CreateProperty(toshibaCoreI7, propGrpProc, "Model", "i7");
        }


        // display the properties of an entity (key is string, TextCode)

    }
}
