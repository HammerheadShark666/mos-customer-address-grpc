using Grpc.Core;

namespace Microservice.CustomerAddress.Grpc.Helpers.Exceptions;

public class RpcNotFoundException(Status status) : RpcException(status)
{
}