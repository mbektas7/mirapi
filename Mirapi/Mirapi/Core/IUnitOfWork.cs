using Mirapi.Core.Domain;
using Mirapi.Core.IRepositories;
using Mirapi.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mirapi.Core
{
    public interface IUnitOfWork : IDisposable
    {
        int Save();
    }
}
