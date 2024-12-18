using HR.Domain.Classes;
using HR.Infrastructure.Common;

namespace HR.Infrastructure.Interfaces
{
    public interface IPayrollRepository : IGenericRepository<Payroll>
    {
        public Task<IEnumerable<Payroll>> GetByEmployeeID(string employeeId);
        public Task<IEnumerable<Payroll>> GetByDate(int month, int year);
        public Task<Payroll> GetByDateforEmployee(string Employeeid, int month, int year);
    }
}
