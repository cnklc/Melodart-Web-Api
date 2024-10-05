using AutoMapper;
using SwordTech.Melodart.Application.Contract.Finance.Models;
using SwordTech.Melodart.Domain.Finance;

namespace SwordTech.Melodart.Application.Contract.Finance.Mappers;

public class AccountMapperProfile : Profile
{
    public AccountMapperProfile()
    {
        CreateMap<Account, AccountDto>()
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Transactions.Where(x=>!x.IsDeleted).Sum(x => x.Amount)));

        CreateMap<Account, AccountDetailDto>()
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Transactions.Where(x => !x.IsDeleted).Sum(x => x.Amount)))
            .ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions.Where(x => !x.IsDeleted)));
        
        CreateMap<AccountCreateDto, Account>();
        CreateMap<AccountUpdateDto, Account>();
    }
}
