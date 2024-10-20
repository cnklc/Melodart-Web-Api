using AutoMapper;
using SwordTech.Melodart.Application.Base;
using SwordTech.Melodart.Application.Contract.Lessons;
using SwordTech.Melodart.Application.Contract.Lessons.Models;
using SwordTech.Melodart.Domain.Contracts.Lessons;
using SwordTech.Melodart.Domain.Lessons;
using SwordTech.Melodart.EFCore.Repositories.Base;

namespace SwordTech.Melodart.Application.Lessons
{
    public class LessonAppService : AppService<Lesson, LessonDto, LessonDto, LessonCreateDto, LessonUpdateDto>, ILessonAppService
    {
        private IEfBaseRepository<Schedule> _scheduleRepository;

        
        public LessonAppService(IEfBaseRepository<Lesson> repository, IMapper mapper, IEfBaseRepository<Schedule> scheduleRepository) : base(repository, mapper)
        {
            _scheduleRepository = scheduleRepository;
        }

        public override async Task<LessonDto> Create(LessonCreateDto input)
        {
            using (var transaction = _repository.BeginTransaction())
            {
                try
                {
                    var entity = _mapper.Map<Lesson>(input);
                    _repository.Add(entity);


                    var times = CreateMonthlySchedule((DayOfWeek)input.DayOfTheWeek, TimeSpan.Parse(input.TimeOfDay), DateTime.Now);
                    
                   
                    
                    times.ForEach(item =>
                    {
                        var schedule = new Schedule()
                        {
                            ScheduleStatusType = ScheduleStatusType.Pending,
                            ScheduleTime = item,
                            DayOfTheWeek = input.DayOfTheWeek,
                            TimeOfDay = TimeSpan.Parse(input.TimeOfDay),
                            Duration = input.Duration,
                            TeacherId = input.TeacherId,
                            DepartmentId = input.DepartmentId,
                            StudentId = input.StudentId,
                            LessonId = entity.Id,
                            Description = ""
                        };
                        _scheduleRepository.Add(schedule);
                    });
                    
                    await transaction.CommitAsync();

                    return await GetById(entity.Id);

                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    throw;
                }

            }
        }
        
        
        public List<DateTime> CreateMonthlySchedule(DayOfWeek lessonDay, TimeSpan lessonTime, DateTime startDate)
        {
            // Bir ay süresince haftalık ders zamanlarını tutacak liste
            List<DateTime> schedule = new List<DateTime>();

            // Bir sonraki dersin tarihini hesaplamak için başlangıç günü
            DateTime currentDate = startDate;

            // Eğer başlangıç tarihi belirtilen haftanın gününden önceyse, o günü bulana kadar ilerlet
            while (currentDate.DayOfWeek != lessonDay)
            {
                currentDate = currentDate.AddDays(1);
            }

            // İlk dersi oluştur ve saati ayarla
            DateTime firstLesson = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, lessonTime.Hours, lessonTime.Minutes, 0);

            // Dersin 4 hafta boyunca planlanmasını yap
            for (int i = 0; i < 4; i++)
            {
                schedule.Add(firstLesson);
                firstLesson = firstLesson.AddDays(7); // Bir sonraki haftaya geç
            }

            return schedule;
        }
    }
}
