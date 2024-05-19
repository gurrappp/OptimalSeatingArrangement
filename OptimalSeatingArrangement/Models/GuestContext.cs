using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using System.Configuration;
using System.Data.Common;

namespace OptimalSeatingArrangement.Models
{
    public class GuestContext : DbContext
    {
        private string connectionString;
        public DbSet<Guest> Guests { get; set; } = null!;

        public GuestContext()
        {
            IConfigurationRoot configuration =
            new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            connectionString = configuration.GetConnectionString("DbContext") ?? "";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            IConfigurationRoot configuration =
                    new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();
            optionsBuilder.UseSqlServer(connectionString);
        }

        //private void SyncGuests()
        //{
        //    foreach (var item in this.ChangeTracker.Entries().Where(_ => _.State == EntityState.Added || _.State == EntityState.Modified))
        //    {
        //        if (item.Entity is Guest guest)
        //        {
        //            guest.GuestPoints[guest.Name] = guest.;
        //        }
        //    }
        //}
    }
}
