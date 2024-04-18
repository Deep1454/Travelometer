using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Travelometer.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);


// Deep Lakhani - 101410218
// Naiya Patel - 101416336
// Keyur Odedara - 101413667


// Add services to the container.
builder.Services.AddDbContext<TravelometerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TravelometerDbContext")));

builder.Services.AddDefaultIdentity<TravelometerUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() // AddRoles to enable role management
    .AddEntityFrameworkStores<TravelometerDbContext>();

builder.Services.AddControllersWithViews();

// Configure logging
builder.Logging.ClearProviders(); // Clear default logging providers
builder.Logging.AddConsole(); // Add console logging provider

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
