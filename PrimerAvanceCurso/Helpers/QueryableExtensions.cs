using PrimerAvanceCurso.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerAvanceCurso.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, PaginacionDTO paginacionDTO)
        {

            var queryable1 = queryable.Skip((paginacionDTO.Pagina - 1) * paginacionDTO.NumeroResgistroPagina)
                .Take(paginacionDTO.NumeroResgistroPagina);
            return queryable1;
        }
    }
}
