using Etagair.Core.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.Tests
{
    [TestClass]
    public class TestSearchTextLocal
    {
        public string RepositConfig = "InMemory";

        [TestInitialize]
        public void Init()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetTextLocalInDefaultCurrentLang_ok()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //----Define languages in the current catalog
            core.Editor.DefineLanguage(LanguageCode.en);    // ->becomes the default language
            core.Editor.DefineLanguage(LanguageCode.fr);

            // create textCode and translation
            TextCode tcName = core.Editor.CreateTextCode("Name");
            TextLocalModel tl_en_Name= core.Editor.CreateTextLocalModel(LanguageCode.en, tcName, "Name");
            TextLocalModel tl_fr_Name = core.Editor.CreateTextLocalModel(LanguageCode.fr, tcName, "Nom");

            TextCode tcNameToshiba = core.Editor.CreateTextCode("NameToshiba");
            core.Editor.CreateTextLocalModel(LanguageCode.en, tcNameToshiba, "Laptop Toshiba Core i7 RAM 8Go HD 1To Win10");
            core.Editor.CreateTextLocalModel(LanguageCode.fr, tcNameToshiba, "Ordinateur portable Toshiba Core i7 RAM 8Go DD 1To Win10");


            // get localized text
            TextLocal tlNameFound = core.Editor.GenerateTextLocal(tcName);

            // should be in english
            Assert.AreEqual(tl_en_Name.Id, tlNameFound.TextLocalModelId, "the TextLocal Language should be 'en'");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ChangeCurrentLang_GetTextLocal_ok()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //----Define languages in the current catalog
            core.Editor.DefineLanguage(LanguageCode.en);    // ->becomes the default language
            core.Editor.DefineLanguage(LanguageCode.fr);

            // create textCode and translation
            TextCode tcName = core.Editor.CreateTextCode("Name");
            TextLocalModel tl_en_Name = core.Editor.CreateTextLocalModel(LanguageCode.en, tcName, "Name");
            TextLocalModel tl_fr_Name = core.Editor.CreateTextLocalModel(LanguageCode.fr, tcName, "Nom");

            // create textCode and translation
            TextCode tcNameToshiba = core.Editor.CreateTextCode("NameToshiba");
            core.Editor.CreateTextLocalModel(LanguageCode.en, tcNameToshiba, "Laptop Toshiba Core i7 RAM 8Go HD 1To Win10");
            core.Editor.CreateTextLocalModel(LanguageCode.fr, tcNameToshiba, "Ordinateur portable Toshiba Core i7 RAM 8Go DD 1To Win10");

            // change the current language from en to fr
            core.Searcher.SetCurrentLanguage(LanguageCode.fr);

            // get localized text
            TextLocal tlNameFound = core.Editor.GenerateTextLocal(tcName);

            // should be in french
            Assert.AreEqual(tl_fr_Name.Id, tlNameFound.TextLocalModelId, "the TextLocal Language should be 'en'");
        }

        /// <summary>
        /// Define en and en_GB languages.
        /// 
        /// Create TextLocal only for 'en' language.
        /// 
        /// </summary>
        [TestMethod]
        public void GetTextLocal_enGB_Get_enGB()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //----Define languages in the current catalog
            core.Editor.DefineLanguage(LanguageCode.en);    // ->becomes the default language
            core.Editor.DefineLanguage(LanguageCode.en_GB);

            // create textCode and translation, only for the en lang
            TextCode tcName = core.Editor.CreateTextCode("Name");
            TextLocalModel tl_en_Name = core.Editor.CreateTextLocalModel(LanguageCode.en, tcName, "Name");
            TextLocalModel tl_enGB_Name = core.Editor.CreateTextLocalModel(LanguageCode.en_GB, tcName, "Namee");

            // change the current language from en to en_GB (exp: because the users are in UK)
            core.Searcher.SetCurrentLanguage(LanguageCode.en_GB);


            // get localized text
            TextLocal tlNameFound = core.Editor.GenerateTextLocal(tcName);

            // en textLocal doesn't exists, so return the en main languageCode textLocal
            Assert.AreEqual(tl_enGB_Name.Id, tlNameFound.TextLocalModelId, "the TextLocal Language should be 'enGB'");
        }


        /// <summary>
        /// Define en and en_GB languages.
        /// 
        /// Create TextLocal only for 'en' language.
        /// 
        /// </summary>
        [TestMethod]
        public void GetTextLocal_enGB_Get_en()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //----Define languages in the current catalog
            core.Editor.DefineLanguage(LanguageCode.en);    // ->becomes the default language
            core.Editor.DefineLanguage(LanguageCode.en_GB);

            // create textCode and translation, only for the en lang
            TextCode tcName = core.Editor.CreateTextCode("Name");
            TextLocalModel tl_en_Name = core.Editor.CreateTextLocalModel(LanguageCode.en, tcName, "Name");

            // change the current language from en to en_GB (exp: because the users are in UK)
            core.Searcher.SetCurrentLanguage(LanguageCode.en_GB);


            // get localized text
            TextLocal tlNameFound = core.Editor.GenerateTextLocal(tcName);

            // en textLocal doesn't exists, so return the en main languageCode textLocal
            Assert.AreEqual(tl_en_Name.Id, tlNameFound.TextLocalModelId, "the TextLocal Language should be 'en'");
        }

        // todo:  changer 2 fois de currentLang, extraire un TextLocal après chaque changement
        // doit chaque fois renvoyer le bon 
    }
}
