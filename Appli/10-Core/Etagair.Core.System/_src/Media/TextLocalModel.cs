using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    /// <summary>
    /// A text matching a code, in a language.
    /// Without parameter.
    /// </summary>
    public class TextLocalModel 
    {
        public string Id { get; set; }

        /// <summary>
        /// The target language.
        /// </summary>
        public LanguageCode LanguageCode { get; set; }

        /// <summary>
        /// Match the textCode.
        /// </summary>
        public string TextCodeId
        { get; set; }

        /// <summary>
        /// The text in the language, without parameters placeholder, 
        /// (but without parameters values).
        /// </summary>
        public string Text
        { get; set; }

    }
}
