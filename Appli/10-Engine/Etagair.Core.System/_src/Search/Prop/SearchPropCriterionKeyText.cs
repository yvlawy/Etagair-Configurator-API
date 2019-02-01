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
        TextMatchRegex,
        TextMatchExact,
        TextMatchContains
    }

    public enum CritOptionTextSensitive
    {
        Yes,
        No
    }
    
    public enum CritOptionPropChildsScan
    {
        /// <summary>
        /// go inside properties group childs if exists.
        /// </summary>
        GoInsidePropGroupChilds,

        /// <summary>
        /// scan only direct property childs.
        /// </summary>
        OnlyDirectChilds,
    }

    /// <summary>
    /// property key text type.
    /// (data type)
    /// A key can be a string or a textCode. 
    /// </summary>
    public enum CritOptionPropKeyTextType
    {
        AllKeyType,
        KeyStringOnly,
        KeyTextCodeOnly
    }

    // property key type: All, OnlyFinal, OnlyGroup
    public enum CritOptionPropKeyType
    {
        // can match both: final and group property
        All,
        OnlyFinal,
        OnlyGroup
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
