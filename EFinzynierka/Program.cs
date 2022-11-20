using EFinzynierka;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

void ConfigureServices(IServiceCollection services)
{
    services.AddMvc();
    services.AddControllersWithViews();
    services.AddDbContext<DbEFinzynierkaContext>(builder =>
    {
        builder.UseSqlServer("Data Source=localhost;Initial Catalog=DbEFinzynierka;Integrated Security=True");
    });
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
