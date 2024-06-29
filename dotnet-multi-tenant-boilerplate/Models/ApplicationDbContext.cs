using dotnet_multi_tenant_boilerplate.Services;
using Microsoft.EntityFrameworkCore;

namespace dotnet_multi_tenant_boilerplate.Models
{
    public class ApplicationDbContext : DbContext 
    {
        private string CurrentTenantId { get; set; }
        private readonly ICurrentTenantService _currentTenantService;

        // get the CurrentTenantId from the db context 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentTenantService currentTenantService) : base (options)
        {
            _currentTenantService = currentTenantService;
            CurrentTenantId = _currentTenantService.TenantId;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        // runs on app startup - and for querying the database 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().HasQueryFilter(a => a.TenantId == CurrentTenantId);
        }

        // override the SaveChanges method 
        // every time we save something 
        public override int SaveChanges()
        {
            foreach(var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.TenantId = CurrentTenantId;
                        break;
                }
            }
            var result = base.SaveChanges();
            return result;
        }
    }
}
