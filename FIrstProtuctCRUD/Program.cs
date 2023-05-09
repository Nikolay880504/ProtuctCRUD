using FIrstProductCRUD.Data;
using FIrstProductCRUD.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FIrstProductCRUD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationContext>(option => option.UseSqlServer(connection));
            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<IServiceStorage, DataBaseStorage>();
            builder.Services.AddScoped<IServiceCartStorage, CartStorage>();
            builder.Services.AddScoped<IServiceOrderStorage, OrderStorage>();
            builder.Services.AddScoped<ApplicationContext>();
            builder.Services.AddControllers(
            options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}