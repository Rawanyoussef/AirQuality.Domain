using AirQuilty.Domain.Entitiy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirQuality.Infrastructure.Context
{
    public class AirContext : DbContext
    {
        public AirContext(DbContextOptions<AirContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AirQualitySnapshot>().HasKey(a => a.Id);
        }

        public DbSet<AirQualitySnapshot> AirQualitySnapshots { get; set; }
    }
}
