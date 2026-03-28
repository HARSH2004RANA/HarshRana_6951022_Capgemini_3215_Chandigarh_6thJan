using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace ProductClient.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly IHttpClientFactory _factory;

        public ProductsModel(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public List<Product> Products { get; set; } = new();

        public async Task OnGetAsync()
        {
            var client = _factory.CreateClient("ProductApi");

            var response = await client.GetAsync("products");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                Products = JsonSerializer.Deserialize<List<Product>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            }
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
    }
}