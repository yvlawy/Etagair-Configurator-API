using Etagair.Core.System;
using Etagair.Engine;
using System;
using System.IO;

namespace DevApp
{
    /// <summary>
    /// Samples to put into doc/wiki,...
    /// 
    /// </summary>
    public class DevEtagairEngine
    {
        /// <summary>
        /// Test, execute one method.
        /// </summary>
        public void Run()
        {
            //CreateFolderWithinEntity();
            //CreateThreeEntitiesSearchPropKeyName();
            //ManageLanguagesAndLocalizedText();

            CreateEntityWithPropGroup();
        }

        #region Private methods

        private EtagairEngine CreateEngine()
        {
            EtagairEngine engine = new EtagairEngine();

            // the path must exists, location where to put the database file
            string dbPath = @".\Data\";

            // delete the previous instance of the db
            if (File.Exists(dbPath + "etagair.db"))
                File.Delete(dbPath + "etagair.db");

            // create the database or reuse the existing one
            if (!engine.Init(dbPath))
            {
                Console.WriteLine("Db initialization Failed.");
                return null;
            }

            // the database is created or reused and opened, ready to the execution
            Console.WriteLine("Db initialized with success.");
            return engine;
        }

        /// <summary>
        /// Very basic sample: create a folder and an entity with properties (key-value).
        /// 
        ///  F:computers\
        ///    E: 
        ///     "Name"= "Toshiba Satellite Core I7"
        ///     "Trademark"= "Toshiba"
        ///     
        /// </summary>
        private void CreateFolderWithinEntity()
        {
            EtagairEngine engine = CreateEngine();

            // create a folder, under the root
            Folder foldComputers = engine.Editor.CreateFolder(null, "computers");

            // create an entity, under the computers folder
            Entity toshibaCoreI7 = engine.Editor.CreateEntity(foldComputers);

            // Add 2 properties to the entity (key - value)
            engine.Editor.CreateProperty(toshibaCoreI7, "Name", "Toshiba Satellite Core I7");
            engine.Editor.CreateProperty(toshibaCoreI7, "Trademark", "Toshiba");
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
        private void CreateThreeEntitiesSearchPropKeyName()
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
        /// Dev the language and text data model.
        /// E:
        ///   P: key=TC:tcName= Value=TC:tcNameToshiba
        /// </summary>
        private void ManageLanguagesAndLocalizedText()
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

            // set the current language, get the localized/translated text
            engine.Searcher.SetCurrentLanguage(LanguageCode.en);

            // create/generate the localized/translated text of a textCode in the current language
            TextLocal tlName = engine.Editor.GenerateTextLocal(tcName);

            // Output: Name in current language (en): Name
            Console.WriteLine("Name in current language (en): " + tlName.Text);

            // create/generate the localized/translated text of a textCode in the specified language
            TextLocal tlNameFr = engine.Editor.GenerateTextLocal(tcName, LanguageCode.fr);

            // Output: Name in fr language: Nom
            Console.WriteLine("Name in fr language: " + tlNameFr.Text);
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
        private void CreateEntityWithPropGroup()
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

        // search entity by a property key (prop key is a TextCode!)

        // languages: Mapping on main fr <- fr_FR,...

        #endregion
    }
    }
