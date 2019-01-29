using Etagair.Core.Reposit.Contract;
using LiteDB;
using System;
using System.IO;

namespace Etagair.Core.Reposit.LiteDB
{
    public class EtagairReposit_LiteDB :IEtagairReposit
    {
        string _stringConnection;

        SysData _sysData = new SysData();

        public EtagairReposit_LiteDB(string stringConnection)
        {
            _stringConnection = stringConnection;

            Finder = new Finder(_sysData);
            //IsCreated = false;
            Builder = new Builder(_sysData);
        }

        public string StringConnection
        {
            get { return _stringConnection; }
            set { _stringConnection = value; }
        }

        public bool IsCreated
        {
            get { return DetectIfDbExists(); }
        }

        public IBuilder Builder { get; }

        // Finder
        public IFinder Finder { get; }

        /// <summary>
        /// Create the repository, is empty.
        /// </summary>
        /// <returns></returns>
        public bool Create()
        {
            if (_sysData.DBEngine != null)
                // a db is already open
                return false;

            if (string.IsNullOrWhiteSpace(StringConnection))
                return false;

            // must not exists
            if (File.Exists(StringConnection))
                return false;

            try
            {
                // create the directory
                //Directory.CreateDirectory(StringConnection);

                // @"C:\Temp\MyData.db"
                var dbEngine = new LiteRepository(StringConnection);

                _sysData.SetDBEngine(dbEngine);

                // create the descriptor
                CreateStructureDescriptor();

                return true;
            }
            catch (Exception e)
            {
                // faild to create the directory
                return false;
            }
        }

        public bool Open()
        {
            try
            {
                // must exists
                if (!File.Exists(StringConnection))
                    return false;

                // maybe it is already opened
                if (_sysData.DBEngine != null)
                    return true;

                // create a db
                var dbEngine = new LiteRepository(StringConnection);
                _sysData.SetDBEngine(dbEngine);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Close()
        {
            try
            {
                if (_sysData.DBEngine == null)
                    return false;

                // dispose the db engine
                _sysData.DBEngine.Dispose();
                _sysData.SetDBEngine(null);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool DetectIfDbExists()
        {
            if (!File.Exists(StringConnection))
                return false;

            return true;
        }

        // create the descriptor
        private void CreateStructureDescriptor()
        {
            StructureDescriptor desc = new StructureDescriptor();
            desc.Id = Guid.NewGuid().ToString();

            // save it
            _sysData.DBEngine.Insert<StructureDescriptor>(desc);
        }

    }
}
