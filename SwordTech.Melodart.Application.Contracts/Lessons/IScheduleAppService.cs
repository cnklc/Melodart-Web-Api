using SwordTech.Melodart.Application.Contract.Base;
using SwordTech.Melodart.Application.Contract.Lessons.Models;
using SwordTech.Melodart.Domain.Lessons;

namespace SwordTech.Melodart.Application.Contract.Lessons;

public interface IScheduleAppService : IAppService<Schedule, ScheduleDto, ScheduleDto, ScheduleCreateDto, ScheduleUpdateDto>
{
    Task<IList<ScheduleDto>> GetToday();
}
