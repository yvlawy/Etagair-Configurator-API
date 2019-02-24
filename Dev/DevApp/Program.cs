using Etagair.Core;
using Etagair.Core.Reposit.Contract;
using Etagair.Core.Reposit.InMemory;
using Etagair.Core.Reposit.LiteDB;
using Etagair.Core.System;
using Etagair.Engine;
using System;
using System.Collections.Generic;
using System.IO;

namespace DevApp
{
    /// <summary>
    /// To develop the Etagair application.
    /// </summary>
    class Program
    {
        static IEtagairReposit CreateRepository_InMemory()
        {
            IEtagairReposit reposit = new EtagairReposit_InMemory();
            return reposit;
        }

        static IEtagairReposit CreateRepository_LiteDB(bool removePreviousDBFile)
        {
            string stringConnection = @".\Data\litedb.db";

            // remove the previous one if exists
            if(removePreviousDBFile)
            {
                if (File.Exists(stringConnection))
                    File.Delete(stringConnection);
            }

            IEtagairReposit reposit = new EtagairReposit_LiteDB(stringConnection);
            return reposit;
        }

        static EtagairCore CreateCore(IEtagairReposit reposit)
        {
            // create the core configurator, inject the concrete repository
            EtagairCore core = new EtagairCore();


            //----init the core: create the catalog, becomes the current catalog
            if (!core.Init(reposit))
            {
                Console.WriteLine("Error, Init failed, can't create the catalog, already exists.");
                return null;
            }

            // configure the catalog

            return core;
        }

        /// <summary>
        /// Dev the language and text data model.
        /// Ent:
        ///     tc:Name= tc:NameToshiba
        /// </summary>
        /// <param name="core"></param>
        static void CreateLangAndTexts(EtagairCore core)
        {
            //----set defined (activate) language codes in the current catalog
            core.Editor.DefineLanguage(LanguageCode.en);
            core.Editor.DefineLanguage(LanguageCode.en_GB); 
            core.Editor.DefineLanguage(LanguageCode.en_US);
            core.Editor.DefineLanguage(LanguageCode.fr);
            core.Editor.DefineLanguage(LanguageCode.fr_FR);


            // create localized text for main languages managed in the application
            TextCode tcName = core.Editor.CreateTextCode("Name");
            core.Editor.CreateTextLocalModel(LanguageCode.en, tcName, "Name");
            core.Editor.CreateTextLocalModel(LanguageCode.fr, tcName, "Nom");

            TextCode tcNameToshiba = core.Editor.CreateTextCode("NameToshiba");
            core.Editor.CreateTextLocalModel(LanguageCode.en, tcNameToshiba, "Laptop Toshiba Core i7 RAM 8Go HD 1To Win10");
            core.Editor.CreateTextLocalModel(LanguageCode.fr, tcNameToshiba, "Ordinateur portable Toshiba Core i7 RAM 8Go DD 1To Win10");

            // set the current language, get the localized/translated text
            core.Searcher.SetCurrentLanguage(LanguageCode.en_GB);

            // define a second current language
            //core.Editor.SetCurrentLang2(LanguageCode.en_GB);

            // create/generate the localized/translated text of a textCode
            // the en_GB language doesn't exists so return the main lang (en for en_GB) textLocal: 'Name'
            TextLocal tlName = core.Editor.GenerateTextLocal(tcName);

            TextLocal tlNameAgain = core.Editor.GenerateTextLocal(tcName, LanguageCode.en);

            
            // todo: gestion des entités avec textes (sur les textCode) dans la langue courante
            // EntityLoc    pour entity Localized? EntityLzd

        }


        /// <summary>
        /// entity with property:
        /// TextCode key
        /// Property group
        /// </summary>
        /// <param name="core"></param>
        static void BuildContent(EtagairCore core)
        {
            //----set defined (activate) language codes in the current catalog
            core.Editor.DefineLanguage(LanguageCode.en); 
            core.Editor.DefineLanguage(LanguageCode.en_GB); 
            core.Editor.DefineLanguage(LanguageCode.en_US);
            core.Editor.DefineLanguage(LanguageCode.fr);
            core.Editor.DefineLanguage(LanguageCode.fr_FR); 

            TextCode tcName = core.Editor.CreateTextCode("Name");

            // should failed
            TextCode tcNameErr = core.Editor.CreateTextCode("Name");

            // create localized text for main languages managed in the application
            // en -> en_uk, en_us,....
            core.Editor.CreateTextLocalModel(LanguageCode.en, tcName, "Name"); 
            core.Editor.CreateTextLocalModel(LanguageCode.fr, tcName, "Nom");

            TextCode tcNameToshiba = core.Editor.CreateTextCode("NameToshiba");
            core.Editor.CreateTextLocalModel(LanguageCode.en, tcNameToshiba, "Laptop Toshiba Core i7 RAM 8Go HD 1To Win10");
            core.Editor.CreateTextLocalModel(LanguageCode.fr, tcNameToshiba, "Ordinateur portable Toshiba Core i7 RAM 8Go DD 1To Win10");

            //---- create a folder, under the root            
            Folder foldComputers = core.Editor.CreateFolder(null, "computers");

            //----create entities
            // create a computer object from scratch (not from a template), under the "computers" folder
            //Object toshibaCoreI7= core.Editor.CreateObject("Toshiba Core i7 RAM 8Go DD 1To Win10")	<- pas de nom de var non? (par propriété nom)
            Entity toshibaCoreI7 = core.Editor.CreateEntity(foldComputers);

            // Add a property to an object: key - value, both are textCode (will be displayed translated depending on the language)
            core.Editor.CreateProperty(toshibaCoreI7, tcName, tcNameToshiba);

            // --test: can't create a property with an existing name/key, in the same group properties
            core.Editor.CreateProperty(toshibaCoreI7, tcName, tcNameToshiba);

            // create property: RAM: 8 Go(Gb)
            core.Editor.CreateProperty(toshibaCoreI7, "RAM", "8");

            // should failed
            core.Editor.CreateProperty(toshibaCoreI7, "RAM", "8");

            // add a properties group (folder inside object) in the object
            PropertyGroup propGrpProc = core.Editor.CreatePropertyGroup(toshibaCoreI7, "Processor");
            PropertyGroup propGrpProc2= core.Editor.CreatePropertyGroup(toshibaCoreI7, "Processor");

            core.Editor.CreateProperty(toshibaCoreI7, propGrpProc, "Constructor", "Intel");
            core.Editor.CreateProperty(toshibaCoreI7, propGrpProc, "Model", "i7");

            // Create a prop: string, imageCode
        }

        // TODO: gérer trad finale absente -> selectionne la mainLang
        // exp: pas de texte en fr_FR mais existe en fr, prend ce dernier.

        /// <summary>
        ///  select entities having a property key='Name'
        ///  
        ///     computers\
        ///         Ent: Name=Toshiba   Ok, selected
        ///         Ent: Name=Dell      Ok, selected
        ///         Ent: Company=HP     --NOT selected
        ///     
        /// </summary>
        /// <param name="core"></param>
        static void TestSearchEntity(EtagairCore core)
        {
            Console.WriteLine("====>TestSearchEntity: ");

            //---- create a folder, under the root
            Folder foldComputers = core.Editor.CreateFolder(null, "computers");

            //==== creates entities, with prop

            //----create entity
            Entity toshibaCoreI7 = core.Editor.CreateEntity(foldComputers);
            core.Editor.CreateProperty(toshibaCoreI7, "Name", "Toshiba Satellite Core I7");
            core.Editor.CreateProperty(toshibaCoreI7, "TradeMark", "Toshiba");

            //----create entity
            Entity dellCoreI7 = core.Editor.CreateEntity(foldComputers);
            core.Editor.CreateProperty(dellCoreI7, "Name", "Dell Inspiron 15-5570-sku6");
            core.Editor.CreateProperty(dellCoreI7, "TradeMark", "Dell");

            //----create entity
            Entity HPCoreI7 = core.Editor.CreateEntity(foldComputers);
            core.Editor.CreateProperty(HPCoreI7, "Company", "HP Core i7");
            core.Editor.CreateProperty(HPCoreI7, "TradeMark", "HP");

            //==== define the search: from the root folder, select all entities having a key prop called 'Name'
            SearchEntity searchEntities = core.Searcher.CreateSearchEntity(SearchFolderScope.Defined);

            //--Add sources folders, set option: go inside folders childs
            core.Searcher.AddSourceFolder(searchEntities, foldComputers, true);

            //--Add criteria: (a boolean expression)
            SearchPropCriterionKeyText criterion = core.Searcher.AddCritPropKeyText(searchEntities, "Name");
            criterion.PropKeyTextType = CritOptionPropKeyTextType.AllKeyType;
            criterion.TextMatch = CritOptionTextMatch.TextMatchExact;

            //--set options
            // TODO: lesquelles?

            //==== execute the search, get the result: list of found entities
            SearchEntityResult result= core.Searcher.ExecSearchEntity(searchEntities);

            // displays found entities
            Console.WriteLine("-Search result: nb=" + result.ListEntityId.Count);
            foreach (string entityId in result.ListEntityId)
            {
                // load the entity

                Console.WriteLine("Ent, id: " + entityId);
            }
        }


        // ==== Create entity with prop childs!! 
        // group

        // Templ Entity, CreatePropertyGroupTempl( key)
        // Add properties childs
        // set rule for childs: One, Several, 0..N,...

        static void TestCore()
        {
            //IEtagairReposit reposit= CreateRepository_InMemory();
            IEtagairReposit reposit = CreateRepository_LiteDB(true);

            EtagairCore core = CreateCore(reposit);

            // Dev the language and text data model.
            CreateLangAndTexts(core);

            //BuildContent(core);

            //TestSearchEntity(core);
        }

        /// <summary>
        /// Use to develop new functionnalities.
        /// See th SamplesApp console application to have stable code samples.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("====Etagair dev:");

            TestCore();

            Console.WriteLine("Input a key ...");
            Console.ReadKey();
        }
    }
}
