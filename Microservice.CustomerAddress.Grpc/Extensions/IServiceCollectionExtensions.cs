using Microservice.CustomerAddress.Grpc.Data.Context;
using Microservice.CustomerAddress.Grpc.Data.Repository;
using Microservice.CustomerAddress.Grpc.Data.Repository.Interfaces;
using Microservice.CustomerAddress.Grpc.Helpers;
using Microservice.CustomerAddress.Grpc.Helpers.Exceptions;
using Microservice.CustomerAddress.Grpc.Helpers.Interceptors;
using Microservice.CustomerAddress.Grpc.Helpers.Providers;
using Microservice.CustomerAddress.Grpc.Middleware;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Microservice.CustomerAddress.Grpc.Extensions;

public static class IServiceCollectionExtensions
{
    public static void ConfigureExceptionHandling(this IServiceCollection services)
    {
        services.AddTransient<ExceptionHandlingMiddleware>();
    }

    public static void ConfigureJwt(this IServiceCollection services)
    {
        services.AddJwtAuthentication();
    }

    public static void ConfigureDI(this IServiceCollection services)
    {
        services.AddScoped<ICustomerAddressRepository, CustomerAddressRepository>();
        services.AddMemoryCache();
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }

    public static void ConfigureSqlServer(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        if (environment.IsProduction())
        {
            var connectionString = configuration.GetConnectionString(Constants.AzureDatabaseConnectionString)
                    ?? throw new DatabaseConnectionStringNotFound("Production database connection string not found.");

            AddDbContextFactory(services, SqlAuthenticationMethod.ActiveDirectoryManagedIdentity, new ProductionAzureSQLProvider(), connectionString);
        }
        else if (environment.IsDevelopment())
        {
            var connectionString = configuration.GetConnectionString(Constants.LocalDatabaseConnectionString)
                    ?? throw new DatabaseConnectionStringNotFound("Development database connection string not found.");

            AddDbContextFactory(services, SqlAuthenticationMethod.ActiveDirectoryServicePrincipal, new DevelopmentAzureSQLProvider(), connectionString);
        }
    }

    private static void AddDbContextFactory(IServiceCollection services, SqlAuthenticationMethod sqlAuthenticationMethod, SqlAuthenticationProvider sqlAuthenticationProvider, string connectionString)
    {
        services.AddDbContextFactory<CustomerAddressDbContext>(options =>
        {
            SqlAuthenticationProvider.SetProvider(
                    sqlAuthenticationMethod,
                    sqlAuthenticationProvider);
            var sqlConnection = new SqlConnection(connectionString);
            options.UseSqlServer(sqlConnection);
        });
    }

    public static void ConfigureGrpc(this IServiceCollection services)
    {
        services.AddGrpc(options =>
        {
            options.Interceptors.Add<ServerLoggerInterceptor>();
        });
        services.AddGrpcReflection();
        services.AddGrpc().AddJsonTranscoding();
    }

    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapperProfile)));
    }
}