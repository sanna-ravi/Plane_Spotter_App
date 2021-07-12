using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Spotters.API.Configuration;
using Spotters.Data.Models;
using Spotters.Data.Models.DBContext;
using Spotters.Data.Repository;
using Spotters.Data.UnitOfWork;
using Spotters.Service.Implementation;
using Spotters.Service.Interface;
using Spotters.Service.Mapping;
using Spotters.Service.ServiceModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Spotters.API
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
            services.Configure<DBConfiguration>(Configuration.GetSection("ConnectionString"));

            services.AddDbContext<SpottersDBContext>((provider, options) =>
            options.UseSqlServer(provider.GetRequiredService<IOptions<DBConfiguration>>().Value.DefaultConnectionString));
            services.Configure<BearerTokensOptions>(options => Configuration.GetSection("Bearer").Bind(options));

            services.AddTransient<AUTHIdentityInitializer>();

            services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 4;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.SignIn.RequireConfirmedEmail = false;
            })
                .AddEntityFrameworkStores<SpottersDBContext>()
                .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(o =>
                    o.TokenLifespan = TimeSpan.FromHours(12));

            RsaSecurityKey signingCredentials;
            RSA publicRsa = RSA.Create();
            String publicXml = Path.Combine(Directory.GetCurrentDirectory(), Configuration["Bearer:RSAPublicKey"]);
            var publicKeyXml = File.ReadAllText(publicXml);
            publicRsa.FromXmlString(publicKeyXml);
            signingCredentials = new RsaSecurityKey(publicRsa);


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        RequireExpirationTime = true,
                        RequireSignedTokens = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken,
                                     TokenValidationParameters validationParameters) =>
                        {
                            return notBefore <= DateTime.UtcNow &&
                                   expires >= DateTime.UtcNow;
                        },

                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Bearer:Issuer"],
                        ValidAudience = Configuration["Bearer:Audience"],
                        IssuerSigningKey = signingCredentials
                    };
                });

            services.AddSingleton((provider) => new MapperConfiguration(cfg => cfg.AddProfile(new TransformationDataMappingProfile())).CreateMapper());
             
            services.AddScoped<IBaseRepository<PlaneSpotter>, BaseRepository<PlaneSpotter>>((provider) =>
                new BaseRepository<PlaneSpotter>(provider.GetService<SpottersDBContext>().Set<PlaneSpotter>()));

            services.AddScoped<IUnitOfWork, UnitOfWork>((provider) => new UnitOfWork(provider.GetService<SpottersDBContext>()));

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IAUTHService, AUTHService>();

            services.AddScoped<ISpotterService, SpotterService>();

            services.AddControllers().AddNewtonsoftJson();

        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<SpottersDBContext>())
                {
                    try
                    {
                        //context.Database.EnsureCreated();
                        context.Database.Migrate();
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AUTHIdentityInitializer identityInitializer)
        {
            UpdateDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            var cachePeriod = env.IsDevelopment() ? "600" : "604800";

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), @"images")),
                RequestPath = new PathString("/images"),
                OnPrepareResponse = ctx =>
                {
                    // Requires the following import:
                    // using Microsoft.AspNetCore.Http;
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cachePeriod}");
                }
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            identityInitializer.Seed().Wait();
        }
    }
}
