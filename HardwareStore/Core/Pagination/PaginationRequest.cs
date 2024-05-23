/*namespace HardwareStore.Core.Pagination
{
    public class PaginationRequest
    {
        private int _page = 1;
        private int _recirdsPerPage = 15;
        private int _maxRecordsPerPage = 50;

        public string? Filter { get; set; }

        public int Page
        {
            get => _page;

            set => _page = value > 0 ? value : _page;
        }
        public int RecordsPerPage
        {
            get => _recirdsPerPage;

            set => _recirdsPerPage = value <= _maxRecordsPerPage ? value : _maxRecordsPerPage;
        }
    }
}*/

namespace HardwareStore.Core.Pagination // Define el espacio de nombres y declara la clase PaginationRequest.
{
    public class PaginationRequest // Declara la clase PaginationRequest.
    {
        private int _pageNumber = 1; // Campo privado para almacenar el número de página actual, con valor predeterminado de 1.
        private int _recordsPerPage = 15; // Campo privado para almacenar el número de registros por página, con valor predeterminado de 15.
        private readonly int _maxRecordsPerPage = 50; // Campo privado para almacenar el número máximo de registros por página, establecido en 50.

        public string? Filter { get; set; } // Propiedad para almacenar el filtro de búsqueda.

        public int PageNumber // Propiedad para el número de página actual.
        {
            get => _pageNumber; // Accesor get para obtener el valor de _pageNumber.
            set => _pageNumber = value > 0 ? value : _pageNumber; // Accesor set para asignar el valor a _pageNumber, asegurándose de que sea mayor que 0.
        }

        public int RecordsPerPage // Propiedad para el número de registros por página.
        {
            get => _recordsPerPage; // Accesor get para obtener el valor de _recordsPerPage.
            set => _recordsPerPage = value <= _maxRecordsPerPage ? value : _maxRecordsPerPage; // Accesor set para asignar el valor a _recordsPerPage, asegurándose de que no exceda el número máximo de registros por página.
        }

        public int Page { get; internal set; } // Propiedad para el número de página, de solo lectura interna.
    }
}
