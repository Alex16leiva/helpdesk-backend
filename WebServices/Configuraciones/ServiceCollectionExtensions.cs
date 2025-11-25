using Aplicacion.Services;
using Aplicacion.Services.BConocimiento;
using Aplicacion.Services.Tickets;
using Infraestructura.Context;
using Infraestructura.Core.Identity;
using Infraestructura.Core.Jwtoken;
using Infraestructura.Core.RestClient;
using Microsoft.EntityFrameworkCore;

namespace WebServices.Configuraciones
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<SecurityAplicationService>();
            services.AddScoped<TicketApplicationService>();
            services.AddScoped<ArticuloAppService>();
            services.AddScoped<CategoriaAppService>();

            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("conectionDataBase");

            services.AddDbContext<MyContext>(options =>
                options.UseSqlServer(connectionString), ServiceLifetime.Transient);

            services.AddTransient<IDataContext, MyContext>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddTransient<ITokenService, JwtTokenService>();

            RestClientFactory.SetCurrent(new HttpRestClientFactory());
            IdentityFactory.SetCurrent(new ADOIdentityGeneratorFactory());

            return services;
        }

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            const string AllowAllOriginsPolicy = "AllowAllOriginsPolicy";

            services.AddCors(options =>
            {
                options.AddPolicy(AllowAllOriginsPolicy, builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            return services;
        }
    }
}
