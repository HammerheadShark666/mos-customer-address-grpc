namespace Microservice.Customer.Address.Grpc.Extensions;

public static class AppExtensions
{
    public static void ConfigureGrpc(this WebApplication app)
    {
        app.MapGrpcService<CustomerAddressService>();
        app.MapGrpcReflectionService();
    }
}
