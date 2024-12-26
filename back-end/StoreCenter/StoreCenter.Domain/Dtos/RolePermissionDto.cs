namespace StoreCenter.Domain.Dtos
{
    public class RolePermissionDto
    {
        public Guid RoleId { get; set; }
        public string? RoleName { get; set; }
        public Guid PermissionId { get; set; }
        public string? PermissionName { get; set; }
    }
}
