using HardwareStore.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace HardwareStore.Data.Entities
{
    public class RolePermission
    {
      
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
