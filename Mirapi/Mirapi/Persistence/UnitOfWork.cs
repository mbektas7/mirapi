using Mirapi.Core;
using Mirapi.Core.Domain;
using Mirapi.Core.IRepositories;
using Mirapi.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Mirapi.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            if (_context == null)
                throw new ArgumentNullException("dbContext can not be null.");
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        private IRepository<User> _users;
        private IRepository<BlackListToken> _tokenBlacklist;


        public IRepository<User> Users { get { return _users = _users ?? new Repository<User>(_context); } }
        public IRepository<BlackListToken> TokenBlacklist { get { return _tokenBlacklist = _tokenBlacklist ?? new Repository<BlackListToken>(_context); } }

    }
}
