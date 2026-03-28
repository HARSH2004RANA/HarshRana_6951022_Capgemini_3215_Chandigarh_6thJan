using AutoMapper;
using BankingApi.Models;
using BankingApi.DTOs;

namespace BankingApi.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ✅ User Mappings
        CreateMap<User, UserDTO>();
        CreateMap<RegisterDTO, User>();

        // ✅ Account Mapping (WITH MASKING LOGIC)
        CreateMap<Account, AccountDTO>()
            .ForMember(dest => dest.MaskedAccountNumber,
                opt => opt.MapFrom(src =>
                    src.AccountNumber != null && src.AccountNumber.Length >= 4
                        ? "XXXXXXXX" + src.AccountNumber.Substring(src.AccountNumber.Length - 4)
                        : "INVALID"
                ));
    }
}