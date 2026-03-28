using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace ProductClient.Pages
{
    public class EditProductModel : PageModel
    {
        private readonly IHttpClientFactory _factory;

        public EditProductModel(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        [BindProperty]
        public Product Product { get; set; } = new();

        public async Task OnGetAsync(int id)
        {
            var client = _factory.CreateClient("ProductApi");

            var res = await client.GetAsync($"products/{id}");
            var json = await res.Content.ReadAsStringAsync();

            Product = JsonSerializer.Deserialize<Product>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _factory.CreateClient("ProductApi");

            var json = JsonSerializer.Serialize(Product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await client.PutAsync($"products/{Product.Id}", content);

            return RedirectToPage("/Products");
        }
    }
}