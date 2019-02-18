using Etagair.Core.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.Tests
{
    [TestClass]
    public class TestTextLocalWithParams
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
        public void CreateTextWithOneParam_ok()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //----Define languages in the current catalog
            core.Editor.DefineLanguage(LanguageCode.en);    // ->becomes the default language
            core.Editor.DefineLanguage(LanguageCode.fr);

            // create textCode and translation
            TextCode tcName = core.Editor.CreateTextCode("NameIs",1);

            TextLocalModel tl_en_Name = core.Editor.CreateTextLocalModel(LanguageCode.en, tcName, "My name is {0}: ");
            TextLocalModel tl_fr_Name = core.Editor.CreateTextLocalModel(LanguageCode.fr, tcName, "Mon nom est {0}: ");

            // get localized text
            List<string> listParams = new List<string>();
            listParams.Add("Yvan");
            TextLocal tlNameFound = core.Editor.GenerateTextLocal(tcName, listParams);

            // should be in english
            Assert.AreEqual("My name is Yvan: ", tlNameFound.Text, "the TextLocal should include the parameter");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CreateTextWithOneParam_ParamNotProvided()
        {
            EtagairCore core = Common.CreateCore(RepositConfig);

            //----Define languages in the current catalog
            core.Editor.DefineLanguage(LanguageCode.en);    // ->becomes the default language
            core.Editor.DefineLanguage(LanguageCode.fr);

            // create textCode and translation
            TextCode tcName = core.Editor.CreateTextCode("NameIs", 1);

            TextLocalModel tl_en_Name = core.Editor.CreateTextLocalModel(LanguageCode.en, tcName, "My name is {0}: ");
            TextLocalModel tl_fr_Name = core.Editor.CreateTextLocalModel(LanguageCode.fr, tcName, "Mon nom est {0}: ");

            // get localized text
            List<string> listParams = new List<string>();
            //listParams.Add("Yvan");
            TextLocal tlNameFound = core.Editor.GenerateTextLocal(tcName, listParams);

            // should be in english
            Assert.AreEqual("My name is {0}: ", tlNameFound.Text, "the TextLocal should include the parameter");
        }

    }
}
