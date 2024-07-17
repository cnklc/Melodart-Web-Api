using SwordTech.Melodart.Application.Contract.Base;
using SwordTech.Melodart.Application.Contract.Students.Models;
using SwordTech.Melodart.Domain.Students;

namespace SwordTech.Melodart.Application.Contract.Students;

public interface IParentAppService : IAppService<Parent, ParentDto, ParentDto, ParentCreateDto, ParentUpdateDto>
{
}
