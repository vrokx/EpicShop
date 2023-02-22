using EpicShopAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace EpicShopAPI.Models.DAL
{
    public class AllRepo<T> : IAllRepo<T> where T : class
    {
        private readonly EpicShopApiDBContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public AllRepo(EpicShopApiDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(int id, T entity)
        {
            var existingEntity = await _dbSet.FindAsync(id);
            _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }

}
