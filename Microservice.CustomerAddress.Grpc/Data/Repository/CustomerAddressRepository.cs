using Microservice.CustomerAddress.Grpc.Data.Context;
using Microservice.CustomerAddress.Grpc.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Microservice.CustomerAddress.Grpc.Data.Repository;

public class CustomerAddressRepository(IDbContextFactory<CustomerAddressDbContext> dbContextFactory) : ICustomerAddressRepository
{
    public async Task<Grpc.Domain.CustomerAddress> ByIdAsync(Guid customerId, Guid addressId)
    {
        await using var db = await dbContextFactory.CreateDbContextAsync();
        return await db.CustomerAddresses
                        .Where(o => o.Id.Equals(addressId) && o.CustomerId.Equals(customerId))
                        .Include(e => e.Country)
                        .SingleOrDefaultAsync();
    }
}