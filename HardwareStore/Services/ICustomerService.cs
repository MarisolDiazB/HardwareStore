using HardwareStore.Core;
using HardwareStore.Data;
using HardwareStore.Data.Entities;
using HardwareStore.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HardwareStore.Services
{
    public interface ICustomerService
    {
        public Task<Response<Customer>> CreateAsync(Customer model);//Crear un cliente
        public Task<Response<List<Customer>>> GetListAsync();//Trae una lista de los cliente
        public Task<Response<Customer>> GetOneAsync(int id);//Trae un cliente segun su id
        public Task<Response<Customer>> EditAsync(Customer model);//Edita un cliente
        public Task<Response<Customer>> DeleteAsync(int id);//Elimina el cliente

        public class CustomerService : ICustomerService
        {
            private readonly DataContext _context;
            public CustomerService(DataContext context)
            {
                _context = context;
            }
            public async Task<Response<Customer>> CreateAsync(Customer model)
            {
                try
                {
                    Customer customer = new Customer
                    {
                        Id = model.Id,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Customeraddress = model.Customeraddress,
                        Phone=model.Phone,
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
                    Customer? customer = await _context.Customer.FirstOrDefaultAsync(C => C.Id == id);

                    if (customer is null)
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

            public async Task<Response<List<Customer>>> GetListAsync()
            {
                try
                {
                    List<Customer> list = await _context.Customer.ToListAsync();

                    return ResponseHelper<List<Customer>>.MakeResponseSuccess(list, "Clientes obtenidos con éxito");
                }
                catch (Exception ex)
                {
                    return ResponseHelper<List<Customer>>.MakeResponseFail(ex);
                }
            }

            public async Task<Response<Customer>> GetOneAsync(int id)
            {
              try
                {
                    Customer? customer = await _context.Customer.FirstOrDefaultAsync(C => C.Id == id);

                    if (customer is null)
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
}
