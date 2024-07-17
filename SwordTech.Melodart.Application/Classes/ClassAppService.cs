using AutoMapper;
using SwordTech.Melodart.Application.Base;
using SwordTech.Melodart.Application.Contract.Classes;
using SwordTech.Melodart.Application.Contract.Classes.Models;
using SwordTech.Melodart.Domain.Classes;
using SwordTech.Melodart.EFCore.Repositories;

namespace SwordTech.Melodart.Application.Classes;

public class ClassAppService : AppService<Class, ClassDto, ClassDto, ClassCreateDto, ClassUpdateDto>, IClassAppService
{
    public ClassAppService(IEfBaseRepository<Class> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
