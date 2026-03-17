using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HotelManagementSystem.Data
{
	public class HotelDbContextFactory : IDesignTimeDbContextFactory<HotelDbContext>
	{
		public HotelDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<HotelDbContext>();

			optionsBuilder.UseSqlServer(
				"Data Source=RAGHVISHARMA\\SQLEXPRESS;Initial Catalog=HotelManagementDB;Integrated Security=True;TrustServerCertificate=True;"
			);

			return new HotelDbContext(optionsBuilder.Options);
		}
	}
}