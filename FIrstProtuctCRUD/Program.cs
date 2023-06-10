using FIrstProductCRUD.Data;
using FIrstProductCRUD.Models;
using FIrstProductCRUD.Pages.UserPages;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;



namespace FIrstProductCRUD
{
    public class Program
    {
        public static void Main(string[] args)
        {
          
            var builder = WebApplication.CreateBuilder(args);
            
            var connectionString = builder.Configuration.
                GetConnectionString("FIrstProductCRUDContextConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(connectionString));

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => { options.LoginPath = "/Account/Register"; });

            builder.Services.AddRazorPages();

            builder.Services.AddAuthorization();

            builder.Services.
                AddDefaultIdentity<WebSiteUser>(options =>
                options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationContext>(); 

            builder.Services.AddRazorPages();

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
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}