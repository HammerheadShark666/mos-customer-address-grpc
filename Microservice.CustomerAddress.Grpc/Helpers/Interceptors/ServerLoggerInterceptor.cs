using Grpc.Core;
using Grpc.Core.Interceptors;
using Microservice.CustomerAddress.Grpc.Helpers.Exceptions;

namespace Microservice.CustomerAddress.Grpc.Helpers.Interceptors;

public class ServerLoggerInterceptor(ILogger<ServerLoggerInterceptor> logger) : Interceptor
{
    private readonly ILogger _logger = logger;

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            LogMethodCall<TRequest, TResponse>(request, context);
            return await continuation(request, context);
        }
        catch (RpcNotFoundException)
        {
            _logger.LogError("Error thrown by {context.Method}.", context.Method);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error thrown by {context.Method}.", context.Method);
            throw new RpcException(new Status(StatusCode.Internal, ex.ToString()));
        }
    }

    private void LogMethodCall<TRequest, TResponse>(TRequest request, ServerCallContext context)
    {
        switch (context.Method)
        {
            case "/CustomerAddress.CustomerAddressGrpc/GetCustomerAddress":
                _logger.LogInformation("Call to Method: {context.Method}. Request: {request}", context.Method, request);
                break;
        }
    }
}