using Microsoft.EntityFrameworkCore;

namespace dotnet_multi_tenant_boilerplate.Models
{
    // current tenant db contect 
    public class TenantDbContext : DbContext
    {
        // we have do DbContextOptions<[db-context-name]> because we are using two db context classes
        // this TenantDbContext is not needed for migration 
        public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options) 
        {

        }

        public DbSet<Tenant> Tenants { get; set; }
    }
}
