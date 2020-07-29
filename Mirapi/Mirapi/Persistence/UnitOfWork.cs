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
        private IRepository<Post> _post;
        private IRepository<Comments> _comments;
        private IRepository<Cars> _cars;
        private IRepository<Brands> _brands;




        public IRepository<User> Users { get { return _users = _users ?? new Repository<User>(_context); } }
        public IRepository<Post> Post { get { return _post = _post ?? new Repository<Post>(_context); } }

        public IRepository<Comments> Comments { get { return _comments = _comments ?? new Repository<Comments>(_context); } }
        public IRepository<Cars> Cars { get { return _cars = _cars ?? new Repository<Cars>(_context); } }

        public  IRepository<Brands> Brands { get { return _brands = _brands ?? new Repository<Brands>(_context); } }

        public IRepository<BlackListToken> TokenBlacklist { get { return _tokenBlacklist = _tokenBlacklist ?? new Repository<BlackListToken>(_context); } }

    }
}
