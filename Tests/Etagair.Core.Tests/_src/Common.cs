using Etagair.Core.Reposit.Contract;
using Etagair.Core.Reposit.InMemory;
using Etagair.Core.Reposit.LiteDB;
using Etagair.Core.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Etagair.Core.Tests
{
    public class Common
    {
        // on the first call, remove previous liteDB files
        public static bool FirstCall=true;

        public static IEtagairReposit CreateRepository_InMemory()
        {
            IEtagairReposit reposit = new EtagairReposit_InMemory();
            return reposit;
        }

        public static IEtagairReposit CreateRepository_LiteDB(string repositConfig)
        {
            // remove previous db files
            if (FirstCall)
            {
                FirstCall = false;
                RemoveLiteAllDBFiles();
            }

            string stringConnection = @".\Data\lite_"+ repositConfig+ ".db";

            IEtagairReposit reposit = new EtagairReposit_LiteDB(stringConnection);
            return reposit;
        }

        public static void RemoveLiteAllDBFiles()
        {
            // scan all files
            DirectoryInfo di = new DirectoryInfo(@".\Data");

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }

        //public static void RemoveLiteDBFile(string repositConfig)
        //{
        //    string stringConnection = @".\Data\lite_" + repositConfig + ".db";

        //    if (File.Exists(stringConnection))
        //        File.Delete(stringConnection);
        //}

        public static EtagairCore CreateCoreInMemory()
        {
            IEtagairReposit reposit = Common.CreateRepository_InMemory();
            return Common.CreateCore(reposit);
        }

        public static EtagairCore CreateCore(IEtagairReposit reposit)
        {
            // create the core configurator, inject the concrete repository
            EtagairCore core = new EtagairCore();

            //----init the core: create the catalog, becomes the current catalog
            if (!core.Init(reposit))
                return null;

            // configure the catalog

            return core;
        }



        public static EtagairCore CreateCore(string repositConfig)
        {
            IEtagairReposit reposit = null;

            if (string.IsNullOrWhiteSpace(repositConfig) || repositConfig == "InMemory")
                reposit = Common.CreateRepository_InMemory();
            else
                reposit = Common.CreateRepository_LiteDB(repositConfig);

            return Common.CreateCore(reposit);
        }

        /// <summary>
        /// Return the property key content as a string.
        /// can be a string or a textCodeId.
        /// </summary>
        /// <param name="propertyKeyBase"></param>
        /// <returns></returns>
        public static string GetPropertyKeyContent(PropertyKeyBase propertyKeyBase)
        {
            PropertyKeyString propKeyString = propertyKeyBase as PropertyKeyString;
            if (propKeyString != null)
                return propKeyString.Key;

            PropertyKeyTextCode propKeyTextCode = propertyKeyBase as PropertyKeyTextCode;
            if (propKeyTextCode != null)
                return propKeyTextCode.TextCodeId;

            return null;
        }

        /// <summary>
        /// Return the property value content as a string.
        /// Can be a string, a textCodeId.
        /// Later: image,...
        /// 
        /// </summary>
        /// <param name="propertyKeyBase"></param>
        /// <returns></returns>
        public static string GetPropertyValueContent(IValue value)
        {
            ValString propValueString = value as ValString;
            if (propValueString != null)
                return propValueString.Value;

            ValTextCodeId propValueTextCode = value as ValTextCodeId;
            if (propValueTextCode != null)
                return propValueTextCode.TextCodeId;

            // TODO: implement others types: Image, ImageCode,...
            return null;
        }

    }
}
