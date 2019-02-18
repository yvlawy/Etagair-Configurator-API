using Etagair.Core;
using Etagair.Core.Reposit.Contract;
using Etagair.Core.Reposit.LiteDB;
using System;
using System.IO;

namespace Etagair.Engine
{
    /// <summary>
    /// The Etagair core API.
    /// Use a concrete repository: InMemory or LiteDB.
    /// </summary>
    public class EtagairEngine : EtagairCore
    {
        string _dbPath = "";

        /// <summary>
        /// The default name of the database file.
        /// </summary>
        string _dbFileName = "etagair.db";

        public EtagairEngine()
        {
            
        }

        /// <summary>
        /// Initialization of the engine.
        /// Create a new database or open the existing one.
        /// </summary>
        /// <returns></returns>
        public bool Init(string dbPath)
        {
            // the path should exists
            if (!Directory.Exists(dbPath))
                return false;

            _dbPath = dbPath;
            string stringConnection = Path.Combine(_dbPath, _dbFileName);

            IEtagairReposit reposit  = new EtagairReposit_LiteDB(stringConnection);

            return base.Init(reposit);
        }
    }
}
