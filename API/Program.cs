﻿using API.Extension;
using API.Helpers;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var services = builder.Services;
            services.AddDbContext<MyDbContext>(x =>
                x.UseSqlServer(builder.Configuration.GetConnectionString("MyDb")));
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddScoped<Saveimage>();
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            // product
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IEntityBaseRepository<>),typeof(EntityBaseRepository<>) );

            // Configure the HTTP request pipeline.
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();

            // auto update migrations, cd API ->dotnet watch run 
            // auto cập nhật ~ thằng migrations pending khi pull form github
            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = service.GetRequiredService<MyDbContext>();
                await context.Database.MigrateAsync();
                //Seed product data
                await MyDbContextSeed.SeedAsync(context, loggerFactory);

                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogWarning("Success Migrations");
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred during migration");
            }

            app.Run();
        }
    }
}