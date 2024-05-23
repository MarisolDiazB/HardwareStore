using Microsoft.EntityFrameworkCore;
using HardwareStore.Core;
using HardwareStore.Data;
using HardwareStore.Data.Entities;
using HardwareStore.Helpers;
using HardwareStore.Requests;
using HardwareStore.Core.Pagination;

namespace HardwareStore.Services
{
    public interface IEmployeeServices
    {
        public Task<Response<Employee>> CreateEmployeeAsync(Employee model);//Crear un Empleado
        Task<Response<PaginationResponse<Employee>>> GetListAsync(PaginationRequest request);//Trae una lista de los empleados
        public Task<Response<Employee>> GetOneEmployeeAsync(int id);//Trae un empleado segun su id
        public Task<Response<Employee>> EditEmployeeAsync(Employee model);//Edita a un empleado
        public Task<Response<Employee>> DeleteEmployeeAsync(int id);//Elimina el empleado        
        public Task<Response<Employee>> ToggleEmployeeAsync(ToggleEmployeeRequest request);//Si el empleado esta activo o no

        public class EmployeeService : IEmployeeServices
        {
            private readonly DataContext _context;

            public EmployeeService(DataContext context)
            {
                _context = context;
            }

            public async Task<Response<Employee>> CreateEmployeeAsync(Employee model)
            {
                try
                {
                    Employee employee = new Employee
                    {
                        Id = model.Id,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Age = model.Age,
                        IsActive= model.IsActive,
                    };

                    await _context.AddAsync(employee);
                    await _context.SaveChangesAsync();

                    return ResponseHelper<Employee>.MakeResponseSuccess(employee, "Empleado creado con éxito");
                }
                catch (Exception ex)
                {
                    return ResponseHelper<Employee>.MakeResponseFail(ex);
                }
            }
            public async Task<Response<Employee>> GetOneEmployeeAsync(int id)
            {
                try
                {
                    Employee? employee = await _context.Employees.FirstOrDefaultAsync(E => E.Id == id);

                    if (employee is null)
                    {
                      return ResponseHelper<Employee>.MakeResponseFail($"No existe un empleado con este id '{id}'.");
                    }

                    return ResponseHelper<Employee>.MakeResponseSuccess(employee);
                }
                catch (Exception ex)
                {
                    return ResponseHelper<Employee>.MakeResponseFail(ex);
                }
            }

            public async Task<Response<PaginationResponse<Employee>>> GetListAsync(PaginationRequest request)
            {
                try
                {
                    // Obtener la consulta base de empleados
                    IQueryable<Employee> queryable = _context.Employees.AsQueryable();

                    // Aplicar filtrado si se proporciona
                    if (!string.IsNullOrWhiteSpace(request.Filter))
                    {
                        string filter = request.Filter.ToLower(); // Convertir el filtro a minúsculas para una comparación insensible a mayúsculas y minúsculas
                        queryable = queryable.Where(b => b.FirstName.ToLower().Contains(filter) || b.LastName.ToLower().Contains(filter));
                    }

                    // Seleccionar solo los campos necesarios
                    queryable = queryable.Select(b => new Employee
                    {
                        Id = b.Id,
                        FirstName = b.FirstName,
                        LastName = b.LastName,
                        Age = b.Age,
                        IsActive = b.IsActive
                    });

                    // Obtener la lista paginada
                    PagedList<Employee> pagedList = await PagedList<Employee>.ToPagedListAsync(queryable, request);

                    // Crear el objeto de respuesta de paginación
                    PaginationResponse<Employee> result = new PaginationResponse<Employee>
                    {
                        List = pagedList,
                        TotalCount = pagedList.TotalCount,
                        RecordsPerPage = pagedList.RecordsPerPage,
                        CurrentPage = pagedList.CurrentPage,
                        TotalPages = pagedList.TotalPages,
                        Filter = request.Filter,
                    };

                    // Devolver la respuesta exitosa
                    return ResponseHelper<PaginationResponse<Employee>>.MakeResponseSuccess(result);
                }
                catch (Exception ex)
                {
                    // Capturar excepciones y devolver una respuesta de error
                    return ResponseHelper<PaginationResponse<Employee>>.MakeResponseFail(ex);
                }
            }
            public async Task<Response<Employee>> EditEmployeeAsync(Employee model)
            {
                try
                {
                    _context.Employees.Update(model);
                    await _context.SaveChangesAsync();

                    return ResponseHelper<Employee>.MakeResponseSuccess(model, "Empleado editada con éxito");
                }
                catch (Exception ex)
                {
                    return ResponseHelper<Employee>.MakeResponseFail(ex);
                }
            }
            public async Task<Response<Employee>> DeleteEmployeeAsync(int id)
            {
                try
                {
                    Employee? employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);

                    if (employee is null)
                    {
                        return ResponseHelper<Employee>.MakeResponseFail($"El empleado con el id '{id}' no existe.");
                    }

                    _context.Employees.Remove(employee);
                    await _context.SaveChangesAsync();

                    return ResponseHelper<Employee>.MakeResponseSuccess("El empleado fue eliminado con éxito");
                }
                catch (Exception ex)
                {
                    return ResponseHelper<Employee>.MakeResponseFail(ex);
                }
            }
            public async Task<Response<Employee>> ToggleEmployeeAsync(ToggleEmployeeRequest request)
            {
                try
                {
                    Employee? model = await _context.Employees.FindAsync(request.Id);

                    if (model == null)
                    {
                        return ResponseHelper<Employee>.MakeResponseFail($"No existe un empleado con este id '{request.Id}'");
                    }
                    model.IsActive = request.Active;

                    _context.Employees.Update(model);
                    await _context.SaveChangesAsync();

                    return ResponseHelper<Employee>.MakeResponseSuccess("Empleado Actualizado con éxito");
                }
                catch (Exception ex)
                {
                    return ResponseHelper<Employee>.MakeResponseFail(ex);
                }
            }

        }

    }
}

