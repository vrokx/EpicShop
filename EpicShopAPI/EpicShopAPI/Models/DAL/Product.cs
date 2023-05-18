using EpicShopAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace EpicShopAPI.Models.DAL
{
    [Serializable]
    public class Product : IProduct
    {
        private readonly EpicShopApiDBContext _dbContext;
        private readonly DbSet<ProductModel> _dbSet;

        public Product(EpicShopApiDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<ProductModel>();
        }
        public async Task<IEnumerable<ProductModel>> GetAllProductsSp()
        {
            var products = await _dbContext.Set<ProductModel>().FromSqlRaw("EXEC GetProductsSP").ToListAsync();
            return products;
        }
    }
}
