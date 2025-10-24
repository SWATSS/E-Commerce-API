using Shared;
using Shared.DataTransferObjects.ProdcutModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction
{
    public interface IProductService
    {
        // Get All Products
        Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams);
        // Get Product By Id
        Task<ProductDto> GetProductByIdAsync(int id);
        // Get All Brands
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        // Get All Types
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
    }
}
