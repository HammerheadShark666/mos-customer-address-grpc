namespace Microservice.Customer.Address.Grpc.Data.Repository.Interfaces;

public interface ICustomerAddressRepository
{ 
    Task<Grpc.Domain.CustomerAddress> ByIdAsync(Guid customerId, Guid addressId);  
}