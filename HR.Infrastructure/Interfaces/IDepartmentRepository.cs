using HR.Domain.Classes;
using HR.Infrastructure.Common;

namespace HR.Infrastructure.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        public Task<List<Department>> GetAllDepartments();
    }
}
