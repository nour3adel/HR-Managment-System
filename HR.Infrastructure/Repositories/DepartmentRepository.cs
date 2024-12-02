using HR.Domain.Classes;
using HR.Infrastructure.Common;
using HR.Infrastructure.Context;
using HR.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HR.Infrastructure.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly DbSet<Department> _departments;
        public DepartmentRepository(HRdbContext dbContext) : base(dbContext)
        {
            _departments = dbContext.Set<Department>();
        }

        public async Task<List<Department>> GetAllDepartments()
        {
            return await _departments.ToListAsync();
        }
    }
}
