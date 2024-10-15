using AutoMapper;
using SwordTech.Melodart.Application.Base;
using SwordTech.Melodart.Application.Contract.Lessons;
using SwordTech.Melodart.Application.Contract.Lessons.Models;
using SwordTech.Melodart.Domain.Lessons;
using SwordTech.Melodart.EFCore.Repositories.Base;

namespace SwordTech.Melodart.Application.Lessons
{
    public class ScheduleAppService : AppService<Schedule, ScheduleDto, ScheduleDto, ScheduleCreateDto, ScheduleUpdateDto>, IScheduleAppService
    {
        public ScheduleAppService(IEfBaseRepository<Schedule> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async override Task<IList<ScheduleDto>> GetAll()
        {
            return ( await base.GetAll()).OrderBy(x=>x.ScheduleTime).ToList();
        }
    }
}
