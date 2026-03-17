namespace ProductApp.Services
{
	public class ProductService : IProductService
	{
		public List<string> GetProducts()
		{
			return new List<string> { "Laptop", "Phone", "Tablet" };
		}
	}
}
