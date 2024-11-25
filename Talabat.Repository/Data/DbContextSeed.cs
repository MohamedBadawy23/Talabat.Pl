using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;     
using Talabat.Repository.Data.Context;

namespace Talabat.Repository.Data
{
    public static class DbContextSeed
    {
        public static async Task SeedAsync(TalabatDbContext dbContext)
        {
            #region Product Brands
            if (!dbContext.ProductBrands.Any())
            {
                var BrandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                if (Brands?.Count > 0)
                {
                    foreach (var Brand in Brands)
                    {
                        await dbContext.Set<ProductBrand>().AddAsync(Brand);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }

            #endregion
            #region Product Types
            if (!dbContext.ProductTypes.Any())
            {
                var TypesData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                if (Types?.Count > 0)
                {
                    foreach (var Type in Types)
                    {
                        await dbContext.Set<ProductType>().AddAsync(Type);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }

            #endregion
            #region Product
            if (!dbContext.Products.Any())
            {
                var ProductData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                if (Products?.Count > 0)
                {
                    foreach (var Product in Products)
                    {
                        await dbContext.Set<Product>().AddAsync(Product);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }

            #endregion
        }
    }
}
