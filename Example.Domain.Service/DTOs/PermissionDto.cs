namespace Example.Domain.Service
{
    public class PermissionDto
    {
        public int Id { get; set; }
        public string? EmployeeName { get; set; }
        public string? LastNameEmployee { get; set; }
        public int TypePermit { get; set; }
        public DateTime DatePermission { get; set; }
    }
}
