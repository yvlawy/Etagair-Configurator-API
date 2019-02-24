using Etagair.Core.System;
using Etagair.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace SamplesApp
{
    public class LanguageTranslation: Common
    {
        /// <summary>
        /// Dev the language and text data model.
        /// E:
        ///   P: key=TC:tcName= Value=TC:tcNameToshiba
        /// </summary>
        public void ManageLanguagesAndLocalizedText()
        {
            EtagairEngine engine = CreateEngine();

            Console.WriteLine("Create TextCode, TextLocalModel, and then TextLocal.");

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

        // languages: Mapping on main fr <- fr_FR,...

    }
}
