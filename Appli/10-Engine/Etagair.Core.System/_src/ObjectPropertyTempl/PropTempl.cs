using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    /// <summary>
    /// An entity property: its a pair of key-value + some rules.
    /// The key is defined in the base class.
    /// </summary>
    public class PropTempl : PropTemplBase
    {

        //public PropValueTemplBase Value { get; set; }
        public IValue Value { get; set; }

        /// <summary>
        /// Set the key and the value.
        /// The value can be null (to be set on instantiation).
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        //public void SetKeyValue(PropKeyTemplBase key, PropValueTemplBase value)
        //{
        //    Key = key;
        //    Value = value;
        //}
        public void SetKeyValue(PropKeyTemplBase key, IValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
