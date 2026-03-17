using Microsoft.EntityFrameworkCore;

namespace WebApplication1demo.Models
{
	class EmployeeDBContext : DbContext
	{
		public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options) : base(options)
		{
		}
		public DbSet<EmployeeModel> employees { get; set; }
	}

}
