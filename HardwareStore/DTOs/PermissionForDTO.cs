using HardwareStore.Data.Entities;

namespace HardwareStore.DTOs
{
    public class PermissionForDTO : Permission
    {
        public bool Selected { get; set; } = false;
    }
}
