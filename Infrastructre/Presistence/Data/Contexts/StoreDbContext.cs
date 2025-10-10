using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Presistence.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Data.Contexts
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration<Product>(new ProductConfigurations()); // not recommanded
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //Old

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly); // new Way (We Create Empty Class For Assembly)
        }
    }
}
