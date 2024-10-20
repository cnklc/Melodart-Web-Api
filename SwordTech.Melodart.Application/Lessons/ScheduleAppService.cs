using AutoMapper;
using SwordTech.Melodart.Application.Base;
using SwordTech.Melodart.Application.Contract.Lessons;
using SwordTech.Melodart.Application.Contract.Lessons.Models;
using SwordTech.Melodart.Domain.Contracts.Lessons;
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
            return (await base.GetAll()).OrderBy(x => x.ScheduleTime).ToList();
        }
        
        public async Task<IList<ScheduleDto>> GetToday()
        {
            return (await base.GetAll(x=>x.ScheduleTime.Date == DateTime.Now.Date && x.ScheduleStatusType == ScheduleStatusType.Pending)).OrderBy(x => x.ScheduleTime).ToList();
        }

        public override async Task<ScheduleDto> Update(Guid id, ScheduleUpdateDto input)
        {
            var entity = _repository.GetById(id);

            entity.ScheduleStatusType = input.ScheduleStatusType;

            if (input.ScheduleTime != null)
            {
                entity.ScheduleTime = input.ScheduleTime.Value;
            }
            if (input.Description != null)
            {
                entity.Description = input.Description;
            }

            _repository.Update(entity);

            return await GetById(entity.Id);
        }
       
    }
}
