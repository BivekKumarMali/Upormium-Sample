using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Upormium.Model.ApplicationClasses;
using Upormium.Model.DbContext;
using Upormium.Model.Models.Users;
using Upormium.Util.SeedDatabase;
using Upormium.Util.StringConstants;

namespace Upormium
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // Add framework services.           
            services.AddDbContext<UpormiumDbContext>(
                options => options.UseNpgsql(Configuration.GetConnectionString("DatabaseConnectionString"),
                x => x.MigrationsAssembly("Upormium")
            ));
            // Setup identity framework.
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<UpormiumDbContext>()
                .AddDefaultTokenProviders();

            #region Added Scoped

            services.AddSingleton<IStringConstant, StringConstant>();
            services.AddScoped<SeedDatabase>();

            #endregion

            #region Default Value
            services.Configure<AdminDetails>(Configuration.GetSection("AdminDetails"));
            services.AddScoped(config => config.GetService<IOptionsSnapshot<AdminDetails>>().Value);
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedDatabase seedDatabase, UpormiumDbContext dbContext)
        {
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=LogIn}/{returnUrl?}");
            });
            dbContext.Database.Migrate();
            seedDatabase.SeedAsync().GetAwaiter().GetResult();
        }
    }
}
