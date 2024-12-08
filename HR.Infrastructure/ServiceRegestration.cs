using HR.Domain.Classes;
using HR.Domain.Classes.Identity;
using HR.Domain.Enums;
using HR.Domain.Helpers;
using HR.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;

namespace HR.Infrastructure
{
    public static class ServiceRegestration
    {
        public static IServiceCollection AddServiceRegisteration(this IServiceCollection services, IConfiguration configuration)
        {

            #region Identity Configurations

            services.AddIdentity<Employee, Role>(option =>
            {
                // Password settings.
                //option.Password.RequireDigit = true;
                //option.Password.RequireLowercase = true;
                //option.Password.RequireNonAlphanumeric = true;
                //option.Password.RequireUppercase = true;
                //option.Password.RequiredLength = 6;
                //option.Password.RequiredUniqueChars = 1;

                //// Lockout settings.
                //option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                //option.Lockout.MaxFailedAccessAttempts = 5;
                //option.Lockout.AllowedForNewUsers = true;

                //// User settings.
                //option.User.AllowedUserNameCharacters =
                //"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                //option.User.RequireUniqueEmail = true;
                //option.SignIn.RequireConfirmedEmail = false;

            }).AddEntityFrameworkStores<HRdbContext>().AddDefaultTokenProviders();

            #endregion

            #region JWT Authentication

            var section = configuration.GetSection("jwtSettings");
            if (!section.Exists())
            {
                throw new Exception("jwtSettings section is missing in appsettings.json");
            }

            var jwtSettings = section.Get<JwtSettings>();
            services.AddSingleton(jwtSettings);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = jwtSettings.ValidateIssuer,
                   ValidIssuers = new[] { jwtSettings.Issuer },
                   ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                   ValidAudience = jwtSettings.Audience,
                   ValidateAudience = jwtSettings.ValidateAudience,
                   ValidateLifetime = jwtSettings.ValidateLifeTime,
               };
           });
            #endregion

            #region Swagger Configurations

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("All", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "HR Managment System",
                    Description = "All Endpoints",

                });
                c.SwaggerDoc("Employees", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Employees",
                    Description = "Employees Web Api in Asp.net Core Version .net8",

                });
                c.SwaggerDoc("Departments", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Departments",
                    Description = "Departments Web Api in Asp.net Core Version .net7",

                });
                c.SwaggerDoc("Attendances", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Attendances",
                    Description = "Attendances Web Api in Asp.net Core Version .net7",

                });
                c.SwaggerDoc("LeaveRequests", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "LeaveRequests",
                    Description = "LeaveRequests Web Api in Asp.net Core Version .net7",

                });
                c.SwaggerDoc("Notifications", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Notifications",
                    Description = "Notifications Web Api in Asp.net Core Version .net7",

                });
                c.SwaggerDoc("Payroll", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Payroll",
                    Description = "Payroll Web Api in Asp.net Core Version .net7",

                });
                c.SwaggerDoc("PerformanceReview", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "PerformanceReview",
                    Description = "PerformanceReview Web Api in Asp.net Core Version .net7",

                });
                c.SwaggerDoc("Authentication", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Authentication",
                    Description = "Authentication Web Api in Asp.net Core Version .net7",

                });
                c.SwaggerDoc("Autherization", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Autherization",
                    Description = "Autherization Web Api in Asp.net Core Version .net7",

                });


                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var groupName = apiDesc.GroupName;
                    if (docName == "All")
                        return true;
                    return docName == groupName;
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                }



                // Add this line to display enum names in Swagger UI
                c.SchemaGeneratorOptions = new SchemaGeneratorOptions
                {
                    UseAllOfForInheritance = true,
                    UseOneOfForPolymorphism = true,
                    SupportNonNullableReferenceTypes = true
                };

                c.MapType<NotificationType>(() => new OpenApiSchema
                {
                    Type = "string",
                    Enum = Enum.GetNames(typeof(NotificationType))
               .Select(name => (IOpenApiAny)new OpenApiString(name)) // Explicit cast to IOpenApiAny
               .ToList()
                });




                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                                         {
                                            {
                                                new OpenApiSecurityScheme
                                                {
                                                    Reference = new OpenApiReference
                                                    {
                                                        Type = ReferenceType.SecurityScheme,
                                                        Id = JwtBearerDefaults.AuthenticationScheme
                                                    }
                                                },
                                                Array.Empty<string>()
                                            }
                                        });

                c.EnableAnnotations();
            });

            #endregion

            return services;
        }
    }
}
