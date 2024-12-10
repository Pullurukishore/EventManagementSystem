using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Models;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args); // Declare builder only once

// Add services to the container
builder.Services.AddControllersWithViews(); // Add support for MVC
builder.Services.AddDistributedMemoryCache(); // For session state

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Optional session timeout
    options.Cookie.HttpOnly = true; // Protect the session cookie
    options.Cookie.IsEssential = true; // Mark session cookie as essential
});

// Add DbContext with connection string from appsettings.json
builder.Services.AddDbContext<EventManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build(); // Build the application

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Production error page
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Serve static files

app.UseRouting();

app.UseSession(); // Enable session state

app.UseAuthorization(); // Enable authorization middleware

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
app.UseStaticFiles();

app.UseStaticFiles();  // Default folder wwwroot
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "assets")),
    RequestPath = "/assets"
});
