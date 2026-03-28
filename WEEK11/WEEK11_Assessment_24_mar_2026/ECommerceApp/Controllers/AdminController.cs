using ECommerceApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            // 🔐 SESSION CHECK
            var isAdmin = HttpContext.Session.GetString("Admin");

            if (isAdmin != "true")
            {
                return RedirectToAction("Login", "Account");
            }

            // 🔥 Total Counts
            ViewBag.TotalProducts = _context.Products.Count();
            ViewBag.TotalCustomers = _context.Customers.Count();
            ViewBag.TotalOrders = _context.Orders.Count();

            // 🔥 Top 5 Products
            var topProducts = _context.OrderItems
                .Include(o => o.Product)
                .GroupBy(o => o.Product.Name)
                .Select(g => new
                {
                    Product = g.Key,
                    Total = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.Total)
                .Take(5)
                .ToList();

            // 🔥 Pending Orders
            var pendingOrders = _context.ShippingDetails
                .Include(s => s.Order)
                .Where(s => s.Status == "Pending")
                .ToList();

            ViewBag.TopProducts = topProducts;
            ViewBag.PendingOrders = pendingOrders;

            return View();
        }
    }
}