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
            return (await base.GetAll(x => x.ScheduleTime.Date <= DateTime.Now.Date && x.ScheduleStatusType == ScheduleStatusType.Pending)).OrderBy(x => x.ScheduleTime).ToList();
        }

        public async Task<ScheduleDto> CreateCompensationSchedule(CreateCompensationScheduleDto input)
        {
            using (var transaction = _repository.BeginTransaction())
            {
                try
                {
                    Schedule oldSchedule = _repository.GetById(input.ScheduleId);
                    oldSchedule.ScheduleStatusType = ScheduleStatusType.LessonCreated;

                    _repository.Update(oldSchedule);

                    var date = input.Date.Add(TimeSpan.Parse(input.Time));

                    Schedule newSchedule = new Schedule()
                    {
                        StudentId = oldSchedule.StudentId,
                        DepartmentId = oldSchedule.DepartmentId,
                        TeacherId = oldSchedule.TeacherId,
                        ScheduleId = oldSchedule.Id,
                        LessonId = oldSchedule.LessonId,
                        ScheduleStatusType = ScheduleStatusType.Pending,
                        DayOfTheWeek = oldSchedule.DayOfTheWeek,
                        TimeOfDay = oldSchedule.TimeOfDay,
                        Duration = oldSchedule.Duration,
                        ScheduleTime = date,
                    };

                    _repository.Add(newSchedule);
                    await transaction.CommitAsync();

                    return await GetById(newSchedule.Id);
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }

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

        public async Task<ScheduleDto> ChangeSchedule(SchedulChangeDto input)
        {
            using (var transaction = _repository.BeginTransaction())
            {
                try
                {
                    var entity = _repository.GetById(input.Id);

                    entity.ScheduleStatusType = ScheduleStatusType.Deferred;

                    var newEntity = new Schedule()
                    {
                        ScheduleId = entity.Id,
                        LessonId = entity.LessonId,
                        DepartmentId = entity.DepartmentId,
                        TeacherId = entity.TeacherId,
                        StudentId = entity.StudentId,
                        DayOfTheWeek = entity.DayOfTheWeek,
                        TimeOfDay = entity.TimeOfDay,
                        Duration = entity.Duration,
                        ScheduleStatusType = ScheduleStatusType.Pending,
                        ScheduleTime = input.NewDate
                    };

                    _repository.Update(entity);
                    _repository.Add(newEntity);
                    
                    await transaction.CommitAsync();

                    return await GetById(newEntity.Id);
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
        public Task<IList<ScheduleDto>> GetScheduleByLessonId(Guid lessonId)
        {
            return base.GetAll(x => x.LessonId == lessonId);
        }
    }
}
