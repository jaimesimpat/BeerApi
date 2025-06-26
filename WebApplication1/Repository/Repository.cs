using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> Get();
        Task<TEntity?> GetById(int id);
        Task Add(TEntity entity);
        void Update(TEntity entity);
        Task Save();
        IEnumerable<TEntity> Search(Func<TEntity, bool> filter);
        Task Remove(int id);
    }

    public class BeerRepository : IRepository<Beer>
    {
        private StoreContext _context;

        public BeerRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Beer>> Get() =>
            await _context.Beers.Include(b => b.Brand).ToListAsync();

        public async Task<Beer?> GetById(int id) =>
            await _context.Beers.Include(b => b.Brand).FirstOrDefaultAsync(b => b.BeerID == id);

        public async Task Add(Beer beer)
            => await _context.Beers.AddAsync(beer);

        public void Update(Beer beer)
        {
            _context.Beers.Attach(beer);
            _context.Entry(beer).State = EntityState.Modified;
        }

        public async Task Save()
            => await _context.SaveChangesAsync();

        public IEnumerable<Beer> Search(Func<Beer, bool> filter)
            => _context.Beers.Include(b => b.Brand).Where(filter).ToList();

        public async Task Remove(int id)
        {
            var beer = await _context.Beers.FindAsync(id);
            if (beer != null)
            {
                _context.Beers.Remove(beer);
            }
        }
    }
}
