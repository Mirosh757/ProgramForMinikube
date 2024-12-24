using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace ImageForMinikube
{
    internal class ApplicationContext : DbContext
    {
        string _ipAddress;
        public DbSet<User> Users => Set<User>();
        public ApplicationContext(string ipAddress)
        {
            _ipAddress = ipAddress;
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql($"Host = {_ipAddress}; Username = postgres; Password = 5dartyr5; Database = Users");
        }
    }
}
