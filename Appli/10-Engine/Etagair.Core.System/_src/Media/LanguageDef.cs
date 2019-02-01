using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    /// <summary>
    /// A language definition, available for the catalog.
    /// </summary>
    public class LanguageDef
    {
        public string Id { get; set; }
        public MainLanguageCode MainLanguageCode { get; set; }
        public LanguageCode LanguageCode { get; set; }

        // todo: autres infos spécifiques à la langue, fuseau horaire?  
        // ou culture: money,...


        public static LanguageCode ToLanguageCode(MainLanguageCode mainLanguageCode)
        {
            if (mainLanguageCode == MainLanguageCode.fr)
                return LanguageCode.fr;

           return LanguageCode.en;
        }
    }
}
