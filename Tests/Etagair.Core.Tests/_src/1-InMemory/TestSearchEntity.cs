using Etagair.Core.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.Tests
{
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
        ///  Search in the root folder: in all folders.
        ///  
        ///  Ent: Name=Toshiba   Ok, selected
        ///  Ent: Name=Dell      Ok, selected
        ///  Ent: Nom=HP         --->NOT seleted
        ///     
        /// </summary>
        [TestMethod]
        public void SearchEntitiesOnePropKeyEqualsName()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //---- create a folder, under the root            
            Folder foldComputers = core.Editor.CreateFolder(null, "computers");

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
            SearchEntity searchEntities = core.Searcher.CreateSearchEntity();

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
        /// $$$Root\
        ///     computers\
        ///         Ent: Name=Toshiba   Ok, selected
        ///         Ent: Name=Dell      Ok, selected
        ///         Ent: Nom=HP         NOT seleted
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
            SearchEntity searchEntities = core.Searcher.CreateSearchEntity("EntitiesHavingPropName");

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

    }
}
