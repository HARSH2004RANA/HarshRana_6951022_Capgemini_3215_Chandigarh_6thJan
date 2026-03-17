using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using StudentManagement.Filters;
using System;
using System.Collections.Generic;

namespace StudentManagement.Controllers
{
	[ServiceFilter(typeof(LogActionFilter))]
	public class ProductController : Controller
	{
		static List<Product> products = new List<Product>();

		public IActionResult Index()
		{
			// Test exception filter
			throw new Exception("Test Exception");

			return View(products);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Product product)
		{
			product.Id = products.Count + 1;
			products.Add(product);
			return RedirectToAction("Index");
		}

		public IActionResult Details(int id)
		{
			var product = products.Find(p => p.Id == id);
			return View(product);
		}
	}
}