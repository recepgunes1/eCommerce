using eCommerce.Data.Extensions;
using eCommerce.Service.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.LoadDataLayerExtensions(builder.Configuration);
builder.Services.LoadServiceLayerExtensions();

var app = builder.Build();

app.MapDefaultControllerRoute();

app.Run();
