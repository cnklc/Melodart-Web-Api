using AutoMapper;
using SwordTech.Melodart.Application.Contract.Finance.Models;
using SwordTech.Melodart.Domain.Finance;

namespace SwordTech.Melodart.Application.Contract.Finance.Mappers;

public class TransactionMapperProfile : Profile
{
    public TransactionMapperProfile()
    {
        CreateMap<Transaction, TransactionDto>();
        CreateMap<TransactionCreateDto, Transaction>();
        CreateMap<TransactionUpdateDto, Transaction>();
    }
}
