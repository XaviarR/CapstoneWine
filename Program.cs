using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using CapstoneWine.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using Microsoft.Identity.Client;
using CapstoneWine.Controllers;

var builder = WebApplication.CreateBuilder(args);

//var KeyVaultURL = new Uri(builder.Configuration.GetSection("KeyVaultURL").Value!);
//var azureCredential = new DefaultAzureCredential();
//builder.Configuration.AddAzureKeyVault(KeyVaultURL, azureCredential);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
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
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Creates the roles
using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

	var roles = new[] { "Admin", "Registered" };

	foreach (var role in roles)
	{
		if (!await roleManager.RoleExistsAsync(role))
			await roleManager.CreateAsync(new IdentityRole(role));
	}
}

// Seeds an admin user
using (var scope = app.Services.CreateScope())
{
    var UserManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string email = "admin1@admin.com";
    string password = "Password1!";

    if (await UserManager.FindByEmailAsync(email) == null) //search for account
    {
        var user = new IdentityUser(); //create new user
        user.UserName = email;
        user.Email = email;
		user.EmailConfirmed = true;

        await UserManager.CreateAsync(user, password); //register user

        await UserManager.AddToRoleAsync(user, "Admin"); //assign role to user
    }

}


app.Run();
