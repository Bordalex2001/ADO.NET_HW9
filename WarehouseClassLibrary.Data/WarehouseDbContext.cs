using ADO.NET_HW9.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_HW9
{
    public class WarehouseDbContext : DbContext
    {
        private static readonly DbContextOptions<WarehouseDbContext> _options;

        static WarehouseDbContext()
        {
            ConfigurationBuilder builder = new();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot? config = builder.Build();
            string? connectionString = config.GetConnectionString("DefaultConnection");

            DbContextOptionsBuilder<WarehouseDbContext> optionsBuilder = new();
            _options = optionsBuilder.UseSqlServer(connectionString).Options;
        }

        public WarehouseDbContext() : base(_options)
        {
            Database.EnsureCreated();
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
