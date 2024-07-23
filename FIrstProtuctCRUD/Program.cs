using FIrstProductCRUD.Data;
using FIrstProductCRUD.Models;
using FIrstProductCRUD.Admin.Pages;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using FIrstProductCRUD.Data.RolesIdentity;
using Microsoft.Extensions.DependencyInjection;


namespace FIrstProductCRUD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            

            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.
                GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(connectionString));

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => { options.LoginPath = "/Account/Register"; });

            builder.Services.AddRazorPages();

            builder.Services.AddAuthorization();

            builder.Services.AddIdentity<WebSiteUser, IdentityRole<int>>(options =>
            options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ApplicationContext>();

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<IServiceStorage, DataBaseStorage>();
            builder.Services.AddScoped<IServiceCartStorage, CartStorage>();
            builder.Services.AddScoped<IServiceOrderStorage, OrderStorage>();
            builder.Services.AddScoped<ApplicationContext>();

            builder.Services.AddControllers(
            options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

            var app = builder.Build();
            var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<WebSiteUser>>();
                new AppDbInitializer().CreateSuperUser(userManager, roleManager).GetAwaiter().GetResult();
            }

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