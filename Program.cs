using DailyCashCollection.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();

// Register the ApplicationDbContext with a MySQL connection string
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//  options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
//        new MySqlServerVersion(new Version(8, 0, 29))));
builder.Services.AddDbContext<DailyCashCollecion.Pages.ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 29)))); // Update MySQL version if needed

var app = builder.Build();

// Middleware
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Configure default route to go to /Account/Register page
app.MapRazorPages();

app.MapGet("/", async context =>
{
    context.Response.Redirect("/Account/Login");  // Redirect to the Register page
});

// Configure endpoints for Razor Pages
app.MapRazorPages();

app.Run();

//using Microsoft.EntityFrameworkCore;
//using DailyCashCollection.Data;  // Ensure correct namespace for ApplicationDbContext

//var builder = WebApplication.CreateBuilder(args);

//// Register Razor Pages and ApplicationDbContext with MySQL
//builder.Services.AddRazorPages();
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
//        new MySqlServerVersion(new Version(8, 0, 29))));

//var app = builder.Build();

//// Middleware setup
//app.UseStaticFiles();
//app.UseRouting();
//app.UseAuthorization();

//// Route to redirect to /Account/Login page
//app.MapGet("/", async context =>
//{
//    context.Response.Redirect("/Account/Login");
//});

//// Map Razor Pages
//app.MapRazorPages();

//app.Run();
