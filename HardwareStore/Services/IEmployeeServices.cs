﻿using Microsoft.EntityFrameworkCore;
using HardwareStore.Core;
using HardwareStore.Data;
using HardwareStore.Data.Entities;
using HardwareStore.Helpers;
using HardwareStore.Requests;
using System.Collections.Generic;
using static HardwareStore.Services.IEmployeeServices;
using static System.Collections.Specialized.BitVector32;

namespace HardwareStore.Services
{
    public interface IEmployeeServices
    {
        public Task<Response<Employee>> CreateEmployeeAsync(Employee model);//Crear un Empleado
        public Task<Response<List<Employee>>> GetListEmployeeAsync();//Trae una lista de los empleados
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
            public async Task<Response<List<Employee>>> GetListEmployeeAsync()
            {
                try
                {
                    List<Employee> list = await _context.Employees.ToListAsync();

                    return ResponseHelper<List<Employee>>.MakeResponseSuccess(list, "Empleados obtenidos con éxito");
                }
                catch (Exception ex)
                {
                    return ResponseHelper<List<Employee>>.MakeResponseFail(ex);
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
