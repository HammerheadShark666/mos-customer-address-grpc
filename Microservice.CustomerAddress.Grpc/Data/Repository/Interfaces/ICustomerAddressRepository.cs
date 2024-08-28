namespace Microservice.CustomerAddress.Grpc.Data.Repository.Interfaces;

public interface ICustomerAddressRepository
{
    Task<Domain.CustomerAddress> ByIdAsync(Guid customerId, Guid addressId);
}