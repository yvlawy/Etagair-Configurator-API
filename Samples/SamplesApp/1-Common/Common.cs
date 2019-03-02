using Etagair.Core.System;
using Etagair.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SamplesApp
{
    public class Common
    {
        // the path must exists, location where to put the database file
        string _dbPath = @".\0-Data\";

        /// <summary>
        /// Create an engine.
        /// Removes previous database files.
        /// </summary>
        /// <returns></returns>
        public EtagairEngine CreateEngine()
        {
            Console.WriteLine("Starts the Engine...");
            EtagairEngine engine = new EtagairEngine();


            // delete the previous instance of the db
            Console.WriteLine("Remove the previous data files.");
            if (File.Exists(_dbPath + "etagair.db"))
                File.Delete(_dbPath + "etagair.db");

            // create the database or reuse the existing one
            if (!engine.Init(_dbPath))
            {
                Console.WriteLine("Db initialization Failed.\n");
                return null;
            }

            // the database is created or reused and opened, ready to the execution
            Console.WriteLine("Db initialized with success.\n");
            return engine;
        }

        public void DisplayAllObjects(EtagairEngine engine, Folder folderParent)
        {
            Console.WriteLine("\n----Displays Data:");
            DisplayAllObjects(engine, folderParent,0);
        }

        /// <summary>
        /// Displays the content of an entity (properties childs).
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="entity"></param>
        /// <param name="depth"></param>
        public void DisplayEntity(EtagairEngine engine, Entity entity, int depth, bool displayRootProperty)
        {
            string indent = new string(' ', depth * 2);
            Console.WriteLine(indent + "Entity id: " + entity.Id);

            DisplayPropertyGroup(engine, entity, entity.PropertyRoot, depth + 1, displayRootProperty);
        }

        /// <summary>
        /// Displays the propertyGroup.
        /// (yes or not if its the root propertyGroup of the entity).
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="entity"></param>
        /// <param name="propertyGroupParent"></param>
        /// <param name="depth"></param>
        /// <param name="displayRootProperty"></param>
        public void DisplayPropertyGroup(EtagairEngine engine, Entity entity, PropertyGroup propertyGroupParent, int depth, bool displayRootProperty)
        {
            string indent = new string(' ', depth * 2);

            // display the proerty group itself
            if (displayRootProperty)
            {
                depth++;
                Console.WriteLine(indent + "PG: K=" + GetPropKeyText(engine, propertyGroupParent.Key) + "\\  (child(s) count= " + propertyGroupParent.ListProperty.Count+")");
            }

            foreach (PropertyBase prop in propertyGroupParent.ListProperty)
            {
                // final property?
                Property propChild = prop as Property;
                if(propChild!=null)
                {
                    DisplayProperty(engine, propChild, depth);
                    continue;
                }

                PropertyGroup propertyGroupChild = prop as PropertyGroup;
                if(propertyGroupChild!=null)
                {
                    DisplayPropertyGroup(engine, entity, propertyGroupChild, depth, true);
                }
            }
        }

        public void DisplayProperty(EtagairEngine engine, Property property, int depth)
        {
            string indent = new string(' ', depth * 2);

            // displays the key and the value
            string keyText= indent + "P: K=" + GetPropKeyText(engine, property.Key);
            string valText = " V=" + GetPropValueText(engine, property.Value);
            Console.WriteLine(keyText+ valText);
        }

        #region Private methods
        /// <summary>
        /// Displays entities and folder, then go inside folders to displays childs.
        /// </summary>
        private void DisplayAllObjects(EtagairEngine engine, Folder folderParent, int depth)
        {
            string indent = new string(' ', depth * 2);

            foreach (var obj in folderParent.ListChildId)
            {
                // the object is an entity
                if (obj.Value == ObjectType.Entity)
                {
                    Entity entity = engine.Searcher.FindEntityById(obj.Key);
                    // load the entity
                    Console.WriteLine(indent + "-Ent, id: " + obj.Key);
                    Console.WriteLine(indent + "   prop count: " + entity.PropertyRoot.ListProperty.Count);
                }

                // the object is an entity
                if (obj.Value == ObjectType.Folder)
                {
                    Folder folder = engine.Searcher.FindFolderById(obj.Key);
                    // load the entity
                    Console.WriteLine(indent + "-Fold, id: " + obj.Key + ", Name: " + folder.Name + ", Child count: " + folder.ListChildId.Count);

                    // displays childs objects of this folder
                    DisplayAllObjects(engine, folder, depth + 1);
                }
            }
        }

        private string GetPropKeyText(EtagairEngine engine, PropertyKeyBase propertyKeybase)
        {
            PropertyKeyString propKeyString = propertyKeybase as PropertyKeyString;
            if(propKeyString!=null)
            {
                return "\"" +propKeyString.Key + "\"";
            }

            PropertyKeyTextCode propKeyTextCode= propertyKeybase as PropertyKeyTextCode;
            string propKeyText = "\""+"tc/" + engine.Searcher.FindTextCodeById(propKeyTextCode.TextCodeId).Code+ "\"";

            return propKeyText;
        }

        private string GetPropValueText(EtagairEngine engine, PropertyValueBase propertyValueBase)
        {
            PropertyValueString propValueString = propertyValueBase as PropertyValueString;
            if (propValueString != null)
            {
                return "\""+ propValueString.Value + "\"";
            }

            PropertyValueTextCode propValueTextCode = propertyValueBase as PropertyValueTextCode;
            string propKeyText = "\""+ "tc/" + engine.Searcher.FindTextCodeById(propValueTextCode.TextCodeId).Code+ "\"";

            return propKeyText;
        }

        #endregion
    }
}
