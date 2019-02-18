using Etagair.Core.Reposit.Contract;
using Etagair.Core.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Etagair.Core.Tests
{
    [TestClass]
    public class TestCreateTextLocal
    {
        public string RepositConfig = "InMemory";

        [TestInitialize]
        public void Init()
        {
        }

        [TestMethod]
        public void CreateTextLocal_Ok()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //----set defined (activate) language codes in the current catalog
            core.Editor.DefineLanguage(LanguageCode.en); 

            TextCode tcName = core.Editor.CreateTextCode("Name");

            // create localized text for main languages managed in the application
            // en -> en_uk, en_us,....
            TextLocalModel tlmName = core.Editor.CreateTextLocalModel(LanguageCode.en, tcName, "Name");
            Assert.IsNotNull(tlmName.Id, "id should be set");
            Assert.AreEqual(tcName.Id, tlmName.TextCodeId, "textCode id should match");
            Assert.AreEqual("Name", tlmName.Text, "text should be set");

        }

        [TestMethod]
        public void CreateTextLocal_NoLanguageDefined_Failed()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            TextCode tcName = core.Editor.CreateTextCode("Name");

            TextLocalModel tlmName = core.Editor.CreateTextLocalModel(LanguageCode.en, tcName, "Name");
            Assert.IsNull(tlmName, "should be null, lang is not defined");
        }

        [TestMethod]
        public void CreateTextLocal_Failed()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //----set defined (activate) language codes in the current catalog
            core.Editor.DefineLanguage(LanguageCode.en);

            TextCode tcName = core.Editor.CreateTextCode("Name");

            TextLocalModel tlmName = core.Editor.CreateTextLocalModel(LanguageCode.en_GB, tcName, "Name");
            Assert.IsNull(tlmName, "Can't be created");
        }

        // en -> en_uk, en_us
        // create textLocal
    }
}
