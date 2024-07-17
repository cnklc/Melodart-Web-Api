using SwordTech.Melodart.Application.Contract.Base;
using SwordTech.Melodart.Application.Contract.Teachers.Models;
using SwordTech.Melodart.Domain.Teachers;

namespace SwordTech.Melodart.Application.Contract.Teachers;

public interface ITeacherAppService : IAppService<Teacher, TeacherDto, TeacherDto, TeacherCreateDto, TeacherUpdateDto>
{
}
