using APIpeliculas.Models;

namespace APIpeliculas.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable,Paginacion paginacion)
        {
            return queryable
                    .Skip((paginacion.Pagina - 1) * paginacion.RecordsPorPagina)
                    .Take(paginacion.RecordsPorPagina);
        }
    }
}
