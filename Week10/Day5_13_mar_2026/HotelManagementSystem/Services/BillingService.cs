namespace HotelManagementSystem.Services
{
	public class BillingService
	{
		public int CalculateBill(int days, int roomPrice, int vehicles, int food)
		{
			int bill = 0;

			bill += days * roomPrice;
			bill += vehicles * days * 50;
			bill += food * 500;

			return bill;
		}
	}
}