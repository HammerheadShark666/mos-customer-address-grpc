using Microservice.CustomerAddress.Grpc.Service;

namespace Microservice.CustomerAddress.Grpc.Extensions;

public static class AppExtensions
{
    public static void ConfigureGrpc(this WebApplication webApplication)
    {
        webApplication.MapGrpcService<CustomerAddressService>();
        webApplication.MapGrpcReflectionService();
    }
}