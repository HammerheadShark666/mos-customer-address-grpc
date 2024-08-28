using Microsoft.EntityFrameworkCore;

namespace Microservice.CustomerAddress.Grpc.Data.Context;

public class CustomerAddressDbContext(DbContextOptions<CustomerAddressDbContext> options) : DbContext(options)
{
    public DbSet<Grpc.Domain.CustomerAddress> CustomerAddresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}