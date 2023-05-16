using Microsoft.EntityFrameworkCore;

namespace APIpeliculas.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertarParametrosPaginacionEnCabecera<T>(this HttpContext httpContext, IQueryable<T> queryable)
        {
            if(httpContext is null)
            {
                throw new ArgumentNullException(nameof(httpContext));

            }
                double cantidad = await queryable.CountAsync();
                httpContext.Response.Headers.Add("CantidadTotalRegistros",cantidad.ToString()); 
        }

    }
}
