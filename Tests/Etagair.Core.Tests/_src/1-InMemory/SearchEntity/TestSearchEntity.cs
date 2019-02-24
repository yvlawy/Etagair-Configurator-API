using Etagair.Core.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.Tests
{
    /// <summary>
    /// Search entity on a basic criterion, test the scope.
    /// </summary>
    [TestClass]
    public class TestSearchEntity
    {
        public string RepositConfig = "InMemory";

        [TestInitialize]
        public void Init()
        {

        }

        /// <summary>
        ///  select entities having a property key='Name'
        ///  Search in the root folder only, not in subfolder.
        ///  
        /// -Data:
        ///  Ent: Name=Toshiba   Ok, selected
        ///  
        ///  Fold: computers\
        ///    Ent: Name=Dell      Ok, seleted
        ///     
        /// </summary>
        [TestMethod]
        public void SearchEntities_All_OnePropKeyEqualsName_OneInFolder()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //==== creates entities, with prop
            //----create entities
            Entity toshibaCoreI7 = core.Editor.CreateEntity();
            core.Editor.CreateProperty(toshibaCoreI7, "Name", "Toshiba Satellite Core I7");
            core.Editor.CreateProperty(toshibaCoreI7, "TradeMark", "Toshiba");

            // create a folder with an entity inside it
            Folder foldComputers = core.Editor.CreateFolder(null, "computers");

            Entity dellCoreI7 = core.Editor.CreateEntity();
            core.Editor.CreateProperty(dellCoreI7, "Name", "Dell Inspiron 15-5570-sku6");
            core.Editor.CreateProperty(dellCoreI7, "TradeMark", "Dell");

            //==== define the search: from the root folder, select all entities having a key prop called 'Name'
            SearchEntity searchEntities = core.Searcher.CreateSearchEntity(SearchFolderScope.All);

            //--Add single criteria: property key text equals to 'Name'
            SearchPropCriterionKeyText criterion = core.Searcher.AddCritPropKeyText(searchEntities, "Name");
            criterion.TextMatch = CritOptionTextMatch.TextMatchExact;
            criterion.PropKeyTextType = CritOptionPropKeyTextType.AllKeyType;

            //==== execute the search, get the result: list of found entities
            SearchEntityResult result = core.Searcher.ExecSearchEntity(searchEntities);

            // check found entities
            Assert.AreEqual(2, result.ListEntityId.Count, "Two found entities expected");

            bool found;
            found = result.ListEntityId.Contains(toshibaCoreI7.Id);
            Assert.IsTrue(found, "The entity id toshibaCoreI7 should be selected");

            found = result.ListEntityId.Contains(dellCoreI7.Id);
            Assert.IsTrue(found, "The entity id dellCoreI7 should be selected");
        }

        /// <summary>
        ///  select entities having a property key='Name'
        ///  Search in the root folder only, not in subfolder.
        ///  
        /// -Data:
        ///  Ent: Name=Toshiba   Ok, selected
        ///  
        ///  Fold: computers\
        ///    Ent: Name=Dell      --->NOT seleted
        ///     
        /// </summary>
        [TestMethod]
        public void SearchEntities_RootOnly_OnePropKeyEqualsName_OneInFolder()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //==== creates entities, with prop
            //----create entities
            Entity toshibaCoreI7 = core.Editor.CreateEntity();
            core.Editor.CreateProperty(toshibaCoreI7, "Name", "Toshiba Satellite Core I7");
            core.Editor.CreateProperty(toshibaCoreI7, "TradeMark", "Toshiba");

            // create a folder with an entity inside it
            Folder foldComputers = core.Editor.CreateFolder(null, "computers");

            Entity dellCoreI7 = core.Editor.CreateEntity(foldComputers);
            core.Editor.CreateProperty(dellCoreI7, "Name", "Dell Inspiron 15-5570-sku6");
            core.Editor.CreateProperty(dellCoreI7, "TradeMark", "Dell");

            //==== define the search: from the root folder, select all entities having a key prop called 'Name'
            SearchEntity searchEntities = core.Searcher.CreateSearchEntity(SearchFolderScope.RootOnly);

            //--Add single criteria: property key text equals to 'Name'
            SearchPropCriterionKeyText criterion = core.Searcher.AddCritPropKeyText(searchEntities, "Name");
            criterion.TextMatch = CritOptionTextMatch.TextMatchExact;
            criterion.PropKeyTextType = CritOptionPropKeyTextType.AllKeyType;

            //==== execute the search, get the result: list of found entities
            SearchEntityResult result = core.Searcher.ExecSearchEntity(searchEntities);

            // check found entities
            Assert.AreEqual(1, result.ListEntityId.Count, "One found entity expected");

            bool found;
            found = result.ListEntityId.Contains(toshibaCoreI7.Id);
            Assert.IsTrue(found, "The entity id toshibaCoreI7 should be selected");
        }

        /// <summary>
        ///  select entities having a property key='Name'
        ///  Search in the root folder only, not in subfolder.
        ///  
        /// -Data:
        ///   Ent: Name=Toshiba   Ok, selected
        ///   Ent: Name=Dell      Ok, selected
        ///   Ent: Nom=HP         --->NOT seleted
        ///     
        /// </summary>
        [TestMethod]
        public void SearchEntities_RootOnly_OnePropKeyEqualsName_OneNotSelected()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //==== creates entities, with prop
            //----create entities
            Entity toshibaCoreI7 = core.Editor.CreateEntity();
            core.Editor.CreateProperty(toshibaCoreI7, "Name", "Toshiba Satellite Core I7");
            core.Editor.CreateProperty(toshibaCoreI7, "TradeMark", "Toshiba");

            Entity dellCoreI7 = core.Editor.CreateEntity();
            core.Editor.CreateProperty(dellCoreI7, "Name", "Dell Inspiron 15-5570-sku6");
            core.Editor.CreateProperty(dellCoreI7, "TradeMark", "Dell");

            Entity HPCoreI7 = core.Editor.CreateEntity();
            core.Editor.CreateProperty(HPCoreI7, "Nom", "HP Core i7");
            core.Editor.CreateProperty(HPCoreI7, "TradeMark", "HP");

            //==== define the search: from the root folder, select all entities having a key prop called 'Name'
            SearchEntity searchEntities = core.Searcher.CreateSearchEntity(SearchFolderScope.RootOnly);

            //--Add single criteria: property key text equals to 'Name'
            SearchPropCriterionKeyText criterion = core.Searcher.AddCritPropKeyText(searchEntities, "Name");
            criterion.TextMatch = CritOptionTextMatch.TextMatchExact;
            criterion.PropKeyTextType = CritOptionPropKeyTextType.AllKeyType;

            //==== execute the search, get the result: list of found entities
            SearchEntityResult result = core.Searcher.ExecSearchEntity(searchEntities);

            // check found entities
            Assert.AreEqual(2, result.ListEntityId.Count, "One found entity expected");

            bool found;
            found = result.ListEntityId.Contains(toshibaCoreI7.Id);
            Assert.IsTrue(found, "The entity id toshibaCoreI7 should be selected");

            found = result.ListEntityId.Contains(dellCoreI7.Id);
            Assert.IsTrue(found, "The entity id dellCoreI7 should be selected");
        }

        /// <summary>
        ///  select entities having a property key='Name'
        ///  Search in the root folder: in all folders.
        ///  
        /// -Data:
        ///  Ent: Name=Toshiba   -->NOT selected
        ///  Fold: computers\
        ///    Ent: Name=Dell      Ok, selected
        ///    Ent: Nom=HP         --->NOT seleted
        ///     
        /// </summary>
        [TestMethod]
        public void SearchEntities_DefinedFolders_OnePropKeyEqualsName_E_F_FE_FE()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //==== creates entities, with prop
            //----create entities
            Entity toshibaCoreI7 = core.Editor.CreateEntity();
            core.Editor.CreateProperty(toshibaCoreI7, "Name", "Toshiba Satellite Core I7");
            core.Editor.CreateProperty(toshibaCoreI7, "TradeMark", "Toshiba");

            //---- create a folder, under the root            
            Folder foldComputers = core.Editor.CreateFolder(null, "computers");

            Entity dellCoreI7 = core.Editor.CreateEntity(foldComputers);
            core.Editor.CreateProperty(dellCoreI7, "Name", "Dell Inspiron 15-5570-sku6");
            core.Editor.CreateProperty(dellCoreI7, "TradeMark", "Dell");

            Entity HPCoreI7 = core.Editor.CreateEntity(foldComputers);
            core.Editor.CreateProperty(HPCoreI7, "Nom", "HP Core i7");
            core.Editor.CreateProperty(HPCoreI7, "TradeMark", "HP");

            //==== define the search: from the root folder, select all entities having a key prop called 'Name'
            SearchEntity searchEntities = core.Searcher.CreateSearchEntity(SearchFolderScope.Defined);

            //--Add sources folders, set option: go inside folders childs
            core.Searcher.AddSourceFolder(searchEntities, foldComputers, true);

            //--Add single criteria: property key text equals to 'Name'
            SearchPropCriterionKeyText criterion = core.Searcher.AddCritPropKeyText(searchEntities, "Name");
            criterion.PropKeyTextType = CritOptionPropKeyTextType.AllKeyType;
            criterion.TextMatch = CritOptionTextMatch.TextMatchExact;

            //==== execute the search, get the result: list of found entities
            SearchEntityResult result = core.Searcher.ExecSearchEntity(searchEntities);

            // check found entities
            Assert.AreEqual(1, result.ListEntityId.Count, "One found entity expected");

            bool found;
            found = result.ListEntityId.Contains(dellCoreI7.Id);
            Assert.IsTrue(found, "The entity id dellCoreI7 should be selected");
        }

        /// <summary>
        ///  select entities having a property key='Name'
        ///  Search in the root folder: in all folders.
        ///  
        /// -Data:
        ///  Fold: computers\
        ///    Ent: Name=Toshiba   Ok, selected
        ///    Ent: Name=Dell      Ok, selected
        ///    Ent: Nom=HP         --->NOT seleted
        ///     
        /// </summary>
        [TestMethod]
        public void SearchEntities_DefinedFolders_OnePropKeyEqualsName()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //---- create a folder, under the root            
            Folder foldComputers = core.Editor.CreateFolder(null, "computers");

            //==== creates entities, with prop
            //----create entities
            Entity toshibaCoreI7 = core.Editor.CreateEntity(foldComputers);
            core.Editor.CreateProperty(toshibaCoreI7, "Name", "Toshiba Satellite Core I7");
            core.Editor.CreateProperty(toshibaCoreI7, "TradeMark", "Toshiba");

            Entity dellCoreI7 = core.Editor.CreateEntity(foldComputers);
            core.Editor.CreateProperty(dellCoreI7, "Name", "Dell Inspiron 15-5570-sku6");
            core.Editor.CreateProperty(dellCoreI7, "TradeMark", "Dell");

            Entity HPCoreI7 = core.Editor.CreateEntity(foldComputers);
            core.Editor.CreateProperty(HPCoreI7, "Nom", "HP Core i7");
            core.Editor.CreateProperty(HPCoreI7, "TradeMark", "HP");

            //==== define the search: from the root folder, select all entities having a key prop called 'Name'
            SearchEntity searchEntities = core.Searcher.CreateSearchEntity(SearchFolderScope.Defined);

            //--Add sources folders, set option: go inside folders childs
            // TODO: only one for now is managed
            core.Searcher.AddSourceFolder(searchEntities, foldComputers, true);

            //--Add single criteria: property key text equals to 'Name'
            SearchPropCriterionKeyText criterion = core.Searcher.AddCritPropKeyText(searchEntities, "Name");
            criterion.TextMatch = CritOptionTextMatch.TextMatchExact;
            criterion.PropKeyTextType = CritOptionPropKeyTextType.AllKeyType;

            //--set options
            // TODO: lesquelles?

            //==== execute the search, get the result: list of found entities
            SearchEntityResult result = core.Searcher.ExecSearchEntity(searchEntities);

            // check found entities
            Assert.AreEqual(2, result.ListEntityId.Count, "2 found entities expected");

            bool found; 
            found = result.ListEntityId.Contains(toshibaCoreI7.Id);
            Assert.IsTrue(found, "The entity id toshibaCoreI7 should be selected");

            found = result.ListEntityId.Contains(dellCoreI7.Id);
            Assert.IsTrue(found, "The entity id dellCoreI7 should be selected");

            found = result.ListEntityId.Contains(HPCoreI7.Id);
            Assert.IsFalse(found, "The entity id HPCoreI7 should NOT be selected");
        }

        /// <summary>
        ///  select entities having a property key='Name'
        ///  
        /// -Data:
        ///  F:computers\
        ///    Ent: Name=Toshiba   Ok, selected
        ///    Ent: Name=Dell      Ok, selected
        ///    Ent: Nom=HP         NOT seleted
        ///     
        /// </summary>
        [TestMethod]
        public void SearchInFolderEntitiesOnePropKeyEqualsName()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //---- create a folder, under the root            
            Folder foldComputers = core.Editor.CreateFolder(null, "computers");

            //==== creates entities, with prop
            //----create entities
            Entity toshibaCoreI7 = core.Editor.CreateEntity(foldComputers);
            core.Editor.CreateProperty(toshibaCoreI7, "Name", "Toshiba Satellite Core I7");
            core.Editor.CreateProperty(toshibaCoreI7, "TradeMark", "Toshiba");

            Entity dellCoreI7 = core.Editor.CreateEntity(foldComputers);
            core.Editor.CreateProperty(dellCoreI7, "Name", "Dell Inspiron 15-5570-sku6");
            core.Editor.CreateProperty(dellCoreI7, "TradeMark", "Dell");

            Entity HPCoreI7 = core.Editor.CreateEntity(foldComputers);
            core.Editor.CreateProperty(HPCoreI7, "Nom", "HP Core i7");
            core.Editor.CreateProperty(HPCoreI7, "TradeMark", "HP");

            //==== define the search: from the root folder, select all entities having a key prop called 'Name'
            SearchEntity searchEntities = core.Searcher.CreateSearchEntity("EntitiesHavingPropName", SearchFolderScope.Defined);

            //--Add sources folders, set option: go inside folders childs
            // TODO: only one for now is managed
            core.Searcher.AddSourceFolder(searchEntities, foldComputers, true);

            //--Add single criteria: property key text equals to 'Name'
            SearchPropCriterionKeyText criterion = core.Searcher.AddCritPropKeyText(searchEntities, "Name");
            criterion.TextMatch = CritOptionTextMatch.TextMatchExact;
            criterion.PropKeyTextType = CritOptionPropKeyTextType.AllKeyType;

            //==== execute the search, get the result: list of found entities
            SearchEntityResult result = core.Searcher.ExecSearchEntity(searchEntities);

            // check found entities
            Assert.AreEqual(2, result.ListEntityId.Count, "2 found entities expected");

            bool found;
            found = result.ListEntityId.Contains(toshibaCoreI7.Id);
            Assert.IsTrue(found, "The entity id toshibaCoreI7 should be selected");

            found = result.ListEntityId.Contains(dellCoreI7.Id);
            Assert.IsTrue(found, "The entity id dellCoreI7 should be selected");

            found = result.ListEntityId.Contains(HPCoreI7.Id);
            Assert.IsFalse(found, "The entity id HPCoreI7 should NOT be selected");
        }

        /// <summary>
        ///  select entities having a property key='Name'
        ///  Search in 2 folders.
        ///  
        /// -Data:
        ///  E: Name=Toshiba   -->NOT selected
        ///  
        ///  F: computers\
        ///    E: Name=Dell      Ok, selected
        ///    E: Nom=HP         --->NOT seleted
        ///    
        ///  F: others\
        ///    E: Name=Asus     -->NOT selected
        ///    
        /// </summary>
        [TestMethod]
        public void SearchEntities_DefinedFolders_OnePropKeyEqualsName_E_F1_2E_F2_2E()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //==== creates entities, with prop
            //----create entities
            Entity toshibaCoreI7 = core.Editor.CreateEntity();
            core.Editor.CreateProperty(toshibaCoreI7, "Name", "Toshiba Satellite Core I7");
            core.Editor.CreateProperty(toshibaCoreI7, "TradeMark", "Toshiba");

            //---- create a folder, under the root            
            Folder foldComputers = core.Editor.CreateFolder(null, "computers");

            Entity dellCoreI7 = core.Editor.CreateEntity(foldComputers);
            core.Editor.CreateProperty(dellCoreI7, "Name", "Dell Inspiron 15-5570-sku6");
            core.Editor.CreateProperty(dellCoreI7, "TradeMark", "Dell");

            Entity HPCoreI7 = core.Editor.CreateEntity(foldComputers);
            core.Editor.CreateProperty(HPCoreI7, "Nom", "HP Core i7");
            core.Editor.CreateProperty(HPCoreI7, "TradeMark", "HP");

            //---- create a folder, under the root            
            Folder foldOthers = core.Editor.CreateFolder(null, "others");

            Entity asusCoreI7 = core.Editor.CreateEntity(foldOthers);
            core.Editor.CreateProperty(asusCoreI7, "Name", "Asus Core i7");
            core.Editor.CreateProperty(asusCoreI7, "TradeMark", "Asus");

            //==== define the search: from the root folder, select all entities having a key prop called 'Name'
            SearchEntity searchEntities = core.Searcher.CreateSearchEntity(SearchFolderScope.Defined);

            //--Add sources folders, set option: go inside folders childs
            // TODO: only one for now is managed
            core.Searcher.AddSourceFolder(searchEntities, foldComputers, true);

            //--Add single criteria: property key text equals to 'Name'
            SearchPropCriterionKeyText criterion = core.Searcher.AddCritPropKeyText(searchEntities, "Name");
            criterion.TextMatch = CritOptionTextMatch.TextMatchExact;
            criterion.PropKeyTextType = CritOptionPropKeyTextType.AllKeyType;

            //==== execute the search, get the result: list of found entities
            SearchEntityResult result = core.Searcher.ExecSearchEntity(searchEntities);

            // check found entities
            Assert.AreEqual(1, result.ListEntityId.Count, "One found entity expected");

            bool found;
            found = result.ListEntityId.Contains(dellCoreI7.Id);
            Assert.IsTrue(found, "The entity id dellCoreI7 should be selected");
        }

        /// <summary>
        ///  select entities having a property key='Name'
        ///  Search in 2 folders.
        ///  
        /// -Data:
        ///  E: Name=Toshiba   -->NOT selected
        ///  
        ///  F: computers\
        ///    E: Name=Dell      Ok, selected
        ///    E: Nom=HP         --->NOT seleted
        ///    
        ///  F: others\
        ///    E: Name=Asus     Ok, selected
        ///    
        /// </summary>
        [TestMethod]
        public void SearchEntities_DefinedFolders_2_OnePropKeyEqualsName_E_F1_2E_F2_2E()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //==== creates entities, with prop
            //----create entities
            Entity toshibaCoreI7 = core.Editor.CreateEntity();
            core.Editor.CreateProperty(toshibaCoreI7, "Name", "Toshiba Satellite Core I7");
            core.Editor.CreateProperty(toshibaCoreI7, "TradeMark", "Toshiba");

            //---- create a folder, under the root            
            Folder foldComputers = core.Editor.CreateFolder(null, "computers");

            Entity dellCoreI7 = core.Editor.CreateEntity(foldComputers);
            core.Editor.CreateProperty(dellCoreI7, "Name", "Dell Inspiron 15-5570-sku6");
            core.Editor.CreateProperty(dellCoreI7, "TradeMark", "Dell");

            Entity HPCoreI7 = core.Editor.CreateEntity(foldComputers);
            core.Editor.CreateProperty(HPCoreI7, "Nom", "HP Core i7");
            core.Editor.CreateProperty(HPCoreI7, "TradeMark", "HP");

            //---- create a folder, under the root            
            Folder foldOthers = core.Editor.CreateFolder(null, "others");

            Entity asusCoreI7 = core.Editor.CreateEntity(foldOthers);
            core.Editor.CreateProperty(asusCoreI7, "Name", "Asus Core i7");
            core.Editor.CreateProperty(asusCoreI7, "TradeMark", "Asus");

            //==== define the search: from the root folder, select all entities having a key prop called 'Name'
            SearchEntity searchEntities = core.Searcher.CreateSearchEntity(SearchFolderScope.Defined);

            //--Add sources folders, set option: go inside folders childs
            core.Searcher.AddSourceFolder(searchEntities, foldComputers, true);

            // add a second source folder
            core.Searcher.AddSourceFolder(searchEntities, foldOthers, true);

            //--Add single criteria: property key text equals to 'Name'
            SearchPropCriterionKeyText criterion = core.Searcher.AddCritPropKeyText(searchEntities, "Name");
            criterion.TextMatch = CritOptionTextMatch.TextMatchExact;
            criterion.PropKeyTextType = CritOptionPropKeyTextType.AllKeyType;

            //==== execute the search, get the result: list of found entities
            SearchEntityResult result = core.Searcher.ExecSearchEntity(searchEntities);

            // check found entities
            Assert.AreEqual(2, result.ListEntityId.Count, "2 found entity expected");

            bool found;
            found = result.ListEntityId.Contains(dellCoreI7.Id);
            Assert.IsTrue(found, "The entity id dellCoreI7 should be selected");
            found = result.ListEntityId.Contains(asusCoreI7.Id);
            Assert.IsTrue(found, "The entity id asusCoreI7 should be selected");
        }

    }
}
