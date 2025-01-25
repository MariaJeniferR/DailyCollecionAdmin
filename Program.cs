using DailyCashCollection.Pages.Account;
using Microsoft.EntityFrameworkCore;
using DailyCashCollection.Pages; // This is correct if it's in the Pages folder

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();

// Register the ApplicationDbContext with a MySQL connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
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

