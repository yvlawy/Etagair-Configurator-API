using Etagair.Core.Reposit.Contract;
using Etagair.Core.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core
{
    /// <summary>
    /// Editor for template.
    /// </summary>
    public class EditorTempl
    {
        IEtagairReposit _reposit;

        public EditorTempl(IEtagairReposit reposit)
        {
            _reposit = reposit;
        }

        #region Create Folder template

        #endregion

        #region Create Entity template

        /// <summary>
        /// Create an entity, under the root folder.
        /// </summary>
        /// <param name="folderParent"></param>
        /// <returns></returns>
        public EntityTempl CreateEntityTempl(string name)
        {
            return CreateEntityTempl(_reposit.Finder.GetRootFolder(), name);
        }

        /// <summary>
        /// Create an entity template under the root folder parent.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public EntityTempl CreateEntityTempl(Folder folderParent, string name)
        {
            // parent folder is null? its the root
            if (folderParent == null)
            {
                folderParent = _reposit.Finder.GetRootFolder();
            }

            // TODO:
            if (string.IsNullOrWhiteSpace(name))
                return null;

            // check the syntax
            // TODO: no special character,...

            // create an entity, attach it the parent
            EntityTempl entityTempl = new EntityTempl();
            entityTempl.ParentFolderId = folderParent.Id;
            entityTempl.Name = name;

            // set a key to the property root
            PropKeyTemplString key = new PropKeyTemplString();
            key.Key = CoreDef.DefaultPropertyRootName;
            entityTempl.PropertyRoot.Key = key;

            // save it
            if (!_reposit.Builder.SaveEntityTempl(entityTempl))
                return null;

            folderParent.AddChild(entityTempl);

            // update the parent foder, has a new child
            if (!_reposit.Builder.UpdateFolder(folderParent))
                return null;

            return entityTempl;

        }
        #endregion

        #region Create Property template

        /// <summary>
        /// Create a property group template.
        /// Under the prop root of the entity.
        /// </summary>
        /// <param name="entityTempl"></param>
        /// <param name="tcKey"></param>
        /// <param name="tcValue"></param>
        /// <returns></returns>
        public PropGroupTempl CreatePropGroupTempl(EntityTempl entityTempl, string key)
        {
            // check the entity parent
            if (entityTempl == null)
                return null;

            // get the root group properties of the entity
            PropGroupTempl propertyParent = entityTempl.PropertyRoot;

            PropKeyTemplString propKeyString= new PropKeyTemplString();
            propKeyString.Key = key;
            return CreatePropGroupTempl(entityTempl, propertyParent, propKeyString);

        }

        /// <summary>
        /// Create a property group template.
        /// Set the key (string or TextCode), no value, will habe prop childs.
        /// </summary>
        /// <param name="entityTempl"></param>
        /// <param name="propGroupTemplParent"></param>
        /// <param name="propKey"></param>
        /// <returns></returns>
        public PropGroupTempl CreatePropGroupTempl(EntityTempl entityTempl, PropGroupTempl propGroupTemplParent, PropKeyTemplBase propKey)
        {
            // check the entity parent
            if (entityTempl == null)
                return null;

            // get the root group properties of the entity
            PropGroupTempl propertyParent = entityTempl.PropertyRoot;

            if (propKey == null)
                return null;

            string propKeyString = GetPropKeyTemplString(propKey);

            // check the key, not used by an existing property
            if (FindPropTemplBaseByKey(entityTempl, propGroupTemplParent, propKeyString) != null)
                return null;

            // create the property, set the key and the value
            PropGroupTempl propGroupTempl = new PropGroupTempl();
            propGroupTempl.PropGroupTemplParentId = propGroupTemplParent.Id;

            //PropKeyTemplTextCode propertyKey = new PropKeyTemplTextCode();
            //propertyKey.TextCodeId = tcKey.Id;

            propGroupTempl.SetKey(propKey);

            // add the property under the root properties
            entityTempl.AddProperty(propGroupTemplParent, propGroupTempl);

            // save the entity modification
            if (!_reposit.Builder.UpdateEntityTempl(entityTempl))
                return null;

            return propGroupTempl;

        }



        /// <summary>
        /// Create a property template.
        /// The string value is null.
        /// </summary>
        /// <param name="entityTempl"></param>
        /// <param name="tcKey"></param>
        /// <returns></returns>
        public PropTempl CreatePropTemplValueStringNull(EntityTempl entityTempl, string key)
        {
            return CreatePropTempl(entityTempl, key, null);
        }

        /// <summary>
        /// Create a property template.
        /// </summary>
        /// <param name="entityTempl"></param>
        /// <param name="tcKey"></param>
        /// <param name="tcValue"></param>
        /// <returns></returns>
        public PropTempl CreatePropTempl(EntityTempl entityTempl, string key, string value)
        {
            // check the entity parent
            if (entityTempl == null)
                return null;

            // get the root group properties of the entity
            PropGroupTempl propertyParent = entityTempl.PropertyRoot;

            return CreatePropTempl(entityTempl, propertyParent, key, value);
        }

        /// <summary>
        /// Create a property template.
        /// </summary>
        /// <param name="entityTempl"></param>
        /// <param name="tcKey"></param>
        /// <param name="tcValue"></param>
        /// <returns></returns>
        public PropTempl CreatePropTempl(EntityTempl entityTempl, PropGroupTempl propertyParent, string key, string value)
        {
            // check the entity parent
            if (entityTempl == null)
                return null;

            PropKeyTemplString propKeyString = new PropKeyTemplString();
            propKeyString.Key= key;

            PropValueTemplString propValueString = new PropValueTemplString();
            propValueString.Value = value;

            return CreatePropTempl(entityTempl, propertyParent, propKeyString, propValueString);

        }

        /// <summary>
        /// Create a property template.
        /// The TextCode value is null.
        /// </summary>
        /// <param name="entityTempl"></param>
        /// <param name="tcKey"></param>
        /// <returns></returns>
        public PropTempl CreatePropTemplValueTextCodeNull(EntityTempl entityTempl, TextCode tcKey)
        {
            return CreatePropTempl(entityTempl, tcKey, (TextCode)null);
        }

        /// <summary>
        /// Create a property template.
        /// to set a value to null, provide this parameter: (TextCode)null.
        /// </summary>
        /// <param name="entityTempl"></param>
        /// <param name="tcKey"></param>
        /// <param name="tcValue"></param>
        /// <returns></returns>
        public PropTempl CreatePropTempl(EntityTempl entityTempl, TextCode tcKey, TextCode tcValue)
        {
            // check the entity parent
            if (entityTempl == null)
                return null;

            // get the root group properties of the entity
            PropGroupTempl propertyParent = entityTempl.PropertyRoot;

            return CreatePropTempl(entityTempl, propertyParent, tcKey, tcValue);
        }


        public PropTempl CreatePropTempl(EntityTempl entityTempl, TextCode tcKey, string value)
        {
            // check the entity parent
            if (entityTempl == null)
                return null;

            // get the root group properties of the entity
            PropGroupTempl propertyParent = entityTempl.PropertyRoot;

            return CreatePropTempl(entityTempl, propertyParent, tcKey, value);
        }

        public PropTempl CreatePropTempl(EntityTempl entityTempl, PropGroupTempl propertyParent, TextCode tcKey, TextCode tcValue)
        {
            // check the entity parent
            if (entityTempl == null)
                return null;

            PropKeyTemplTextCode propKeyTextCode = new PropKeyTemplTextCode();
            propKeyTextCode.TextCodeId = tcKey.Id;

            PropValueTemplTextCode propValueTextCode = null;

            propValueTextCode = new PropValueTemplTextCode();
            if (tcValue != null)
                // can be null (to set on instantiation)
                propValueTextCode.TextCodeId = tcValue.Id;

            return CreatePropTempl(entityTempl, propertyParent, propKeyTextCode, propValueTextCode);
        }

        public PropTempl CreatePropTempl(EntityTempl entityTempl, PropGroupTempl propertyParent, TextCode tcKey, string value)
        {
            // check the entity parent
            if (entityTempl == null)
                return null;

            PropKeyTemplTextCode propKeyTextCode= new PropKeyTemplTextCode();
            propKeyTextCode.TextCodeId = tcKey.Id;
            PropValueTemplString propValueString= new PropValueTemplString();
            propValueString.Value = value;
            return CreatePropTempl(entityTempl, propertyParent, propKeyTextCode, propValueString);
        }

        public PropTempl CreatePropTempl(EntityTempl entityTempl, PropGroupTempl propGroupTemplParent, PropKeyTemplBase propKey, PropValueTemplBase propValue)
        {
            // check the entity parent
            if (entityTempl == null)
                return null;

            if (propGroupTemplParent == null)
                propGroupTemplParent = entityTempl.PropertyRoot;

            if (propKey == null)
                return null;

            string propKeyString = GetPropKeyTemplString(propKey);

            // check the key, not used by an existing property
            if (FindPropTemplBaseByKey(entityTempl, propGroupTemplParent, propKeyString) != null)
                return null;

            // create the property, set the key and the value
            PropTempl propertyTempl = new PropTempl();
            propertyTempl.PropGroupTemplParentId = propGroupTemplParent.Id;

            //PropKeyTemplTextCode propertyKey = new PropKeyTemplTextCode();
            //propertyKey.TextCodeId = tcKey.Id;

            //PropValueTemplString propertyValue = null;
            //if (value != null)
            //{
            //    propertyValue = new PropValueTemplString();
            //    propertyValue.Value = value;
            //}

            propertyTempl.SetKeyValue(propKey, propValue);

            // add the property under the root properties
            entityTempl.AddProperty(propGroupTemplParent, propertyTempl);

            // save the entity modification
            if (!_reposit.Builder.UpdateEntityTempl(entityTempl))
                return null;

            return propertyTempl;
        }

        /// <summary>
        /// Add a rule to a property template.
        /// </summary>
        /// <param name="propTempl"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        public bool AddPropTemplRule(EntityTempl entityTempl, PropTempl propTempl, PropTemplRuleBase rule)
        {
            // do some checks
            // TODO:  

            // the propTempl shouln't have the same rule type
            if (propTempl.ListRule.Find(r => r.Type == rule.Type) != null)
                return false;

            rule.EntityTemplId = entityTempl.Id;

            rule.PropKeyTempl = propTempl.Key;

            // find the property group parent of the prop
            //PropGroupTempl propGroupTemplParent = FindPropGroupTemplParent(entityTempl, propTempl);
            //rule.PropGroupTemplId = propGroupTemplParent.Id;
            rule.PropGroupTemplId = propTempl.PropGroupTemplParentId;

            propTempl.AddRule(rule);

            // save the entity modification
            return _reposit.Builder.UpdateEntityTempl(entityTempl);
        }

        #endregion

        #region Public Find Methods

        /// <summary>
        /// Find a property by the key "raw" string (can be a textCode).
        /// in direct childs. 
        /// (not recursivly).
        /// </summary>
        /// <param name="entityTempl"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public PropTemplBase FindPropTemplBaseByKey(EntityTempl entityTempl, PropGroupTempl propertyTemplParent, string key)
        {
            if (entityTempl == null)
                return null;
            if (propertyTemplParent == null)
                return null;
            if (string.IsNullOrWhiteSpace(key))
                return null;

            // load the textCode of the key, if exists
            TextCode tcKey = _reposit.Finder.FindTextCodeByCode(key);

            // the key can be a string or a textCode
            foreach (var propertyBase in propertyTemplParent.ListProperty)
            {
                // is it a final property?
                PropTempl property = propertyBase as PropTempl;
                if (property != null)
                {
                    // is the property key a textCode?
                    if (IsKeyMatchProperty(property, key, tcKey))
                        return property;

                    // next property
                    continue;
                }

                // is it a group property?
                PropGroupTempl propertyGroup = propertyBase as PropGroupTempl;
                if (propertyGroup != null)
                {
                    // is the property key a textCode?
                    if (IsKeyMatchProperty(propertyGroup, key, tcKey))
                        return propertyGroup;

                    // next property
                    continue;
                }
            }

            // not found
            return null;

        }

        #endregion

        #region Privates methods

        public string GetPropKeyTemplString(PropKeyTemplBase propKey)
        {
            string keyString = "";

            PropKeyTemplString propKeyTemplString = propKey as PropKeyTemplString;
            if(propKeyTemplString!=null)
            {
                keyString = propKeyTemplString.Key;
                return keyString;
            }

            PropKeyTemplTextCode propKeyTemplTextCode = propKey as PropKeyTemplTextCode;
            if (propKeyTemplTextCode != null)
            {
                // load the textCode
                keyString = _reposit.Finder.FindTextCodeById(propKeyTemplTextCode.TextCodeId).Code;
                return keyString;
            }

            return null;
        }

        // is the property key a textCode?
        private bool IsKeyMatchProperty(PropTemplBase property, string key, TextCode tcKey)
        {
            PropKeyTemplTextCode keyTextCode = property.Key as PropKeyTemplTextCode;
            if (keyTextCode != null)
            {
                if (tcKey != null)
                {
                    // the key to find is a TextCode
                    if (keyTextCode.TextCodeId.Equals(tcKey.Id))
                        return true;
                }
                else
                {
                    // the property key is a textCode, load it to get the code, because the key is a string
                    TextCode tcPropKey = _reposit.Finder.FindTextCodeById(keyTextCode.TextCodeId);
                    if (tcPropKey.Code.Equals(key))
                        return true;
                }
            }

            // is the property key a string?
            PropKeyTemplString keyString = property.Key as PropKeyTemplString;
            if (keyString != null)
            {
                // compare strings
                if (keyString.Key.Equals(key))
                    return true;
            }

            // the property doesn't match the key (string or TextCode) 
            return false;
        }

        #endregion

    }
}
