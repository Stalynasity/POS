using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;

namespace POS.Infrastructure.Persistences.Interfaces
{
    //implemnta el IGenericRepository 
    public interface ICategoryRepositoy : IGenericRepository<Category>
    {
        Task<BaseEntityResponse<Category>> ListCategory(BaseFiltersRequest filters);
    }
}
