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
        public void DataSeed()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    _dbContext.Database.Migrate();
                }
                if (!_dbContext.ProductBrands.Any())
                {
                    var ProductBrandData = File.ReadAllText(@"..\Infrastructre\Presistence\Data\DataSeeding\brands.json");
                    var productBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandData);
                    if (productBrands is not null && productBrands.Any())
                    {
                        _dbContext.ProductBrands.AddRange(productBrands);
                    }
                }
                if (!_dbContext.ProductTypes.Any())
                {
                    var ProductTypeData = File.ReadAllText(@"..\Infrastructre\Presistence\Data\DataSeeding\types.json");
                    var productTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypeData);
                    if (productTypes is not null && productTypes.Any())
                    {
                        _dbContext.ProductTypes.AddRange(productTypes);
                    }
                }
                if (!_dbContext.Products.Any())
                {
                    var ProductData = File.ReadAllText(@"..\Infrastructre\Presistence\Data\DataSeeding\products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                    if (products is not null && products.Any())
                    {
                        _dbContext.Products.AddRange(products);
                    }
                }
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
