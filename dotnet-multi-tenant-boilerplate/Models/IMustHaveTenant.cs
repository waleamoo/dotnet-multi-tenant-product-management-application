namespace dotnet_multi_tenant_boilerplate.Models
{
    public interface IMustHaveTenant
    {
        public string TenantId { get; set; }
    }
}
