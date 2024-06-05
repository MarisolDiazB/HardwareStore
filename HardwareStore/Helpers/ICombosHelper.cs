using HardwareStore.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HardwareStore.Helpers
    {
        public interface ICombosHelper
        {
          
            Task<IEnumerable<SelectListItem>> GetComboRolesAsync();
            Task<IEnumerable<SelectListItem>> GetComboEmployees();
        }

        public class CombosHelper : ICombosHelper
        {
            private readonly DataContext _context;

            public CombosHelper(DataContext context)
            {
                _context = context;
            }

        public async Task<IEnumerable<SelectListItem>> GetComboRolesAsync()
        {
            List<SelectListItem> list = await _context.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString(),
            }).ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un rol...]",
                Value = "0"
            });

            return list;
        }

        // Obtiene la lista de empleados para un combo.
        public async Task<IEnumerable<SelectListItem>> GetComboEmployees()
            {
                var employees = await _context.Employees
                    .Select(e => new SelectListItem
                    {
                        Text = $"{e.FirstName} {e.LastName}",
                        Value = e.Id.ToString(),
                    })
                    .ToListAsync();

                // Agrega un elemento predeterminado al principio de la lista.
                employees.Insert(0, new SelectListItem
                {
                    Text = "[Seleccione un Empleado...]",
                    Value = "0"
                });

                return employees;
            }
        }
    }

