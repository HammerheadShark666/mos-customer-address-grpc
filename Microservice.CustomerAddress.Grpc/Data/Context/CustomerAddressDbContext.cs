using Microsoft.EntityFrameworkCore;

namespace Microservice.Customer.Address.Grpc.Data.Contexts;

public class CustomerAddressDbContext : DbContext
{ 
    public CustomerAddressDbContext(DbContextOptions<CustomerAddressDbContext> options) : base(options) { }
 
    public DbSet<Grpc.Domain.CustomerAddress> CustomerAddresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        base.OnModelCreating(modelBuilder);
    }
}

//add-migration
//update-database

//azurite --silent --location c:\azurite --debug c:\azurite\debug.log