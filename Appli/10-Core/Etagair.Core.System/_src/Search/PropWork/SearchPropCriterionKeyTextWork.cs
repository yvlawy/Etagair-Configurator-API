using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    public class SearchPropCriterionKeyTextWork: SearchPropCriterionBaseWork
    {
        public SearchPropCriterionKeyTextWork()
        {
            //Type = SearchPropCriterionType.PropKeyText;
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
