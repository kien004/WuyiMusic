

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WuyiDAL.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Đăng ký constraint
builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer("Data Source=DESKTOP-77H263D\\SQLEXPRESS;Initial Catalog=DB_WuyiMusic2;Integrated Security=True;Trust Server Certificate=True"));

builder.Services.AddHttpClient();

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


// Đăng ký routes ở cấp cao nhất

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Users}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
