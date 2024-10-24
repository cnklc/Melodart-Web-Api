using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SwordTech.Melodart.Application.Base;
using SwordTech.Melodart.Application.Contract.Lessons.Models;
using SwordTech.Melodart.Application.Contract.Students;
using SwordTech.Melodart.Application.Contract.Students.Models;
using SwordTech.Melodart.Domain._ManyToMany;
using SwordTech.Melodart.Domain.Contracts.Lessons;
using SwordTech.Melodart.Domain.Contracts.Student;
using SwordTech.Melodart.Domain.Departments;
using SwordTech.Melodart.Domain.Lessons;
using SwordTech.Melodart.Domain.Students;
using SwordTech.Melodart.Domain.Teachers;
using SwordTech.Melodart.EFCore.Repositories;
using SwordTech.Melodart.EFCore.Repositories.Base;

namespace SwordTech.Melodart.Application.Students;

public class StudentAppService : AppService<Student, StudentDto, StudentDto, StudentCreateDto, StudentUpdateDto>, IStudentAppService
{
    private IEfBaseRepository<Department> _departmentRepository;
    private IEfBaseRepository<Teacher> _teacherRepository;
    private IEfBaseRepository<Lesson> _lessonRepository;
    private IEfBaseRepository<Schedule> _scheduleRepository;
    private IEfBaseRepository<Parent> _parentRepository;

    public StudentAppService(IEfBaseRepository<Student> repository, IMapper mapper, IEfBaseRepository<Teacher> teacherRepository, IEfBaseRepository<Department> departmentRepository, IEfBaseRepository<Schedule> scheduleRepository,
        IEfBaseRepository<Lesson> lessonRepository, IEfBaseRepository<Parent> parentRepository) : base(repository, mapper)
    {
        _teacherRepository = teacherRepository;
        _departmentRepository = departmentRepository;
        _scheduleRepository = scheduleRepository;
        _lessonRepository = lessonRepository;
        _parentRepository = parentRepository;
    }

    public override async Task<StudentDto> Create(StudentCreateDto input)
    {
        using (var transaction = _repository.BeginTransaction())
        {
            try
            {
                // var entity = _mapper.Map<Student>(input);
                var entity = new Student(input.Name, input.LastName, input.PhoneNumber, input.Birthday, input.Address, input.Description, input.Gender);

                if (!string.IsNullOrEmpty(input.MothersName) && !string.IsNullOrEmpty(input.MothersPhoneNumber))
                {
                    var nameSplit = input.MothersName.Split(" ");

                    string lastName = nameSplit[nameSplit.Length - 1];
                    string firstName = string.Join(" ", nameSplit.Take(nameSplit.Length - 1));

                    entity.Parents.Add(new Parent(ParentType.Mother, firstName, lastName, input.MothersPhoneNumber, null, null));
                }

                if (!string.IsNullOrEmpty(input.FathersName) && !string.IsNullOrEmpty(input.FathersPhoneNumber))
                {
                    var nameSplit = input.FathersName.Split(" ");

                    string lastName = nameSplit[nameSplit.Length - 1];
                    string firstName = string.Join(" ", nameSplit.Take(nameSplit.Length - 1));

                    entity.Parents.Add(new Parent(ParentType.Father, firstName, lastName, input.FathersPhoneNumber, null, null));
                }

                _repository.Add(entity);

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

    public override async Task<StudentDto> Update(Guid id, StudentUpdateDto input)
    {
        var entity = _repository.GetById(id);
        _mapper.Map(input, entity);
     

        if (!string.IsNullOrEmpty(input.MothersName) && !string.IsNullOrEmpty(input.MothersPhoneNumber))
        {
            var nameSplit = input.MothersName.Split(" ");

            string lastName = nameSplit[nameSplit.Length - 1];
            string firstName = string.Join(" ", nameSplit.Take(nameSplit.Length - 1));

            Parent parent = _parentRepository.GetAll().FirstOrDefault(x => x.StudentId == id && x.ParentType == ParentType.Mother);

            if (parent != null)
            {
                parent.Name = firstName;
                parent.LastName = lastName;
                parent.PhoneNumber = input.MothersPhoneNumber;
                _parentRepository.Update(parent);
            }
            else
            {
                entity.Parents.Add(new Parent(ParentType.Mother, firstName, lastName, input.MothersPhoneNumber, null, null));
            }
        }

        if (!string.IsNullOrEmpty(input.FathersName) && !string.IsNullOrEmpty(input.FathersPhoneNumber))
        {
            var nameSplit = input.FathersName.Split(" ");

            string lastName = nameSplit[nameSplit.Length - 1];
            string firstName = string.Join(" ", nameSplit.Take(nameSplit.Length - 1));

            Parent parent = _parentRepository.GetAll().FirstOrDefault(x => x.StudentId == id && x.ParentType == ParentType.Father);

            if (parent != null)
            {
                parent.Name = firstName;
                parent.LastName = lastName;
                parent.PhoneNumber = input.MothersPhoneNumber;
                _parentRepository.Update(parent);
            }
            else
            {
                entity.Parents.Add(new Parent(ParentType.Father, firstName, lastName, input.FathersPhoneNumber, null, null));
            }
        }
        _repository.Update(entity);
        return await GetById(entity.Id);
    }

    public override async Task Delete(Guid id)
    {

        using (var transaction = _repository.BeginTransaction())
        {
            try
            {
                var student = _repository.GetById(id);

                if (student != null)
                {
                    _repository.Delete(student);

                    var lessons = _lessonRepository.GetAll().Where(x => x.StudentId == student.Id).ToList();

                    foreach (var lesson in lessons)
                    {
                        _lessonRepository.Delete(lesson);

                        var schedules = _scheduleRepository.GetAll().Where(x => x.LessonId == lesson.Id && x.ScheduleStatusType == ScheduleStatusType.Pending).ToList();

                        foreach (var schedule in schedules)
                        {
                            _scheduleRepository.Delete(schedule);
                        }
                    }
                }

                await transaction.CommitAsync();
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
