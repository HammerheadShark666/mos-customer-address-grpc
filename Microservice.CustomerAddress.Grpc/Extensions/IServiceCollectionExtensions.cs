using Microservice.Customer.Address.Api.Address.Api.Extensions;
using Microservice.Customer.Address.Grpc.Data.Contexts;
using Microservice.Customer.Address.Grpc.Data.Repository;
using Microservice.Customer.Address.Grpc.Data.Repository.Interfaces;
using Microservice.Customer.Address.Grpc.Helpers;
using Microservice.Customer.Address.Grpc.Helpers.Interceptors;
using Microservice.Customer.Address.Grpc.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Microservice.Customer.Address.Grpc.Extensions;

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

    public static void ConfigureDatabaseContext(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContextFactory<CustomerAddressDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(Grpc.Helpers.Constants.DatabaseConnectionString),
            options => options.EnableRetryOnFailure()));
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