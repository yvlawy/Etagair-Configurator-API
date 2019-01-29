using Etagair.Core.System;
using System;
using System.Collections.Generic;

namespace Etagair.Core.Reposit.Contract
{
    public interface IEtagairReposit
    {
        string StringConnection { get; }
        bool IsCreated { get; }

        bool Create();
        bool Open();

        // Build/Edit
        IBuilder Builder { get; }

        // Finder
        IFinder Finder { get; }
    }
}
