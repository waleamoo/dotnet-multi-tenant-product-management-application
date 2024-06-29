using dotnet_multi_tenant_boilerplate.Models;
using dotnet_multi_tenant_boilerplate.Services.Dtos;

namespace dotnet_multi_tenant_boilerplate.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product CreateProduct(CreateProductRequestDto request);
        bool DeleteProduct(int id);
    }
}
