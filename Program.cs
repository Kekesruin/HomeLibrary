using Microsoft.EntityFrameworkCore;
using HomeLibrary.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HomeLibrary.Data.AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();
builder.WebHost.UseUrls("http://0.0.0.0:5123");
var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
