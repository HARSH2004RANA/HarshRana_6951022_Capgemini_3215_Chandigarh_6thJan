using Microsoft.AspNetCore.Mvc;
using ProductManagement.Filters;
using ProductManagement.Models;

namespace ProductManagement.Controllers
{
	public class ProductController : Controller
	{
		[ServiceFilter(typeof(LogActionFilter))]
		public IActionResult Index()
		{
			List<Product> products = new List<Product>()
			{
				new Product{Id=1, Name="Laptop", Price=60000},
				new Product{Id=2, Name="Mobile", Price=20000},
				new Product{Id=3, Name="Headphones", Price=3000}
			};

			return View(products);
		}
	}
}