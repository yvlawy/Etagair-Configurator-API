using Etagair.Core.Reposit.Contract;
using Etagair.Core.System;
using System;

namespace Etagair.Core.Reposit.InMemory
{
    public class EtagairReposit_InMemory: IEtagairReposit
    {
        SysData _sysData = new SysData();

        public EtagairReposit_InMemory()
        {
            IsCreated = false;
            Builder = new Builder(_sysData);
            Finder = new Finder(_sysData);
        }

        public string StringConnection
        {
            get; private set;
        }

        public bool IsCreated
        {
            get ; private set;
        }

        public IBuilder Builder { get; }

        // Finder
        public IFinder Finder { get; }

        /// <summary>
        /// Create the repository and the default catalog.
        /// </summary>
        /// <returns></returns>
        public bool Create()
        {
            IsCreated = true;
            return true;
        }

        public bool Open()
        {
            if(!IsCreated)
                return false;

            return true;
        }


    }
}
