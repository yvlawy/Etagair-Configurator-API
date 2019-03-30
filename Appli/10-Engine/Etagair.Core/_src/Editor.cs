using Etagair.Core.Reposit.Contract;
using Etagair.Core.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core
{
    /// <summary>
    /// The core editor.
    /// </summary>
    public class Editor : EditorBase
    {

        public Editor(IEtagairReposit reposit, Searcher searcher):base(reposit, searcher)
        {
        }

        #region Public Folder/entity methods.

        /// <summary>
        /// Create a folder, under the root folder.
        /// </summary>
        /// <param name="folderParent"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public Folder CreateFolder(string name)
        {
            return CreateFolder(_reposit.Finder.GetRootFolder(), name);
        }

        /// <summary>
        /// Create an entity, under the root folder.
        /// </summary>
        /// <param name="folderParent"></param>
        /// <returns></returns>
        public Entity CreateEntity()
        {
            return CreateEntity(_reposit.Finder.GetRootFolder());
        }

        #endregion


        #region Public Create property methods.
        /// <summary>
        /// Add a property to an object: key - value, 
        /// under the root group properties.
        /// both are textCode (will be displayed translated depending on the language).
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tcKey"></param>
        /// <param name="tcValue"></param>
        /// <returns></returns>
        public Property CreateProperty(Entity entity, TextCode tcKey, TextCode tcValue)
        {
            // check the entity parent
            if (entity == null)
                return null;

            // get the root group properties of the entity
            PropertyGroup propertyParent = entity.PropertyRoot;

            return CreateProperty(entity, propertyParent, tcKey, tcValue);
        }

        public Property CreateProperty(Entity entity, string key, TextCode tcValue)
        {
            // check the entity parent
            if (entity == null)
                return null;

            // get the root group properties of the entity
            PropertyGroup propertyParent = entity.PropertyRoot;

            return CreateProperty(entity, propertyParent, key, tcValue);
        }

        public Property CreateProperty(Entity entity, string key, string value)
        {
            // check the entity parent
            if (entity == null)
                return null;

            // get the root group properties of the entity
            PropertyGroup propertyParent = entity.PropertyRoot;

            return CreateProperty(entity, propertyParent, key, value);
        }

        /// <summary>
        /// Create a property to an object: key - value, 
        /// both are textCode (will be displayed translated depending on the language).
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tcKey"></param>
        /// <param name="tcValue"></param>
        /// <returns></returns>
        public Property CreateProperty(Entity entity, TextCode tcKey, string value)
        {
            // check the entity parent
            if (entity == null)
                return null;

            // get the root group properties of the entity
            PropertyGroup propertyParent = entity.PropertyRoot;

            return CreateProperty(entity, propertyParent, tcKey, value);        
        }

        /// <summary>
        /// Create a property to an object: key - value, 
        /// both are textCode (will be displayed translated depending on the language).
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tcKey"></param>
        /// <param name="tcValue"></param>
        /// <returns></returns>
        public Property CreateProperty(Entity entity, TextCode tcKey, double value)
        {
            // check the entity parent
            if (entity == null)
                return null;

            // get the root group properties of the entity
            PropertyGroup propertyParent = entity.PropertyRoot;

            return CreateProperty(entity, propertyParent, tcKey, value);
        }

        /// <summary>
        /// Create a property to an object: key - value, 
        /// both are textCode (will be displayed translated depending on the language).
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tcKey"></param>
        /// <param name="tcValue"></param>
        /// <returns></returns>
        public Property CreateProperty(Entity entity, TextCode tcKey, int value)
        {
            // check the entity parent
            if (entity == null)
                return null;

            // get the root group properties of the entity
            PropertyGroup propertyParent = entity.PropertyRoot;

            return CreateProperty(entity, propertyParent, tcKey, value);
        }

        /// <summary>
        /// Create a property to an object: key - value, 
        /// both are textCode (will be displayed translated depending on the language).
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tcKey"></param>
        /// <param name="tcValue"></param>
        /// <returns></returns>
        public Property CreateProperty(Entity entity, TextCode tcKey, bool value)
        {
            // check the entity parent
            if (entity == null)
                return null;

            // get the root group properties of the entity
            PropertyGroup propertyParent = entity.PropertyRoot;

            return CreateProperty(entity, propertyParent, tcKey, value);
        }

        /// <summary>
        /// Create a property to an object: key - value, 
        /// both are textCode (will be displayed translated depending on the language).
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tcKey"></param>
        /// <param name="tcValue"></param>
        /// <returns></returns>
        public Property CreateProperty(Entity entity, PropertyGroup propertyParent, TextCode tcKey, TextCode tcValue)
        {
            // create the property value
            ValTextCodeId valTextCode = new ValTextCodeId();
            valTextCode.TextCodeId = tcValue.Id;

            return CreateProperty(entity, propertyParent, tcKey, valTextCode);
        }

        public Property CreateProperty(Entity entity, PropertyGroup propertyParent, string key, TextCode tcValue)
        {
            // create the property value
            ValTextCodeId valTextCode = new ValTextCodeId();
            valTextCode.TextCodeId = tcValue.Id;

            return CreateProperty(entity, propertyParent, key, valTextCode);
        }


        /// <summary>
        /// Create a property (key-value) undeer a property group in an entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyParent"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Property CreateProperty(Entity entity, PropertyGroup propertyParent, string key, string value)
        {
            // create the property value
            ValString valString = new ValString();
            valString.Value = value;

            return CreateProperty(entity, propertyParent, key, valString);
        }

        /// <summary>
        /// Create a property to an object: key - value, 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tcKey"></param>
        /// <param name="tcValue"></param>
        /// <returns></returns>
        public Property CreateProperty(Entity entity, PropertyGroup propertyParent, TextCode tcKey, string value)
        {
            // create the property value
            ValString valString = new ValString();
            valString.Value = value;

            return CreateProperty(entity, propertyParent, tcKey, valString);
        }

        /// <summary>
        /// Create a property to an object: key - value, 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tcKey"></param>
        /// <param name="tcValue"></param>
        /// <returns></returns>
        public Property CreateProperty(Entity entity, PropertyGroup propertyParent, TextCode tcKey, double value)
        {
            // create the property value
            ValDouble valDouble = new ValDouble();
            valDouble.Value = value;

            return CreateProperty(entity, propertyParent, tcKey, valDouble);
        }

        /// <summary>
        /// Create a property to an object: key - value, 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tcKey"></param>
        /// <param name="tcValue"></param>
        /// <returns></returns>
        public Property CreateProperty(Entity entity, PropertyGroup propertyParent, TextCode tcKey, int value)
        {
            // create the property value
            ValInt valInt = new ValInt();
            valInt.Value = value;

            return CreateProperty(entity, propertyParent, tcKey, valInt);
        }

        /// <summary>
        /// Create a property to an object: key - value, 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tcKey"></param>
        /// <param name="tcValue"></param>
        /// <returns></returns>
        public Property CreateProperty(Entity entity, PropertyGroup propertyParent, TextCode tcKey, bool value)
        {
            // create the property value
            ValBool valBool = new ValBool();
            valBool.Value = value;

            return CreateProperty(entity, propertyParent, tcKey, valBool);
        }

        public PropertyGroup CreatePropertyGroup(Entity entity, string key)
        {
            // check the entity parent
            if (entity == null)
                return null;

            // get the root group properties of the entity
            PropertyGroup propertyParent = entity.PropertyRoot;

            return CreatePropertyGroup(entity, propertyParent, key);
        }

        public PropertyGroup CreatePropertyGroup(Entity entity, TextCode tcKey)
        {
            // check the entity parent
            if (entity == null)
                return null;

            // get the root group properties of the entity
            PropertyGroup propertyParent = entity.PropertyRoot;

            return CreatePropertyGroup(entity, propertyParent, tcKey);
        }

        #endregion

    }
}
