/*using Microsoft.EntityFrameworkCore;

namespace HardwareStore.Core.Pagination
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int RecordsPerPage { get; private set; }
        public int TotalCount { get; private set; }

        public PagedList(List<T> items, int count, int pageNumber, int recordsperPage)
        {
            TotalCount = count;
            RecordsPerPage = recordsperPage;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)recordsperPage);
            AddRange(items);
        }

        public static async Task<PagedList<T>> ToPagedListAsync(IQueryable<T> source, PaginationRequest request)
        {
            int count = await source.CountAsync();

            List<T> items = await source.Paginate<T>(request)
                                        .ToListAsync();

            return new PagedList<T>(items, count, request.Page, request.RecordsPerPage);
        }
    }
}*/
using Microsoft.EntityFrameworkCore; // Importa el espacio de nombres de Entity Framework Core para acceder a las funcionalidades de acceso a datos.

namespace HardwareStore.Core.Pagination // Define el espacio de nombres y declara la clase PagedList.
{
    public class PagedList<T> : List<T> // Declara la clase PagedList que hereda de List<T>, siendo T el tipo de elementos en la lista.
    {
        public int CurrentPage { get; private set; } // Propiedad para almacenar el número de página actual.
        public int TotalPages { get; private set; } // Propiedad para almacenar el número total de páginas.
        public int RecordsPerPage { get; private set; } // Propiedad para almacenar el número de registros por página.
        public int TotalCount { get; private set; } // Propiedad para almacenar el número total de elementos en la lista.

        // Constructor de la clase PagedList.
        public PagedList(List<T> items, int count, int pageNumber, int recordsPerPage)
        {
            TotalCount = count; // Asigna el total de elementos en la lista.
            RecordsPerPage = recordsPerPage; // Asigna el número de registros por página.
            CurrentPage = pageNumber; // Asigna el número de página actual.
            TotalPages = (int)Math.Ceiling(count / (double)recordsPerPage); // Calcula el número total de páginas.

            AddRange(items); // Agrega los elementos a la lista.
        }

        // Método estático para convertir una IQueryable en una lista paginada asincrónica.
        public static async Task<PagedList<T>> ToPagedListAsync(IQueryable<T> source, PaginationRequest request)
        {
            int count = await source.CountAsync(); // Obtiene el número total de elementos en la fuente.

            List<T> items = await source.Skip((request.Page - 1) * request.RecordsPerPage) // Salta los elementos según la página y toma los elementos según el número de registros por página.
                                        .Take(request.RecordsPerPage)
                                        .ToListAsync(); // Convierte el resultado en una lista asincrónica.

            return new PagedList<T>(items, count, request.Page, request.RecordsPerPage); // Devuelve una nueva instancia de PagedList.
        }
    }
}
