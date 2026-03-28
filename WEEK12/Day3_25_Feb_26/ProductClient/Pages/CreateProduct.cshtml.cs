using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace ProductClient.Pages
{
    public class CreateProductModel : PageModel
    {
        private readonly IHttpClientFactory _factory;

        public CreateProductModel(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        [BindProperty]
        public Product Product { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _factory.CreateClient("ProductApi");

            var json = JsonSerializer.Serialize(Product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await client.PostAsync("products", content);

            return RedirectToPage("/Products");
        }
    }
}