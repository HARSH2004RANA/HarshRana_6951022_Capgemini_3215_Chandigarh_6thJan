using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerceApp.Data;
using ECommerceApp.Models;

namespace ECommerceApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // 🔷 INDEX
        public async Task<IActionResult> Index()
        {
            var orders = _context.Orders.Include(o => o.Customer);
            return View(await orders.ToListAsync());
        }

        // 🔷 DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.ShippingDetail)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null) return NotFound();

            return View(order);
        }

        // 🔷 CREATE (GET)
        public IActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(_context.Customers, "CustomerId", "Email");
            ViewBag.Products = _context.Products.ToList();

            return View();
        }

        // 🔷 CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order order, int[] productIds, int[] quantities)
        {
            if (ModelState.IsValid)
            {
                _context.Orders.Add(order);
                _context.SaveChanges();

                // 🔥 ADD ORDER ITEMS
                if (productIds != null && quantities != null && productIds.Length == quantities.Length)
                {
                    for (int i = 0; i < productIds.Length; i++)
                    {
                        var item = new OrderItem
                        {
                            OrderId = order.OrderId,
                            ProductId = productIds[i],
                            Quantity = quantities[i]
                        };

                        _context.OrderItems.Add(item);
                    }

                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.CustomerId = new SelectList(_context.Customers, "CustomerId", "Email", order.CustomerId);
            ViewBag.Products = _context.Products.ToList();

            return View(order);
        }

        // 🔷 ORDER HISTORY
        public IActionResult OrderHistory(int customerId)
        {
            var orders = _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.ShippingDetail)
                .Where(o => o.CustomerId == customerId)
                .ToList();

            return View(orders);
        }
    }
}