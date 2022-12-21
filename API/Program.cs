using API.Data;
using API.Helpers;
using API.Helpers.Nlog;
using API.Repository;
using API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Fluent;
using System;
using System.IO;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", option => option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            // Add services to the container.
            var services = builder.Services;
            services.AddDbContext<MyDbContext>(x =>
                x.UseSqlServer(builder.Configuration.GetConnectionString("MyDb")));

            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped(typeof(IEntityBaseRepository<>), typeof(EntityBaseRepository<>));

            services.AddScoped<IProductRepository, ProductServices>();
            services.AddScoped<ICustomerRepository,CustomerServices>();

            services.AddScoped<IOrderRepository, OrderServices>();
            services.AddScoped<IOrderDetailRepository, OrderDetailServices>();

            services.AddScoped<IBuyOrderRepository, BuyOrderServices>();
            services.AddScoped<IBuyOrderDetailRepository, BuyOrderDetailServices>();

            //Nlog
            services.AddSingleton<ILoggerManager, LoggerManager>();


            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            //System.IO.FileNotFoundException: 'Failed to load NLog LoggingConfiguration. Searched the following locations:
            //-C:\NglehoangBao\BE_ecommerce\API / nlog.config


            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), @"\Helpers\Nlog\nlog.config"));

            // Configure the HTTP request pipeline.
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseHttpsRedirection();

            app.UseAuthorization();

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
// B1- F