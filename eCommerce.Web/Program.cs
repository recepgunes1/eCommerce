using eCommerce.Data.Context;
using eCommerce.Data.Extensions;
using eCommerce.Entity.Entities;
using eCommerce.Service.Extensions;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

builder.Services.LoadDataLayerExtensions(builder.Configuration);

builder.Services.LoadServiceLayerExtensions();

builder.Services.AddIdentity<User, Role>()
    .AddRoleManager<RoleManager<Role>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = new PathString("/Auth/Home/Login");
    config.LogoutPath = new PathString("/Auth/Home/Logout");
    config.Cookie = new()
    {
        Name = "eCommerce",
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        SecurePolicy = CookieSecurePolicy.SameAsRequest
    };
    config.SlidingExpiration = true;
    config.ExpireTimeSpan = TimeSpan.FromDays(7);
    config.AccessDeniedPath = new PathString("/Auth/Home/AccessDenied");
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapAreaControllerRoute(name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller}/{action}/{id?}"
    );

app.MapAreaControllerRoute(name: "Auth",
    areaName: "Auth",
    pattern: "Auth/{controller}/{action}"
    );

app.MapAreaControllerRoute(name: "Cart",
    areaName: "Cart",
    pattern: "Cart/{controller}/{action}"
    );

app.MapAreaControllerRoute(name: "Profile",
    areaName: "Profile",
    pattern: "Profile/{controller}/{action}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapDefaultControllerRoute();

app.Run();
