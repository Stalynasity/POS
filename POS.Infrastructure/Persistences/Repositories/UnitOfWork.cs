using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;

namespace POS.Infrastructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepositoy Category { get; private set; }

        public IUserRepository User { get; private set;}

        private readonly POSContext _context;

        public UnitOfWork(POSContext context)
        {
            _context = context;
            Category = new CategoyRepository(_context);
            User = new UserRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChange()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
