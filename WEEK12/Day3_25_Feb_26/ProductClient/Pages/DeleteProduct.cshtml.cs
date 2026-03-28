using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProductClient.Pages
{
    public class DeleteProductModel : PageModel
    {
        private readonly IHttpClientFactory _factory;

        public DeleteProductModel(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _factory.CreateClient("ProductApi");

            await client.DeleteAsync($"products/{id}");

            return RedirectToPage("/Products");
        }
    }
}