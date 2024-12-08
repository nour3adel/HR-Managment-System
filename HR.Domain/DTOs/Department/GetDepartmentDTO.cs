namespace HR.Domain.DTOs.Department
{
    public class GetDepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<string> EmployeeNames { get; set; } = new List<string>();
    }
}
