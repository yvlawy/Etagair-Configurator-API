using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    public enum PropertyMatch
    {
        NotSet,
        Yes,
        No
    }

    /// <summary>
    /// A searchProBase used for work/analyzing.
    /// </summary>
    public abstract class SearchPropBaseWork
    {
        public SearchPropBaseWork()
        {
            PropertyMatch = PropertyMatch.NotSet;
        }

        /// <summary>
        /// The SearchPropBase referenced.
        /// </summary>
        public string SearchPropBaseId { get; set; }

        public PropertyMatch PropertyMatch { get; set; }
    }
}
