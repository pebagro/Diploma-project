using EFinzynierka;
using EFinzynierka.Models;
using EFinzynierka.Services;
using EFinzynierka.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EFinzynierka.Controllers;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;


internal class Program
{

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var provider = builder.Services.BuildServiceProvider();
        var configuration = provider.GetRequiredService<IConfiguration>();
        builder.Services.AddLogging();
        builder.Services.AddSingleton(configuration.GetValue<string>("RFIDReader:PortName"));
        builder.Services.AddScoped<IEmployeeservices, Employeeservices>();
        builder.Services.AddScoped<IEmployeeDataService, EmployeeDataService>();
        builder.Services.AddTransient<ISchedulerservices, Schedulerservices>();
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication();
        builder.Services.AddMvc();
        
        
        builder.Services.AddDbContext<DbEFinzynierkaContext>(builder =>
        {
            builder.UseSqlServer(@"Data Source=DESKTOP-BFG7ULK\MSSQLSERVER01;Initial Catalog=DbEFinzynierka;Integrated Security=True;Encrypt=false;TrustServerCertificate=true");
        });
        builder.Services.AddHostedService<RFIDReaderService>();
        builder.Services.AddIdentity<UserModel, IdentityRole>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 5;

        }).AddEntityFrameworkStores<DbEFinzynierkaContext>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseStaticFiles();
        app.UseHttpsRedirection();
        app.UseRouting();
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Account}/{action=Login}/{id?}");

        app.Run();

    }
}