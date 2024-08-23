using AutoMapper;
using SwordTech.Melodart.Application.Base;
using SwordTech.Melodart.Application.Contract.Students;
using SwordTech.Melodart.Application.Contract.Students.Models;
using SwordTech.Melodart.Domain.Students;
using SwordTech.Melodart.EFCore.Repositories;
using SwordTech.Melodart.EFCore.Repositories.Base;

namespace SwordTech.Melodart.Application.Students;

public class ParentAppService : AppService<Parent, ParentDto, ParentDto, ParentCreateDto, ParentUpdateDto>, IParentAppService
{
    public ParentAppService(IEfBaseRepository<Parent> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}