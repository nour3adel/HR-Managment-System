using HR.Domain.Classes;
using HR.Domain.Classes.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HR.Infrastructure.Context
{
    public class HRdbContext : IdentityDbContext<Employee, Role, string>
    {

        public HRdbContext() : base() { }

        // Constructor accepting DbContextOptions for dependency injection
        public HRdbContext(DbContextOptions<HRdbContext> options)
            : base(options) { }

        // DbSet properties for your entities
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }
        public virtual DbSet<Payroll> Payrolls { get; set; }
        public virtual DbSet<PerformanceReview> PerformanceReviews { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

    }
}