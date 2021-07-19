using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ITService.Domain.Query.Dto.Auth;
using ITService.Domain.Repositories;
using ITService.Infrastructure;
using ITService.Infrastructure.Repositories;
using ITService.UI.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ITService.Domain.Utilities;
using Stripe;

namespace ITService.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            JwtOptions jwtOptions = new JwtOptions();
            Configuration.GetSection("JwtOptions").Bind(jwtOptions);
            services.AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = "Bearer";
                    option.DefaultScheme = "Bearer";
                    option.DefaultChallengeScheme = "Bearer";
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = jwtOptions.JwtIssuer,
                        ValidAudience = jwtOptions.JwtIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.JwtKey))
                    };

                    cfg.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["Authorization"];
                            return Task.CompletedTask;
                        }
                    };
                });
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
            });
            services.AddSingleton(jwtOptions);

            services.AddDistributedRedisCache(r => { r.Configuration = Configuration["redis:connectionString"]; });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddControllersWithViews();

            services.AddSession(s => s.IdleTimeout = TimeSpan.FromMinutes(30));

            var connectionString = Configuration.GetConnectionString("ItService");
            services.AddDbContext<ITServiceDBContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<JwtAuthFilter>();



            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));

        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<CategoriesRepository>().As<ICategoriesRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<FacilitiesRepository>().As<IFacilitiesRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<ManufacturersRepository>().As<IManufacturersRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<OrdersRepository>().As<IOrdersRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<OrderDetailsRepository>().As<IOrderDetailsRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<ProductsRepository>().As<IProductsRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<RolesRepository>().As<IRolesRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<ServicesRepository>().As<IServicesRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<ShoppingCartsRepository>().As<IShoppingCartsRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<TokenRepository>().As<ITokenRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<UsersRepository>().As<IUsersRepository>().InstancePerLifetimeScope();
            containerBuilder.ConfigureMediator();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseAuthentication();

            app.UseRouting();
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];

            app.UseAuthorization();

            AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=identity}/{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
