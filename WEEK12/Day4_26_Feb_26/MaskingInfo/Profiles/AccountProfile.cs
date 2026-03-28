using AutoMapper;
using MaskingInfo.Models;
using MaskingInfo.DTOs;

namespace MaskingInfo.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountDTO>()
                .ForMember(dest => dest.AccountNumber,
                    opt => opt.MapFrom(src => MaskAccountNumber(src.AccountNumber)));
        }

        private string MaskAccountNumber(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber))
                return accountNumber;

            // If length <= 4, don't mask
            if (accountNumber.Length <= 4)
                return accountNumber;

            int visibleDigits = 4;
            int maskedLength = accountNumber.Length - visibleDigits;

            string maskedPart = new string('X', maskedLength);
            string visiblePart = accountNumber.Substring(accountNumber.Length - visibleDigits);

            return maskedPart + visiblePart;
        }
    }
}