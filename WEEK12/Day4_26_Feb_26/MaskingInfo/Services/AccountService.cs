using MaskingInfo.Models;

namespace MaskingInfo.Services
{
    public class AccountService
    {
        public List<Account> GetAccounts()
        {
            return new List<Account>
            {
                new Account { Id = 1, AccountNumber = "1234567890", AccountHolderName = "John" },
                new Account { Id = 2, AccountNumber = "98765", AccountHolderName = "Alice" },
                new Account { Id = 3, AccountNumber = "1234", AccountHolderName = "Bob" }
            };
        }
    }
}