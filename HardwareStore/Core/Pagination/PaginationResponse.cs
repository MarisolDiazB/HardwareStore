namespace HardwareStore.Core.Pagination // Define el espacio de nombres y declara la clase PaginationResponse<T>.
{
    public class PaginationResponse<T> where T : class // Declara la clase PaginationResponse<T>, donde T es un tipo de clase.
    {
        // Propiedades para la paginación.
        public int CurrentPage { get; set; } // Propiedad para el número de página actual.
        public int TotalPages { get; set; } // Propiedad para el número total de páginas.
        public int RecordsPerPage { get; set; } // Propiedad para el número de registros por página.
        public int TotalCount { get; set; } // Propiedad para el total de registros.

        // Propiedades calculadas para la navegación.
        public bool HasPrevious => CurrentPage > 1; // Propiedad booleana que indica si hay una página anterior.
        public bool HasNext => CurrentPage < TotalPages; // Propiedad booleana que indica si hay una página siguiente.

        // Propiedad para el número de páginas visibles en la barra de paginación.
        public int VisiblePages => 5;

        public string? Filter { get; set; } // Propiedad para el filtro de búsqueda.

        // Propiedad para almacenar la lista de números de página para mostrar en la barra de paginación.
        public List<int> Pages
        {
            get
            {
                // Cálculo de las páginas a mostrar.
                List<int> pages = new List<int>();
                int half = (VisiblePages / 2);
                int start = CurrentPage - half + 1 - (VisiblePages % 2);
                int end = CurrentPage + half;

                int vPages = VisiblePages;

                // Ajustar el número de páginas visibles si excede el total de páginas.
                if (vPages > TotalPages)
                {
                    vPages = TotalPages;
                }

                // Ajustar los límites de inicio y fin de las páginas a mostrar.
                if (start <= 0)
                {
                    start = 1;
                    end = vPages;
                }

                if (end > TotalPages)
                {
                    start = TotalPages - vPages + 1;
                    end = TotalPages;
                }

                // Generar la lista de páginas a mostrar.
                int itPage = start;
                while (itPage <= end)
                {
                    pages.Add(itPage);
                    itPage += 1;
                }

                return pages; // Devuelve la lista de páginas.
            }
        }

        public PagedList<T> List { get; set; } = null!; // Propiedad para almacenar la lista de elementos paginados.
    }
}
