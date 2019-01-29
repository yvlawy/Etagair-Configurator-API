using Etagair.Core.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.Tests
{
    [TestClass]
    public class TestLanguage
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
        public void DefineLangBecomesCurrent()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //----set defined (activate) language codes in the current catalog
            core.Editor.DefineLanguage(LanguageCode.en);
            core.Editor.DefineLanguage(LanguageCode.en_GB);

            // get the current/default language: the first one defined
            Language lang= core.Searcher.GetCurrentLanguage();

            Assert.IsNotNull(lang, "Lang should exists");
            Assert.AreEqual(lang.LanguageCode, LanguageCode.en,  "The current languageCode should be 'en'");
        }

        [TestMethod]
        public void NoLangDefinedNoCurrentLang()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            // get the current/default language: the first one defined
            Language lang = core.Searcher.GetCurrentLanguage();

            Assert.IsNull(lang, "Lang should NOT exists");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void DefineLangChangeCurrent_Ok()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //----set defined (activate) language codes in the current catalog
            core.Editor.DefineLanguage(LanguageCode.en);
            core.Editor.DefineLanguage(LanguageCode.en_GB);
            core.Editor.DefineLanguage(LanguageCode.fr);

            bool res= core.Searcher.SetCurrentLanguage(LanguageCode.fr);
            Assert.IsTrue(res, "the setCurrentLanguage should succeded");

            // get the current/default language: the first one defined
            Language lang = core.Searcher.GetCurrentLanguage();

            Assert.IsNotNull(lang, "Lang should exists");
            Assert.AreEqual(lang.LanguageCode, LanguageCode.fr, "The current languageCode should be 'fr'");
        }


        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void DefineLangChangeCurrent_Failed()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //----set defined (activate) language codes in the current catalog
            core.Editor.DefineLanguage(LanguageCode.en);
            core.Editor.DefineLanguage(LanguageCode.en_GB);

            bool res = core.Searcher.SetCurrentLanguage(LanguageCode.fr);
            Assert.IsFalse(res, "the setCurrentLanguage should failed");

            // get the current/default language: the first one defined
            Language lang = core.Searcher.GetCurrentLanguage();

            Assert.IsNotNull(lang, "Lang should exists");
            Assert.AreEqual(lang.LanguageCode, LanguageCode.en, "The current languageCode should be 'en'");
        }

    }
}
