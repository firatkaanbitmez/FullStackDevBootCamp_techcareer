using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ShopAppProject.Data;
using System;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(options =>
{
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("mydb");
    options.UseSqlite(connectionString);
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(30); //30gün boyunca giriş bilgisini tut 30gün geçince cookie bilgisi silinir
});

var app = builder.Build();

// Create roles and a default admin user on application startup
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    try
    {
        // RoleManager kullanarak rolleri oluştur
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        // "Company" adlı rolü oluştur
        if (!await roleManager.RoleExistsAsync("Company"))
        {
            var role = new IdentityRole("Company");
            await roleManager.CreateAsync(role);
        }
        // "Customer" adlı rolü oluştur
        if (!await roleManager.RoleExistsAsync("Customer"))
        {
            var role = new IdentityRole("Customer");
            await roleManager.CreateAsync(role);
        }

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            var role = new IdentityRole("Admin");
            await roleManager.CreateAsync(role);
        }

        // Default admin kullanıcısını oluştur

        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var defaultAdminEmail = "admin@example.com"; // Admin kullanıcının e-posta adresi
        var defaultAdmin = await userManager.FindByEmailAsync(defaultAdminEmail);

        if (defaultAdmin == null)
        {
            var adminUser = new ApplicationUser
            {
                UserName = defaultAdminEmail,
                Email = defaultAdminEmail,
                // Diğer gerekli özellikleri de ekleyebilirsiniz
            };

            var result = await userManager.CreateAsync(adminUser, "19051905Asd.");

            if (result.Succeeded)
            {
                // Admin kullanıcısına "Entrepreneur" rolünü ata
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
    catch (Exception)
    {
        // Hata yönetimi burada
    }
}

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

app.UseAuthentication(); // Ekledik
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
