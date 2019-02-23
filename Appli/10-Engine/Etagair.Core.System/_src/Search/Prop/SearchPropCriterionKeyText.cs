using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{

    /// <summary>
    /// Text match
    /// for key or value text.
    /// </summary>
    public enum CritOptionTextMatch
    {
        TextMatchExact,
        //TextMatchRegex,
        //TextMatchContains
    }

    public enum CritOptionTextSensitive
    {
        Yes,
        No
    }
    
    /// <summary>
    /// Match property key text type:
    /// string, TextCode or Both/All.
    /// </summary>
    public enum CritOptionPropKeyTextType
    {
        AllKeyType,
        KeyStringOnly,
        KeyTextCodeOnly
    }

    /// <summary>
    /// Match property key: final, group or All.
    /// FinalGroupAll
    /// </summary>
    public enum CritOptionPropKeyType
    {
        // can match both: final and group property
        All,
        OnlyFinal,
        OnlyGroup
    }

    /// <summary>
    /// The scope.
    /// Scan Only direct childs of the root property group of the entity.
    /// or scan inside sub property groups.
    /// </summary>
    public enum CritOptionPropChildsScan
    {
        /// <summary>
        /// go inside properties group childs if exists.
        /// </summary>
        GoInsidePropGroupChilds,

        /// <summary>
        /// scan only direct property childs.
        /// </summary>
        //OnlyDirectChilds,
    }

    /// <summary>
    /// A final search expression operand.
    /// the criterion is the property key text, used to find entities.
    /// </summary>
    public class SearchPropCriterionKeyText: SearchPropCriterionBase
    {
        public SearchPropCriterionKeyText()
        {
            Type = SearchPropCriterionType.PropKeyText;
        }

        /// <summary>
        /// the text to find in prop key.
        /// </summary>
        public string KeyText { get; set; }

        /// <summary>
        /// prop key text/data type.
        /// </summary>
        public CritOptionPropKeyTextType PropKeyTextType { get; set; }

        public CritOptionPropKeyType PropKeyType { get; set; }

        public CritOptionTextMatch TextMatch { get; set; }

        public CritOptionTextSensitive TextSensitive { get; set; }

        public CritOptionPropChildsScan PropChildsScan { get; set; }
    }
}
