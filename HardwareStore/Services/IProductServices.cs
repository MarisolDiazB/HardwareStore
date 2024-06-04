
using Microsoft.EntityFrameworkCore;
using HardwareStore.Core;
using HardwareStore.Data;
using HardwareStore.Data.Entities;
using HardwareStore.Helpers;
using HardwareStore.Requests;
using HardwareStore.Core.Pagination;
using static System.Collections.Specialized.BitVector32;
using static HardwareStore.Services.IProductServices;

namespace HardwareStore.Services
{
    public interface IProductServices
    {
        Task<Response<Products>> CreateAsync(Products model);//Crear un producto
        Task<Response<PaginationResponse<Products>>> GetListAsync(PaginationRequest request);//Trae una lista de los productos
        Task<Response<Products>> GetOneAsync(int id);//Trae un producto segun su id
        Task<Response<Products>> EditAsync(Products model);//Edita un producto
        Task<Response<Products>> DeleteAsync(int id);//Elimina el producto
        public class ProductService : IProductServices
        {
            private readonly DataContext _context;
            public ProductService(DataContext context)
            {
                _context = context;
            }
            public async Task<Response<Products>> CreateAsync(Products model)
            {
                try
                {
                    Products products = new Products
                    {
                        Id= model.Id,
                        Name = model.Name,
                        Price = model.Price,
                        Stock=model.Stock
                    };
                    await _context.AddAsync(products);
                    await _context.SaveChangesAsync();

                    return ResponseHelper<Products>.MakeResponseSuccess(products, "Producto creado con éxito");
                }
                catch (Exception ex)
                {
                    return ResponseHelper<Products>.MakeResponseFail(ex);
                }
            }
            public async Task<Response<Products>> GetOneAsync(int id)
            {
                try
                {
                    Products? products = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

                    if (products is null)//Revisar que no este nulo
                    {
                        return ResponseHelper<Products>.MakeResponseFail($"No existe un producto con este id '{id}'.");
                    }

                    return ResponseHelper<Products>.MakeResponseSuccess(products);
                }
                catch (Exception ex)
                {
                    return ResponseHelper<Products>.MakeResponseFail(ex);
                }
            }
            public async Task<Response<PaginationResponse<Products>>> GetListAsync(PaginationRequest request)
            {
                try
                {
                    // Obtener la consulta base de empleados
                    IQueryable<Products> queryable = _context.Products.AsQueryable();

                    // Aplicar filtrado si se proporciona
                    if (!string.IsNullOrWhiteSpace(request.Filter))
                    {
                        string filter = request.Filter.ToLower(); 
                        queryable = queryable.Where(b => b.Name.ToLower().Contains(filter));
                    }

                    // Seleccionar solo los campos necesarios
                    queryable = queryable.Select(b => new Products
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Price = b.Price,
                        Stock = b.Stock
                    });

                    // Obtener la lista paginada
                    PagedList<Products> pagedList = await PagedList<Products>.ToPagedListAsync(queryable, request);

                    // Crear el objeto de respuesta de paginación
                    PaginationResponse<Products> result = new PaginationResponse<Products>
                    {
                        List = pagedList,
                        TotalCount = pagedList.TotalCount,
                        RecordsPerPage = pagedList.RecordsPerPage,
                        CurrentPage = pagedList.CurrentPage,
                        TotalPages = pagedList.TotalPages,
                        Filter = request.Filter,
                    };

                    // Devolver la respuesta exitosa
                    return ResponseHelper<PaginationResponse<Products>>.MakeResponseSuccess(result);
                }
                catch (Exception ex)
                {
                    // Capturar excepciones y devolver una respuesta de error
                    return ResponseHelper<PaginationResponse<Products>>.MakeResponseFail(ex);
                }
            }
            public async Task<Response<Products>> EditAsync(Products model)
            {
                try
                {
                    _context.Products.Update(model);
                    await _context.SaveChangesAsync();

                    return ResponseHelper<Products>.MakeResponseSuccess(model, "Empleado editada con éxito");
                }
                catch (Exception ex)
                {
                    return ResponseHelper<Products>.MakeResponseFail(ex);
                }
            }
            public async Task<Response<Products>> DeleteAsync(int id)
            {
                try
                {
                    Products? products = await _context.Products.FirstOrDefaultAsync(e => e.Id == id);

                    if (products is null)
                    {
                        return ResponseHelper<Products>.MakeResponseFail($"El producto con el id '{id}' no existe.");
                    }

                    _context.Products.Remove(products);
                    await _context.SaveChangesAsync();

                    return ResponseHelper<Products>.MakeResponseSuccess("El producto fue eliminado con éxito");
                }
                catch (Exception ex)
                {
                    return ResponseHelper<Products>.MakeResponseFail(ex);
                }
            }
        }
    }
}
