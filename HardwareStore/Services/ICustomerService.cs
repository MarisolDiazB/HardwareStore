using HardwareStore.Core;
using HardwareStore.Data;
using HardwareStore.Data.Entities;
using HardwareStore.Helpers;
using Microsoft.EntityFrameworkCore;
using HardwareStore.Core.Pagination;
namespace HardwareStore.Services
{
    public interface ICustomerService
    {

        Task<Response<Customer>> CreateAsync(Customer model); // Crear un cliente
        Task<Response<PaginationResponse<Customer>>> GetListAsync(PaginationRequest request); // Traer una lista de los clientes paginada
        Task<Response<Customer>> GetOneAsync(int id); // Traer un cliente según su id
        Task<Response<Customer>> EditAsync(Customer model); // Editar un cliente
        Task<Response<Customer>> DeleteAsync(int id); // Eliminar un cliente
    }

    public class CustomerService : ICustomerService
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public CustomerService(DataContext context, IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        public async Task<Response<PaginationResponse<Customer>>> GetListAsync(PaginationRequest request)
        {
            try
            {
                // Obtener la consulta base de empleados
                IQueryable<Customer> queryable = _context.Customer.AsQueryable();

                // Aplicar filtrado si se proporciona
                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    string filter = request.Filter.ToLower(); // Convertir el filtro a minúsculas para una comparación insensible a mayúsculas y minúsculas
                    queryable = queryable.Where(b => b.FirstName.ToLower().Contains(filter) || b.LastName.ToLower().Contains(filter));
                }

                queryable = queryable.Select(b => new Customer
                {
                    Id = b.Id,
                    FirstName = b.FirstName,
                    LastName = b.LastName,
                    Customeraddress = b.Customeraddress,
                    Phone = b.Phone
                });

                PagedList<Customer> pagedList = await PagedList<Customer>.ToPagedListAsync(queryable, request);

                PaginationResponse<Customer> result = new PaginationResponse<Customer>
                {
                    List = pagedList,
                    TotalCount = pagedList.TotalCount,
                    RecordsPerPage = pagedList.RecordsPerPage,
                    CurrentPage = pagedList.CurrentPage,
                    TotalPages = pagedList.TotalPages,
                    Filter = request.Filter,
                };

                // Devolver la respuesta exitosa
                return ResponseHelper<PaginationResponse<Customer>>.MakeResponseSuccess(result);
            }
            catch (Exception ex)
            {
                // Capturar excepciones y devolver una respuesta de error
                return ResponseHelper<PaginationResponse<Customer>>.MakeResponseFail(ex);
            }
        }
        public async Task<Response<Customer>> CreateAsync(Customer model)
        {
            try
            {
                Customer customer = new Customer
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Customeraddress = model.Customeraddress,
                    Phone = model.Phone,
                };

                await _context.AddAsync(customer);
                await _context.SaveChangesAsync();

                return ResponseHelper<Customer>.MakeResponseSuccess(customer, "Cliente creado con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Customer>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Customer>> DeleteAsync(int id)
        {
            try
            {
                Customer customer = await _context.Customer.FindAsync(id);

                if (customer == null)
                {
                    return ResponseHelper<Customer>.MakeResponseFail($"El cliente con el id '{id}' no existe.");
                }

                _context.Customer.Remove(customer);
                await _context.SaveChangesAsync();

                return ResponseHelper<Customer>.MakeResponseSuccess("El cliente fue eliminado con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Customer>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Customer>> EditAsync(Customer model)
        {
            try
            {
                _context.Customer.Update(model);
                await _context.SaveChangesAsync();

                return ResponseHelper<Customer>.MakeResponseSuccess(model, "Cliente editado con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Customer>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Customer>> GetOneAsync(int id)
        {
            try
            {
                Customer customer = await _context.Customer.FindAsync(id);

                if (customer == null)
                {
                    return ResponseHelper<Customer>.MakeResponseFail($"No existe un cliente con este id '{id}'.");
                }

                return ResponseHelper<Customer>.MakeResponseSuccess(customer);
            }
            catch (Exception ex)
            {
                return ResponseHelper<Customer>.MakeResponseFail(ex);
            }
        }
    }
}
