using AutoMapper;
using Grpc.Core;
using Microservice.CustomerAddress.Api.Protos;
using Microservice.CustomerAddress.Grpc.Data.Repository.Interfaces;
using Microservice.CustomerAddress.Grpc.Helpers.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace Microservice.CustomerAddress.Grpc.Service;

public class CustomerAddressService(ICustomerAddressRepository customerAddressRepository, IMapper mapper) : CustomerAddressGrpc.CustomerAddressGrpcBase
{
    private readonly ICustomerAddressRepository _customerAddressRepository = customerAddressRepository;

    private readonly IMapper _mapper = mapper;

    [Authorize]
    public override async Task<CustomerAddressResponse> GetCustomerAddress(CustomerAddressRequest request, ServerCallContext context)
    {
        var customerAddress = await _customerAddressRepository.ByIdAsync(new Guid(request.CustomerId), new Guid(request.AddressId)) ?? throw new RpcNotFoundException(new Status(StatusCode.NotFound, "Customer address not found."));
        return _mapper.Map<CustomerAddressResponse>(customerAddress);
    }
}