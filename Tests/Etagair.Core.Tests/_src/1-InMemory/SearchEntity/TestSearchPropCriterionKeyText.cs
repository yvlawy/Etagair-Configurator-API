using Etagair.Core.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.Tests
{
    [TestClass]
    public class TestSearchPropCriterionKeyText
    {
        public string RepositConfig = "InMemory";

        [TestInitialize]
        public void Init()
        {

        }

        /// <summary>
        /// Search entities having a property key content equals to "Name".
        /// Source folder: from the root.
        /// 
        /// -Data:
        ///  Ent: Name=Toshiba   Ok, selected
        /// </summary>
        [TestMethod]
        public void SearchEntities_PropKeyEqualsName()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //==== creates entities, with prop
            //----create entities
            Entity toshibaCoreI7 = core.Editor.CreateEntity();
            core.Editor.CreateProperty(toshibaCoreI7, "Name", "Toshiba Satellite Core I7");
            core.Editor.CreateProperty(toshibaCoreI7, "TradeMark", "Toshiba");

            //==== define the search: from the root folder, select all entities having a key prop called 'Name'
            SearchEntity searchEntities = core.Searcher.CreateSearchEntity(SearchFolderScope.All);

            //--Add single criteria: property key text equals to 'Name'
            SearchPropCriterionKeyText criterion = core.Searcher.AddCritPropKeyText(searchEntities, "Name");
            criterion.PropKeyTextType = CritOptionPropKeyTextType.AllKeyType;
            criterion.TextMatch = CritOptionTextMatch.TextMatchExact;

            //==== execute the search, get the result: list of found entities
            SearchEntityResult result = core.Searcher.ExecSearchEntity(searchEntities);

            // check found entities
            Assert.AreEqual(1, result.ListEntityId.Count, "One found entities expected");

            bool found;
            found = result.ListEntityId.Contains(toshibaCoreI7.Id);
            Assert.IsTrue(found, "The entity id toshibaCoreI7 should be selected");
        }

        /// <summary>
        /// Search entities having a property key content equals to "Name".
        /// Source folder: from the root.
        /// 
        /// -Data:
        ///  E: tc/Name=Toshiba   Ok, selected
        ///  E: tc/LeNom=Asus     NOT Selected
        ///  
        /// </summary>
        [TestMethod]
        public void SearchEntities_PropKey_IsTextCode()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //==== creates entities, with prop
            //----create entities
            Entity toshibaCoreI7 = core.Editor.CreateEntity();
            TextCode tcName = core.Editor.CreateTextCode("Name");
            core.Editor.CreateProperty(toshibaCoreI7, tcName, "Toshiba Satellite Core I7");
            core.Editor.CreateProperty(toshibaCoreI7, "TradeMark", "Toshiba");

            Entity asusCoreI7 = core.Editor.CreateEntity();
            TextCode tcLeNom = core.Editor.CreateTextCode("LeNom");
            core.Editor.CreateProperty(asusCoreI7, tcLeNom, "Asus Core I7");
            core.Editor.CreateProperty(asusCoreI7, "TradeMark", "Asus");

            //==== define the search: from the root folder, select all entities having a key prop called 'Name'
            SearchEntity searchEntities = core.Searcher.CreateSearchEntity(SearchFolderScope.All);

            //--Add single criteria: property key text equals to 'Name'
            SearchPropCriterionKeyText criterion = core.Searcher.AddCritPropKeyText(searchEntities, "Name");
            criterion.PropKeyTextType = CritOptionPropKeyTextType.AllKeyType;
            criterion.TextMatch = CritOptionTextMatch.TextMatchExact;

            //==== execute the search, get the result: list of found entities
            SearchEntityResult result = core.Searcher.ExecSearchEntity(searchEntities);

            // check found entities
            Assert.AreEqual(1, result.ListEntityId.Count, "One found entities expected");

            bool found;
            found = result.ListEntityId.Contains(toshibaCoreI7.Id);
            Assert.IsTrue(found, "The entity id toshibaCoreI7 should be selected");
        }

        /// <summary>
        /// Search entities having a property key content equals to "Name".
        /// Source folder: from the root.
        /// 
        /// -Data:
        ///  E: tc/Name=Toshiba   Ok, selected
        ///  E: s/Name=Asus       NOT Selected
        ///  
        /// </summary>
        [TestMethod]
        public void SearchEntities_PropKey_OnlyTextCode()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //==== creates entities, with prop
            //----create entities
            Entity toshibaCoreI7 = core.Editor.CreateEntity();
            TextCode tcName = core.Editor.CreateTextCode("Name");
            core.Editor.CreateProperty(toshibaCoreI7, tcName, "Toshiba Satellite Core I7");
            core.Editor.CreateProperty(toshibaCoreI7, "TradeMark", "Toshiba");

            Entity asusCoreI7 = core.Editor.CreateEntity();
            core.Editor.CreateProperty(asusCoreI7, "Name", "Asus Core I7");
            core.Editor.CreateProperty(asusCoreI7, "TradeMark", "Asus");

            //==== define the search: from the root folder, select all entities having a key prop called 'Name'
            SearchEntity searchEntities = core.Searcher.CreateSearchEntity(SearchFolderScope.All);

            //--Add single criteria: property key text equals to 'Name'
            SearchPropCriterionKeyText criterion = core.Searcher.AddCritPropKeyText(searchEntities, "Name");
            // only textCode!!
            criterion.PropKeyTextType = CritOptionPropKeyTextType.KeyTextCodeOnly;
            criterion.TextMatch = CritOptionTextMatch.TextMatchExact;

            //==== execute the search, get the result: list of found entities
            SearchEntityResult result = core.Searcher.ExecSearchEntity(searchEntities);

            // check found entities
            Assert.AreEqual(1, result.ListEntityId.Count, "One found entities expected");

            bool found;
            found = result.ListEntityId.Contains(toshibaCoreI7.Id);
            Assert.IsTrue(found, "The entity id toshibaCoreI7 should be selected");
        }

        /// <summary>
        /// Search entities having a property key content equals to "Name".
        /// Source folder: from the root.
        /// 
        /// -Data:
        ///  E: tc/Name=Toshiba   NOT selected
        ///  E: s/Name=Asus       Ok, Selected
        ///  
        /// </summary>
        [TestMethod]
        public void SearchEntities_PropKey_OnlyString()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //==== creates entities, with prop
            //----create entities
            Entity toshibaCoreI7 = core.Editor.CreateEntity();
            TextCode tcName = core.Editor.CreateTextCode("Name");
            core.Editor.CreateProperty(toshibaCoreI7, tcName, "Toshiba Satellite Core I7");
            core.Editor.CreateProperty(toshibaCoreI7, "TradeMark", "Toshiba");

            Entity asusCoreI7 = core.Editor.CreateEntity();
            core.Editor.CreateProperty(asusCoreI7, "Name", "Asus Core I7");
            core.Editor.CreateProperty(asusCoreI7, "TradeMark", "Asus");

            //==== define the search: from the root folder, select all entities having a key prop called 'Name'
            SearchEntity searchEntities = core.Searcher.CreateSearchEntity(SearchFolderScope.All);

            //--Add single criteria: property key text equals to 'Name'
            SearchPropCriterionKeyText criterion = core.Searcher.AddCritPropKeyText(searchEntities, "Name");
            // only textCode!!
            criterion.PropKeyTextType = CritOptionPropKeyTextType.KeyStringOnly;
            criterion.TextMatch = CritOptionTextMatch.TextMatchExact;

            //==== execute the search, get the result: list of found entities
            SearchEntityResult result = core.Searcher.ExecSearchEntity(searchEntities);

            // check found entities
            Assert.AreEqual(1, result.ListEntityId.Count, "One found entities expected");

            bool found;
            found = result.ListEntityId.Contains(asusCoreI7.Id);
            Assert.IsTrue(found, "The entity id asusCoreI7 should be selected");
        }

        // tester les autres options du critere
        // caseSensitive,...

        // rechercher dans des prop childs (dans des propGroup)

        // +tard: ScanScope: avec des prop Group et childs
    }
}
