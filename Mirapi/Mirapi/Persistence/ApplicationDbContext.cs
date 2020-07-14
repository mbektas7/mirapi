using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mirapi.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Mirapi.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public virtual DbSet<User> Users { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
  
            optionsBuilder.UseNpgsql(Startup.Configuration.GetConnectionString("DefaultConneciton"));
        }
    }
}
