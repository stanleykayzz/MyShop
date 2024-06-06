using Microsoft.EntityFrameworkCore;
using MyShop.Core.Interface;
using MyShop.Infrastructure.Models;

namespace MyShop.Infrastructure.Repositories
{
    public class ProductRepository : IRepository<Core.Entities.Product>
    {
        private readonly MyShopDbContext _dbContext;
        public ProductRepository(MyShopDbContext context)
        {
            _dbContext = context;
        }

        public async Task AddAsync(Core.Entities.Product entity)
        {
            var product = new Product
            {
                ProductBrand = entity.Brand,
                ProductName = entity.Name,
                ProductSize = entity.Size
            };
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Core.Entities.Product>> GetAllAsync()
        {
            return await _dbContext.Products
                .Select(x => new Core.Entities.Product
                {
                    Id = x.ProductId,
                    Brand = x.ProductBrand,
                    Name = x.ProductName,
                    Size = x.ProductSize
                }).ToListAsync();
        }

        public Task<Core.Entities.Product> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsynct(Core.Entities.Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
