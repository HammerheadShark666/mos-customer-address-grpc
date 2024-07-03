using Grpc.Core;

namespace Microservice.Customer.Address.Grpc.Helpers.Exceptions;

public class RpcNotFoundException : RpcException
{
    public RpcNotFoundException(Status status) : base(status)
    {
    }
}