using Microsoft.EntityFrameworkCore;
namespace TokenAuthLogin.Models{
    public class EmployeeContext:DbContext{
        public EmployeeContext(DbContextOptions<EmployeeContext> options)
        :base(options)
        {
        }
        public DbSet<Employee> employees {get;set;}
    }
}