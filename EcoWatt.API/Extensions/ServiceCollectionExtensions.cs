using EcoWatt.API.Configuration;
using EcoWatt.Database;
using EcoWatt.Model;
using EcoWatt.Repository;
using EcoWatt.Service.UsuarioUseServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace EcoWatt.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection service)
        {
            

            service.AddScoped<IUsuarioUseService, UsuarioUseService>();

            
            return service;
        }

        public static IServiceCollection AddDBContexts(this IServiceCollection service, AppConfiguration appConfiguration)
        {

            service.AddDbContext<FIAPDBContext>(options =>
            {
                options.UseOracle(appConfiguration.ConnectionStrings.OracleFIAP,
                    builder => builder.MigrationsAssembly("EcoWatt.Database"));
            });

            service.AddDbContext<AuthorizationDbContext>(options =>
            {
                options.UseInMemoryDatabase("UserAccess");
            });

            return service;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection service)
        {
            
            service.AddScoped<IRepository<Usuario>, Repository<Usuario>>();
            service.AddScoped<IRepository<Bateria>, Repository<Bateria>>();
            service.AddScoped<IRepository<UsuarioUse>, Repository<UsuarioUse>>();
            

            return service;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection service, AppConfiguration appConfiguration)
        {

            service.AddSwaggerGen(swagger =>
            {
                swagger.TagActionsBy(api =>
                {
                    var identityEndpoints = new[] { "logout", "confirmEmail", "resendConfirmationEmail", "forgotPassword", "resetPassword", "manage", "refresh", "register", "login", "logout", "confirm-email", "forgot-password", "reset-password", "change-password" };

                    if (api.RelativePath != null && identityEndpoints.Any(endpoint => api.RelativePath.Contains(endpoint)))
                    {
                        return new[] { "Autenticação e Autorização" };
                    }

                    return new[] { api.GroupName ?? api.ActionDescriptor.RouteValues["controller"] };
                });

                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = appConfiguration.Swagger.Title,
                    Version = "v1",
                    Description = appConfiguration.Swagger.Description,
                    Contact = new OpenApiContact()
                    {
                        Email = appConfiguration.Swagger.Email,
                        Name = appConfiguration.Swagger.Name
                    }
                }
                );

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                swagger.IncludeXmlComments(xmlPath);
            });

            return service;
        }


        public static IServiceCollection AddHealthChecks(this IServiceCollection services, AppConfiguration appConfiguration)
        {

            services
            .AddHealthChecks()
            .AddOracle(appConfiguration.ConnectionStrings.OracleFIAP);
                
            return services;
        }
        public static IServiceCollection AddAuthenticationAndAutorization(this IServiceCollection services)
        {
            services.AddAuthentication(); 
            services.AddAuthorization(); 

            services.AddIdentityApiEndpoints<AccessUser>()
                .AddEntityFrameworkStores<AuthorizationDbContext>();

            return services;
        }

    }
}
