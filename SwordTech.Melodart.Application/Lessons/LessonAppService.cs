using AutoMapper;
using SwordTech.Melodart.Application.Base;
using SwordTech.Melodart.Application.Contract.Lessons;
using SwordTech.Melodart.Application.Contract.Lessons.Models;
using SwordTech.Melodart.Domain.Lessons;
using SwordTech.Melodart.EFCore.Repositories.Base;

namespace SwordTech.Melodart.Application.Lessons
{
    public class LessonAppService : AppService<Lesson, LessonDto, LessonDto, LessonCreateDto, LessonUpdateDto>, ILessonAppService
    {
        public LessonAppService(IEfBaseRepository<Lesson> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
