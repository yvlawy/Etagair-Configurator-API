using Etagair.Core.Reposit.Contract;
using Etagair.Core.Reposit.InMemory;
using Etagair.Core.Reposit.LiteDB;
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
            EtagairCore core = new EtagairCore(reposit);

            //----init the core: create the catalog, becomes the current catalog
            if (core.Init() == null)
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

    }
}
