using AutoMapper;
using SwordTech.Melodart.Application.Contract.Classes.Models;
using SwordTech.Melodart.Domain.Classes;

namespace SwordTech.Melodart.Application.Contract.Classes.Mappers;
 
public class ClassMapperProfile : Profile
{
    public ClassMapperProfile()
    {
        CreateMap<Class, ClassDto>();
        CreateMap<ClassCreateDto, Class>();
        CreateMap<ClassUpdateDto, Class>();
    }
}
