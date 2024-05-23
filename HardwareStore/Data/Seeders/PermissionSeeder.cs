/*using HardwareStore.Data.Entities;

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
                Permission? tmpPermission = _context.Permissions.Where(p => p.Name == permission.Name && p.Module == permission.Module)
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
                new Permission { Name = "showProducts", Description = "Ver Productos", Module = "Products" },
                new Permission { Name = "createProducts", Description = "Crear Productos", Module = "Products" },
                new Permission { Name = "updateProducts", Description = "Editar Productos", Module = "Products" },
                new Permission { Name = "deleteProducts", Description = "Eliminar Productos", Module = "Products" },
            };

            return list;
        }
        private List<Permission> Customer()
        {
            List<Permission> list = new List<Permission>
            {
                new Permission { Name = "showCustomer", Description = "Ver Clientes", Module = "Customer" },
                new Permission { Name = "createCustomer", Description = "Crear Clientes", Module = "Customer" },
                new Permission { Name = "updateCustomer", Description = "Editar Clientes", Module = "Customer" },
                new Permission { Name = "deleteCustomer", Description = "Eliminar Clientes", Module = "Customer" },
            };

            return list;
        }

        private List<Permission> Employee()
        {
            List<Permission> list = new List<Permission>
            {
                new Permission { Name = "showEmployee", Description = "Ver Empleados", Module = "Employee" },
                new Permission { Name = "createEmployee", Description = "Crear Empleados", Module = "Employee" },
                new Permission { Name = "updateEmployee", Description = "Editar Empleados", Module = "Employee" },
                new Permission { Name = "deleteEmployee", Description = "Eliminar Empleados", Module = "Employee" },
            };

            return list;
        }
        private List<Permission> Roles()
        {
            List<Permission> list = new List<Permission>
            {
                new Permission { Name = "showRoles", Description = "Ver Roles", Module = "Roles" },
                new Permission { Name = "createRoles", Description = "Crear Roles", Module = "Roles" },
                new Permission { Name = "updateRoles", Description = "Editar Roles", Module = "Roles" },
                new Permission { Name = "deleteRoles", Description = "Eliminar Roles", Module = "Roles" },
            };

            return list;
        }

        private List<Permission> Users()
        {
            List<Permission> list = new List<Permission>
            {
                new Permission { Name = "showUsers", Description = "Ver Usuarios", Module = "Usuarios" },
                new Permission { Name = "createUsers", Description = "Crear Usuarios", Module = "Usuarios" },
                new Permission { Name = "updateUsers", Description = "Editar Usuarios", Module = "Usuarios" },
                new Permission { Name = "deleteUsers", Description = "Eliminar Usuarios", Module = "Usuarios" },
            };

            return list;
        }

    }
}*/
using HardwareStore.Data.Entities; // Importa el espacio de nombres para las entidades de datos.
using System.Collections.Generic; // Importa el espacio de nombres para las listas genéricas.
using System.Linq; // Importa el espacio de nombres para realizar consultas LINQ.
using System.Threading.Tasks; // Importa el espacio de nombres para tareas asincrónicas.

namespace HardwareStore.Data.Seeders // Define el espacio de nombres y declara la clase PermissionSeeder.
{
    public class PermissionSeeder // Declara la clase PermissionSeeder.
    {
        private readonly DataContext _context; // Declara una variable privada para el contexto de datos.

        // Constructor de la clase PermissionSeeder que recibe el contexto de datos como parámetro.
        public PermissionSeeder(DataContext context)
        {
            _context = context; // Asigna el contexto de datos recibido al campo privado.
        }

        // Método para sembrar datos de permisos de forma asíncrona.
        public async Task SeedAsync()
        {
            List<Permission> permissions = new List<Permission>(); // Crea una lista de permisos vacía.

            // Agrega permisos de diferentes módulos a la lista.
            permissions.AddRange(Products());
            permissions.AddRange(Roles());
            permissions.AddRange(Users());
            permissions.AddRange(Customer());
            permissions.AddRange(Employee());

            foreach (Permission permission in permissions) // Itera sobre cada permiso en la lista.
            {
                // Busca si el permiso ya existe en la base de datos.
                Permission? tmpPermission = _context.Permissions
                    .Where(p => p.Name == permission.Name && p.Module == permission.Module)
                    .FirstOrDefault();

                if (tmpPermission is null) // Si el permiso no existe en la base de datos.
                {
                    _context.Permissions.Add(permission); // Agrega el permiso al contexto.
                }
            }

            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos.
        }

        // Método para obtener una lista de permisos relacionados con productos.
        private List<Permission> Products()
        {
            List<Permission> list = new List<Permission> // Crea una lista de permisos.
            {
                // Agrega permisos relacionados con productos a la lista.
                new Permission { Name = "showProducts", Description = "Ver Productos", Module = "Products", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "createProducts", Description = "Crear Productos", Module = "Products", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "updateProducts", Description = "Editar Productos", Module = "Products", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "deleteProducts", Description = "Eliminar Productos", Module = "Products", RolePermissions = new List<RolePermission>() },
            };

            return list; // Devuelve la lista de permisos relacionados con productos.
        }

        // Método para obtener una lista de permisos relacionados con clientes.
        private List<Permission> Customer()
        {
            List<Permission> list = new List<Permission> // Crea una lista de permisos.
            {
                // Agrega permisos relacionados con clientes a la lista.
                new Permission { Name = "showCustomer", Description = "Ver Clientes", Module = "Customer", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "createCustomer", Description = "Crear Clientes", Module = "Customer", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "updateCustomer", Description = "Editar Clientes", Module = "Customer", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "deleteCustomer", Description = "Eliminar Clientes", Module = "Customer", RolePermissions = new List<RolePermission>() },
            };

            return list; // Devuelve la lista de permisos relacionados con clientes.
        }

        // Método para obtener una lista de permisos relacionados con empleados.
        private List<Permission> Employee()
        {
            List<Permission> list = new List<Permission> // Crea una lista de permisos.
            {
                // Agrega permisos relacionados con empleados a la lista.
                new Permission { Name = "showEmployee", Description = "Ver Empleados", Module = "Employee", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "createEmployee", Description = "Crear Empleados", Module = "Employee", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "updateEmployee", Description = "Editar Empleados", Module = "Employee", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "deleteEmployee", Description = "Eliminar Empleados", Module = "Employee", RolePermissions = new List<RolePermission>() },
            };

            return list; // Devuelve la lista de permisos relacionados con empleados.
        }

        // Método para obtener una lista de permisos relacionados con roles.
        private List<Permission> Roles()
        {
            List<Permission> list = new List<Permission> // Crea una lista de permisos.
            {
                // Agrega permisos relacionados con roles a la lista.
                new Permission { Name = "showRoles", Description = "Ver Roles", Module = "Roles", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "createRoles", Description = "Crear Roles", Module = "Roles", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "updateRoles", Description = "Editar Roles", Module = "Roles", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "deleteRoles", Description = "Eliminar Roles", Module = "Roles", RolePermissions = new List<RolePermission>() },
            };

            return list; // Devuelve la lista de permisos relacionados con roles.
        }

        // Método para obtener una lista de permisos relacionados con usuarios.
        private List<Permission> Users()
        {
            List<Permission> list = new List<Permission> // Crea una lista de permisos.
            {
                // Agrega permisos relacionados con usuarios a la lista.
                new Permission { Name = "showUsers", Description = "Ver Usuarios", Module = "Usuarios", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "createUsers", Description = "Crear Usuarios", Module = "Usuarios", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "updateUsers", Description = "Editar Usuarios", Module = "Usuarios", RolePermissions = new List<RolePermission>() },
                new Permission { Name = "deleteUsers", Description = "Eliminar Usuarios", Module = "Usuarios", RolePermissions = new List<RolePermission>() },
            };

            return list; // Devuelve la lista de permisos relacionados con usuarios.
        }
    }
}

