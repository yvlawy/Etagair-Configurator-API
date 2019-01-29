using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    public enum SearchPropCriterionType
    {
        // entity having property key text/textCode equals to...
        PropKeyText,

        // entity having a property value which type is an image
        PropValueIsTypeImage
    }

    /// <summary>
    /// One criterion base to search property in an entity.
    /// Its a final operand (not a bool expression).
    /// </summary>
    public abstract class SearchPropCriterionBase : SearchPropBase
    {
        public SearchPropCriterionType Type { get; set; }
    }
}
