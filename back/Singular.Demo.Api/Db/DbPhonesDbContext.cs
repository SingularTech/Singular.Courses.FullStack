using Microsoft.EntityFrameworkCore;
using Singular.Demo.Api.Models;

namespace Singular.Demo.Api.Db
{
    public class DbPhonesDbContext : DbContext
    {
        public DbPhonesDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //optionsBuilder.UseSqlite();
            optionsBuilder.UseSqlServer();
        }

        public DbSet<Phone> Phones { get; set; }
    }
}
