using Autofac;
using Autofac.Extensions.DependencyInjection;
using Libary.Business.DependencyResolvers;
using Serilog;
using Serilog.Core;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
#region Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder =>
{
builder.RegisterModule(new AutofacBusinessModule());
}); 
#endregion
// Add services to the container.
builder.Services.AddControllersWithViews();

Logger log = new LoggerConfiguration()
   .WriteTo.MSSqlServer(
              connectionString: "server=DESKTOP-VTNRLAJ; database=LibaryDb; TrustServerCertificate=True;integrated security=true", // DbContext'ta tanýmlanan baðlantý dizesi
              tableName: "logs",
              autoCreateSqlTable: true)
   .CreateLogger();

builder.Host.UseSerilog(log);
var cultureInfo = new CultureInfo("tr-TR");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=GetAll}/{id?}");

app.Run();
