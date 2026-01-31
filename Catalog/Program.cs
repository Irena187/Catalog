using Catalog.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Catalog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            // DbContext конфигурация
            builder.Services.AddDbContext<CatalogContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            // Identity конфигурация - ВАЖНО: Използвай ApplicationUser, не IdentityUser!
            builder.Services.AddIdentity<Userr, IdentityRole>(options =>
            {
                // Настройки за паролата
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;

                // Настройки за потребителя
                options.User.RequireUniqueEmail = true;

                // Настройки за заключване на акаунт
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
            .AddEntityFrameworkStores<CatalogContext>()
            .AddDefaultTokenProviders();

            // Конфигурация на cookie за Razor Pages
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";        // Път към Login страница
                options.LogoutPath = "/Account/Logout";      // Път към Logout
                options.AccessDeniedPath = "/Account/AccessDenied";  // Достъп отказан
                options.ExpireTimeSpan = TimeSpan.FromHours(24);
                options.SlidingExpiration = true;
            });

            var app = builder.Build();

            // Seed роли при стартиране
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                SeedRolesAsync(services).GetAwaiter().GetResult();
                //  await SeedRolesAsync(services);

                SeedAdminUserAsync(services).GetAwaiter().GetResult();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // app.UseHttpsRedirection();  // Остава коментирано, ако не искаш HTTPS
            app.UseStaticFiles();
            app.UseRouting();

            // ВАЖНО: Authentication преди Authorization!
            app.UseAuthentication();  // Добавено
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }

        // Метод за създаване на роли
        private static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "Manager", "User" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
        private static async Task SeedAdminUserAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<Userr>>();
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            string adminEmail = "admin@catalog.com";
            string adminPassword = "Admin123!";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var user = new Userr
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(user, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    logger.LogInformation($"Admin потребител създаден: {adminEmail}");
                }
                else
                {
                    logger.LogError("Грешка при създаване на Admin потребител");
                }
            }
        }

    }
}

