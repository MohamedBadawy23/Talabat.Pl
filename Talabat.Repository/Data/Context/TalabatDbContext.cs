using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data.Context
{
    //خلي بالك ان توولز بتنزلها في المكان اللي عامل فيه كونيكشن سترينج في البريزينتيشين بلير بس هخليه واقف علي ريبوساتوري وانا رايح اعمل مايجريشن
    public class TalabatDbContext:DbContext
    {
        public TalabatDbContext(DbContextOptions<TalabatDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand>ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Department { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>()
            //    .HasOne(P=>P.ProductBrand)
            //    .WithMany(P=>P.Products)
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        
    }

}
