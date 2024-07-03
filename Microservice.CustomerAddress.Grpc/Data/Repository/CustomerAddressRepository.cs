using Microservice.Customer.Address.Grpc.Data.Contexts;
using Microservice.Customer.Address.Grpc.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Customer.Address.Grpc.Data.Repository;

public class CustomerAddressRepository(IDbContextFactory<CustomerAddressDbContext> dbContextFactory) : ICustomerAddressRepository
{    
    public IDbContextFactory<CustomerAddressDbContext> _dbContextFactory { get; set; } = dbContextFactory;

    public async Task<Grpc.Domain.CustomerAddress> ByIdAsync(Guid customerId, Guid addressId)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync();
        return await db.CustomerAddresses
                        .Where(o => o.Id.Equals(addressId) && o.CustomerId.Equals(customerId))
                        .Include(e => e.Country)
                        .SingleOrDefaultAsync();
    } 
}