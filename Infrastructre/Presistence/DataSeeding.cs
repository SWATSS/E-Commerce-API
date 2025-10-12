using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Presistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }
                if (!_dbContext.ProductBrands.Any())
                {
                    //var ProductBrandData = await File.ReadAllTextAsync(@"..\Infrastructre\Presistence\Data\DataSeeding\brands.json");
                    var ProductBrandData = File.OpenRead(@"..\Infrastructre\Presistence\Data\DataSeeding\brands.json"); // openRead : Stream With File
                    var productBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandData);
                    if (productBrands is not null && productBrands.Any())
                    {
                        await _dbContext.ProductBrands.AddRangeAsync(productBrands);
                    }
                }
                if (!_dbContext.ProductTypes.Any())
                {
                    var ProductTypeData = File.OpenRead(@"..\Infrastructre\Presistence\Data\DataSeeding\types.json");
                    var productTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypeData);
                    if (productTypes is not null && productTypes.Any())
                    {
                        await _dbContext.ProductTypes.AddRangeAsync(productTypes);
                    }
                }
                if (!_dbContext.Products.Any())
                {
                    var ProductData = File.OpenRead(@"..\Infrastructre\Presistence\Data\DataSeeding\products.json");
                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductData);
                    if (products is not null && products.Any())
                    {
                        await _dbContext.Products.AddRangeAsync(products);
                    }
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            { 

            }
        }
    }
}
