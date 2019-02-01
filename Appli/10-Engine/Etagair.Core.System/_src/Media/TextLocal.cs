using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    /// <summary>
    /// Instance of a textLocalModel.
    /// Adds parameters.
    /// 
    /// Not saved in the repository.
    /// </summary>
    public class TextLocal
    {
        //public string Id { get; set; }

        public string TextLocalModelId { get; set; }

        // the parameters values
        // TODO:

        /// <summary>
        /// The text in the language, without parameters values. 
        /// </summary>
        public string Text
        { get; set; }
    }
}
