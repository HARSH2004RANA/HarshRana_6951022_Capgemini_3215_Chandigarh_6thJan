namespace BankingApi.Models;

public class Account
{
    public int Id { get; set; }
    public string AccountHolderName { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public string UserEmail { get; set; } = string.Empty;
}