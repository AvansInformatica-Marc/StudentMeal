using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentMeal.AppLogic;
using StudentMeal.DataAccess;
using StudentMeal.DataAccess.Database;

namespace StudentMeal {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddTransient<StudentMealDbContext>();

            services.AddTransient<IRepository, FakeDataRepository>();
            services.AddTransient<StudentMealManager>();

            //services.AddTransient<UserDbContext>();

            //services.AddDbContext<StudentMealDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            services.AddDbContext<UserDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("defaultAuthConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<UserDbContext>().AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            } else {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseAuthentication();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "defaultWithId",
                    template: "{controller=Home}/{action=Index}/{id:int}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }
    }
}
