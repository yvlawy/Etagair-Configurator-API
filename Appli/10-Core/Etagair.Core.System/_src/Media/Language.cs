using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    /// <summary>
    /// A language used in the catalog.
    /// A correspondingn LanguageDef (on LanguageCode) must exists.
    /// </summary>
    public class Language
    {
        public string Id { get; set; }
        public MainLanguageCode MainLanguageCode { get; set; }
        public LanguageCode LanguageCode { get; set; }

        // special config for the catalog

    }
}
