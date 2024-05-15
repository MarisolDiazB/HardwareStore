﻿using HardwareStore.Core;
using HardwareStore.Data;
using HardwareStore.Data.Entities;
using HardwareStore.Helpers;
using HardwareStore.Requests;
using Microsoft.EntityFrameworkCore;

namespace HardwareStore.Services
{
    public interface IClientService
    {
        public Task<Response<Clients>> CreateAsync(Clients model);//Crear un cliente
        public Task<Response<List<Clients>>> GetListAsync();//Trae una lista de los cliente
        public Task<Response<Clients>> GetOneAsync(int id);//Trae un cliente segun su id
        public Task<Response<Clients>> EditAsync(Clients model);//Edita un cliente
        public Task<Response<Clients>> DeleteAsync(int id);//Elimina el cliente

        public class ClientService : IClientService
        {
            private readonly DataContext _context;
            public ClientService(DataContext context)
            {
                _context = context;
            }
            public async Task<Response<Clients>> CreateAsync(Clients model)
            {
                try
                {
                    Clients clients = new Clients
                    {
                        Id = model.Id,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Customeraddress = model.Customeraddress,
                        Phone=model.Phone,
                    };

                    await _context.AddAsync(clients);
                    await _context.SaveChangesAsync();

                    return ResponseHelper<Clients>.MakeResponseSuccess(clients, "Cliente creado con éxito");
                }
                catch (Exception ex)
                {
                    return ResponseHelper<Clients>.MakeResponseFail(ex);
                }
            }

            public async Task<Response<Clients>> DeleteAsync(int id)
            {
                try
                {
                    Clients? clients = await _context.Clients.FirstOrDefaultAsync(C => C.Id == id);

                    if (clients is null)
                    {
                        return ResponseHelper<Clients>.MakeResponseFail($"El cliente con el id '{id}' no existe.");
                    }

                    _context.Clients.Remove(clients);
                    await _context.SaveChangesAsync();

                    return ResponseHelper<Clients>.MakeResponseSuccess("El cliente fue eliminado con éxito");
                }
                catch (Exception ex)
                {
                    return ResponseHelper<Clients>.MakeResponseFail(ex);
                }
            }

            public async Task<Response<Clients>> EditAsync(Clients model)
            {
                try
                {
                    _context.Clients.Update(model);
                    await _context.SaveChangesAsync();

                    return ResponseHelper<Clients>.MakeResponseSuccess(model, "Cliente editado con éxito");
                }
                catch (Exception ex)
                {
                    return ResponseHelper<Clients>.MakeResponseFail(ex);
                }
            }

            public async Task<Response<List<Clients>>> GetListAsync()
            {
                try
                {
                    List<Clients> list = await _context.Clients.ToListAsync();

                    return ResponseHelper<List<Clients>>.MakeResponseSuccess(list, "Clientes obtenidos con éxito");
                }
                catch (Exception ex)
                {
                    return ResponseHelper<List<Clients>>.MakeResponseFail(ex);
                }
            }

            public async Task<Response<Clients>> GetOneAsync(int id)
            {
              try
                {
                    Clients? clients = await _context.Clients.FirstOrDefaultAsync(C => C.Id == id);

                    if (clients is null)
                    {
                        return ResponseHelper<Clients>.MakeResponseFail($"No existe un cliente con este id '{id}'.");
                    }

                    return ResponseHelper<Clients>.MakeResponseSuccess(clients);
                }
                catch (Exception ex)
                {
                    return ResponseHelper<Clients>.MakeResponseFail(ex);
                }
            }
        }

    }
}