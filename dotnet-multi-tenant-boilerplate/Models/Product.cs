﻿namespace dotnet_multi_tenant_boilerplate.Models
{
    public class Product : IMustHaveTenant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TenantId { get; set; }
    }
}
