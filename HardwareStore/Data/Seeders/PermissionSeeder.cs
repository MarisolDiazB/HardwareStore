using HardwareStore.Data.Entities; 

namespace HardwareStore.Data.Seeders 
{
    public class PermissionSeeder 
    {
        private readonly DataContext _context;

        
        public PermissionSeeder(DataContext context)
        {
            _context = context; 
        }

        
        public async Task SeedAsync()
        {
            List<Permission> permissions = new List<Permission>(); 
          
            permissions.AddRange(Products());
            permissions.AddRange(Roles());
            permissions.AddRange(Users());
            permissions.AddRange(Customer());
            permissions.AddRange(Employee());

            foreach (Permission permission in permissions) 
            {
               
                Permission? tmpPermission = _context.Permissions
                    .Where(p => p.Name == permission.Name && p.Module == permission.Module)
                    .FirstOrDefault();

                if (tmpPermission is null) 
                {
                    _context.Permissions.Add(permission); 
                }
            }

            await _context.SaveChangesAsync(); 
        }

        private List<Permission> Products()
        {
            List<Permission> list = new List<Permission> 
            {
                
                new Permission { Name = "showProducts", Description = "Ver Productos", Module = "Products", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "createProducts", Description = "Crear Productos", Module = "Products", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "updateProducts", Description = "Editar Productos", Module = "Products", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "deleteProducts", Description = "Eliminar Productos", Module = "Products", RolePermissions = new List<RolePermission>() },
            };

            return list; 
        }

        private List<Permission> Customer()
        {
            List<Permission> list = new List<Permission> 
            {
                
                new Permission { Name = "showCustomer", Description = "Ver Clientes", Module = "Customer", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "createCustomer", Description = "Crear Clientes", Module = "Customer", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "updateCustomer", Description = "Editar Clientes", Module = "Customer", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "deleteCustomer", Description = "Eliminar Clientes", Module = "Customer", RolePermissions = new List<RolePermission>() },
            };

            return list; 
        }

        
        private List<Permission> Employee()
        {
            List<Permission> list = new List<Permission> 
            {
                
                new Permission { Name = "showEmployee", Description = "Ver Empleados", Module = "Employee", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "createEmployee", Description = "Crear Empleados", Module = "Employee", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "updateEmployee", Description = "Editar Empleados", Module = "Employee", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "deleteEmployee", Description = "Eliminar Empleados", Module = "Employee", RolePermissions = new List<RolePermission>() },
            };

            return list; 
        }

       
        private List<Permission> Roles()
        {
            List<Permission> list = new List<Permission> 
            {
                
                new Permission { Name = "showRoles", Description = "Ver Roles", Module = "Roles", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "createRoles", Description = "Crear Roles", Module = "Roles", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "updateRoles", Description = "Editar Roles", Module = "Roles", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "deleteRoles", Description = "Eliminar Roles", Module = "Roles", RolePermissions = new List<RolePermission>() },
            };

            return list; 
        }

        
        private List<Permission> Users()
        {
            List<Permission> list = new List<Permission> 
            {
                
                new Permission { Name = "showUsers", Description = "Ver Usuarios", Module = "Usuarios", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "createUsers", Description = "Crear Usuarios", Module = "Usuarios", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "updateUsers", Description = "Editar Usuarios", Module = "Usuarios", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "deleteUsers", Description = "Eliminar Usuarios", Module = "Usuarios", RolePermissions = new List<RolePermission>() },
            };

            return list; 
        }
    }
}

