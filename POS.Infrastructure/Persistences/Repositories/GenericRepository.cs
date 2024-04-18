using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Helpers;
using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Utilities.Static;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace POS.Infrastructure.Persistences.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly POSContext _context;
        private readonly DbSet<T> _entity;

        public GenericRepository(POSContext context)
        {
            _context = context;
            //mapea eh implementa los repositorio
            _entity = _context.Set<T>();
        }

        //metodo listar todo
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            //de generica pasamos una entidad
            var getAll = await _entity
                //y que traiga los registros donde el estado sea 1(activo) y los delect sean null
                .Where(x=> x.State.Equals((int)StateTypes.Active) && x.AuditDeleteUser == null && x.AuditDeleteDate == null).AsNoTracking().ToListAsync();
            
            return getAll;
        }

        //listar por id especifico
        public async Task<T> GetByIdAsync(int id)
        {
            var getByID = await _entity!.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));

            return getByID!;
        }
        
        // para registrar
        public async Task<bool> RegisterAsync(T entity)
        {
            entity.AuditCreateUser = 1;
            entity.AuditCreateDate = DateTime.Now;

            await _context.AddAsync(entity);

            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }
        
        //para modificar
        public async Task<bool> EditAsync(T entity)
        {
            entity.AuditUpdateUser = 1;
            entity.AuditUpdateDate = DateTime.Now;

            _context.Update(entity);

            _context.Entry(entity).Property(x => x.AuditCreateUser).IsModified = false;
            _context.Entry(entity).Property(x => x.AuditCreateDate).IsModified = false;

            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }

        //para eliminar
        public async Task<bool> RemoveAsync(int id)
        {
            //recibir una entidad generica por el Id q voy pasando
            T entity = await GetByIdAsync(id);

            entity.AuditDeleteUser = 1;
            entity.AuditDeleteDate = DateTime.Now;

            _context.Update(entity);

            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }

        //para obtener la entidad de tipo Iqueryable
        public IQueryable<T> GetEntityQuery(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _entity;

            if (filter != null) query.Where(filter);

            return query;
        }


        //Ordenamiento de datos
        public IQueryable<TDTO> Ordering<TDTO>(BasePaginationRequest request, IQueryable<TDTO> queryable, bool pagination = false) where TDTO : class
        {
            IQueryable<TDTO> queryDto = request.Order == "desc" ? queryable.OrderBy($"{request.Sort} descending") : queryable.OrderBy($"{request.Sort} ascending");
            
            if (pagination) queryDto = queryDto.Paginate(request);
            return queryDto;
        }

    }
}
