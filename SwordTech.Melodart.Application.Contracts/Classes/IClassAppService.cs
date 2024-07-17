using SwordTech.Melodart.Application.Contract.Base;
using SwordTech.Melodart.Application.Contract.Classes.Models;
using SwordTech.Melodart.Domain.Classes;

namespace SwordTech.Melodart.Application.Contract.Classes;

public interface IClassAppService : IAppService<Class, ClassDto, ClassDto, ClassCreateDto, ClassUpdateDto>
{
}
