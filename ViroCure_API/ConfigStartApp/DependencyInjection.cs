using AutoMapper;
using BusinessLayer.CategoryBusiness;

using DataAccessLayer;
using DataAccessLayer.Entities;
using BusinessLayer.ConfigHelper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using BusinessLayer.Helper;
using Mapper = BusinessLayer.Helper.Mapper;

namespace ViroCure_API.ConfigStartApp
{
    public static class DependencyInjection
    {
        public static void InstallService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true; ;
                options.LowercaseQueryStrings = true;
            });

            // Database Context
            services.AddDbContext<ViroCureFal2024dbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("LocalDatabase"));
            });

            // Add db factory, unitofwork, generic repo
            services.AddScoped<Func<ViroCureFal2024dbContext>>((provider) => () => provider.GetService<ViroCureFal2024dbContext>());
            //services.AddScoped<DbFactory>();
            services.AddScoped<UnitOfWork>();
            services.AddScoped<IViroCureUserService,ViroCureUserService>();
            services.AddScoped<IPersonService,PersonService>();
            // AutoMapper
            services.AddAutoMapper(typeof(Mapper));

            // Other Service
            
        }
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings.GetSection("SecretKey").Value);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }
        public static void ConfigureSwaggerServices(this IServiceCollection services, string appName)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = appName, Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                });
            });

            services.AddEndpointsApiExplorer();
        }
    }
   
}
