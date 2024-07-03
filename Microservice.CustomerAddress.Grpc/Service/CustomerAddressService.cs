using AutoMapper;
using Grpc.Core;
using Microservice.Customer.Address.Api.Protos;
using Microservice.Customer.Address.Grpc.Data.Repository.Interfaces;
using Microservice.Customer.Address.Grpc.Helpers.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace Microservice.Customer.Address.Grpc;

public class CustomerAddressService : CustomerAddressGrpc.CustomerAddressGrpcBase
{ 
    private readonly ICustomerAddressRepository _customerAddressRepository;
     
    private readonly IMapper _mapper;

    public CustomerAddressService(ICustomerAddressRepository customerAddressRepository, IMapper mapper)
    {
        _customerAddressRepository = customerAddressRepository; 
        _mapper = mapper;
    }

    [Authorize]
    public override async Task<CustomerAddressResponse> GetCustomerAddress(CustomerAddressRequest request, ServerCallContext context)
    {
        var customerAddress = await _customerAddressRepository.ByIdAsync(new Guid(request.CustomerId), new Guid(request.AddressId));
        if (customerAddress == null)
        {
            throw new RpcNotFoundException(new Status(StatusCode.NotFound, "Customer address not found."));
        }

        return _mapper.Map<CustomerAddressResponse>(customerAddress);
    }
}