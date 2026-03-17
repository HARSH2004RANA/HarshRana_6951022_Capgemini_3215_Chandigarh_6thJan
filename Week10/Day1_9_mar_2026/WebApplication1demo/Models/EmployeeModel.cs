using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace WebApplication1demo.Models
{
	[Table("tblEmployees")]

	public class EmployeeModel
    {
		//[Key]
		public int EmployeeModelId { get; set; }

		public string EName { get; set; }
		public string Job { get; set; }
		public int Salary { get; set; }
	}
}
