using BeestjeOpJeFeestje.BusinessLayer;
using BeestjeOpJeFeestje.BusinessLayer.DiscountRules;
using BeestjeOpJeFeestje.Domain;
using BeestjeOpJeFeestje.Domain.Interfaces;
using BeestjeOpJeFeestje.Domain.Sql;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BeestjeOpJeFeestje.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime.
        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BeestjeOpJeFeestjeContext>(options =>
                           options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<BeestjeOpJeFeestjeContext>();

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddScoped<IAnimalRepository, AnimalRepositorySql>();
            services.AddScoped<IAnimalTypeRepository, AnimalTypeRepositorySql>();
            services.AddScoped<IBookingRepository, BookingRepositorySql>();
            services.AddScoped<IUserRepository, UserRepositorySql>();
            services.AddScoped<IAccessoriesRepository, AccessoriesRepositorySql>();
            services.AddScoped<ICustomerCardTypeRepository, CustomerCardTypeRepositorySql>();

            services.AddSingleton<IDiscountRule, CustomerCardDiscountRule>();
            services.AddSingleton<IDiscountRule, DuckDiscountRule>();
            services.AddSingleton<IDiscountRule, LetterDiscountRule>();
            services.AddSingleton<IDiscountRule, SameTypeDiscountRule>();
            services.AddSingleton<IDiscountRule, WeekdayDiscountRule>();

            services.AddScoped<IDiscountService>(provider =>
            {
                var discountRules = provider.GetServices<IDiscountRule>().ToList();
                return new DiscountService(discountRules);
            });

            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

        }


        // This method gets called by the runtime.
        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services, BeestjeOpJeFeestjeContext context)
        {
            context.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
            app.UseRequestLocalization();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Boeking}/{action=Index}/{id?}");
            });

            SetupRolesAndUsersAsync(services).GetAwaiter().GetResult();

        }

        private static async Task SetupRolesAndUsersAsync(IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                // Ensure the database is created and migrated
                var context = serviceProvider.GetRequiredService<BeestjeOpJeFeestjeContext>();
                context.Database.Migrate();

                // Create roles if they do not exist
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "admin", "klant" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                // Create a default manager user if it doesn't exist
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                string email = "admin@boerderij.nl";
                string password = "Wachtwoord123!";

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new IdentityUser
                    {
                        UserName = email,
                        Email = email
                    };
                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, "admin");
                }
            }
        }
    }
}
