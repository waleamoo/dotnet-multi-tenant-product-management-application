namespace dotnet_multi_tenant_boilerplate.Services
{
    public interface ICurrentTenantService
    {
        string? TenantId { get; set; }
        public Task<bool> SetTenant(string tenant);
    }
}
