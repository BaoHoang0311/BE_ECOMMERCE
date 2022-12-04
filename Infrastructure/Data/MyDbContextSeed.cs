using Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class MyDbContextSeed
    {
        public static async Task SeedAsync(MyDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                //if (!context.ProductBrands.Any())
                //{
                //    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                //    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                //    foreach (var item in brands)
                //    {
                //        context.ProductBrands.Add(item);
                //    }

                //    await context.SaveChangesAsync();
                //}

                if (!context.products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    
                    foreach (var item in products)
                    {
                        item.Id = Guid.NewGuid().ToString();
                        item.CreatedDate = DateTime.Now;
                        item.ModifiedDate = DateTime.Now;
                        context.products.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

                var logger = loggerFactory.CreateLogger<MyDbContextSeed>();
                logger.LogError($"Seed thanh cong");
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<MyDbContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
