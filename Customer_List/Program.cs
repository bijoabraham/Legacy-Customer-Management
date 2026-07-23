using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(builder.Environment.ContentRootPath, "App_Data"));
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CustomersWebDemo.DbAccess.CustomerEntitiesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CustomersDB")));
var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customer}/{action=Index}/{id?}");
app.Run();
