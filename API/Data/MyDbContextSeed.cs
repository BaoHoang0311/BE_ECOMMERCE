
using API.Data;
using API.Entites;
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

namespace API.Data
{
    public class MyDbContextSeed
    {
        public static async Task SeedAsync(MyDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var customersData1 = File.ReadAllText("../API/Data/SeedData/MOCK_DATA.json");
                var customers1 = JsonSerializer.Deserialize<List<Customer>>(customersData1);
                await context.Customers.AddRangeAsync(customers1);
                await context.SaveChangesAsync();

                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../API/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    
                    foreach (var item in products)
                    {
                        //item.Id = Guid.NewGuid().ToString();
                        item.CreatedDate = DateTime.Now;
                        item.ModifiedDate = DateTime.Now;
                        context.Products.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Customers.Any())
                {
                    var customersData = File.ReadAllText("../API/Data/SeedData/customers.json");
                    var customers = JsonSerializer.Deserialize<List<Customer>>(customersData);

                    foreach (var item in customers)
                    {
                        //item.Id = Guid.NewGuid().ToString();
                        item.CreatedBy = "admin";
                        item.CreatedDate = DateTime.Now;
                        item.ModifiedDate = DateTime.Now;
                        context.Customers.Add(item);
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
