using POS.Infrastructure.Commons.Bases.Request;

namespace POS.Infrastructure.Helpers
{
    // metodos recomendables es usar los metodos skip y take para la paginacion
    public static class QueryableHelper
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, BasePaginationRequest request)
        {
            return queryable.Skip((request.NumPage - 1) * request.Records).Take(request.Records);
        }
    }
}
