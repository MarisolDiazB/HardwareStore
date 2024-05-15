using Microsoft.EntityFrameworkCore;
using HardwareStore.Core;
using HardwareStore.Data;
using HardwareStore.Data.Entities;
using HardwareStore.Helpers;
using HardwareStore.Requests;
using System.Collections.Generic;
using static HardwareStore.Services.IRoleServices;
namespace HardwareStore.Services
{
    public interface IRoleServices
    {
        public Task<Response<Role>> CreateAsync(Role model);//Crear un rol
        public Task<Response<List<Role>>> GetListAsync();//Trae una lista de los rol
        public Task<Response<Role>> GetOneAsync(int id);//Trae un rol segun su id
        public Task<Response<Role>> EditAsync(Role model);//Edita un rol
        public Task<Response<Role>> DeleteAsync(int id);//Elimina el rol

        public class RoleService : IRoleServices
        {
            private readonly DataContext _context;

            public RoleService(DataContext context)
            {
                _context = context;
            }

            public async Task<Response<Role>> CreateAsync(Role model)
            {
                try
                {
                    Role role = new Role
                    {
                        Id = model.Id,
                        Name = model.Name
                    };
                    await _context.AddAsync(role);
                    await _context.AddAsync(role);
                    await _context.SaveChangesAsync();

                    return ResponseHelper<Role>.MakeResponseSuccess(role, "Rol creado con éxito");
                }
                catch (Exception ex)
                {
                    return ResponseHelper<Role>.MakeResponseFail(ex);
                }
            }

            public async Task<Response<Role>> DeleteAsync(int id)
            {
                try
                {
                    Role? role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

                    if (role is null)
                    {
                        return ResponseHelper<Role>.MakeResponseFail($"El rol con el id '{id}' no existe.");
                    }

                    _context.Roles.Remove(role);
                    await _context.SaveChangesAsync();

                    return ResponseHelper<Role>.MakeResponseSuccess("El rol fue eliminado con éxito");
                }
                catch (Exception ex)
                {
                    return ResponseHelper<Role>.MakeResponseFail(ex);
                }
            }

            public async Task<Response<Role>> EditAsync(Role model)
            {
                try
                {
                    _context.Roles.Update(model);
                    await _context.SaveChangesAsync();

                    return ResponseHelper<Role>.MakeResponseSuccess(model, "Rol editada con éxito");
                }
                catch (Exception ex)
                {
                    return ResponseHelper<Role>.MakeResponseFail(ex);
                }
            }

            public async Task<Response<List<Role>>> GetListAsync()
            {
                try
                {
                    List<Role> list = await _context.Roles.ToListAsync();

                    return ResponseHelper<List<Role>>.MakeResponseSuccess(list, "Roles obtenidas con éxito");
                }
                catch (Exception ex)
                {
                    return ResponseHelper<List<Role>>.MakeResponseFail(ex);
                }
            }

            public async Task<Response<Role>> GetOneAsync(int id)
            {
                try
                {
                    Role? role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
                    if (role is null)//Revisar que no est
                    {
                        return ResponseHelper<Role>.MakeResponseFail($"No existe un rol con este id '{id}'.");
                    }

                    return ResponseHelper<Role>.MakeResponseSuccess(role);
                }
                catch (Exception ex)
                {
                    return ResponseHelper<Role>.MakeResponseFail(ex);
                }
            }
        }

    }
}
