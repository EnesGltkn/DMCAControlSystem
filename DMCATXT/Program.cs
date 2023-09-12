using DMCATXT;
using DMCATXT.Models;
using DMCATXT.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.WindowsServices;
using Serilog;
using System.Net;

var options = new WebApplicationOptions
{
    Args = args,
    ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default
};

var builder = WebApplication.CreateBuilder(options);
builder.Services.AddControllers();
builder.Services.AddTransient<IDataService, DataService>();

// SQL Server baðlantý dizesini alýn
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// DbContext'i ekleyin ve baðlantý dizesini kullanarak yapýlandýrýn
builder.Services.AddDbContext<SqlDBContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<SqlDBContext>();
builder.Services.Configure<FilePath>(builder.Configuration.GetSection("UploadPath"));

builder.Host.UseWindowsService();
builder.Services.AddMvc().AddControllersAsServices();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();

