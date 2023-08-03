using EmpMgnt.Models;
using Microsoft.EntityFrameworkCore;

namespace EmpMgnt.Data
{
    public class EmpMgntDbContext : DbContext
    {
        public EmpMgntDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }
    }
}
