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


    public StudentAppService(IEfBaseRepository<Student> repository, IMapper mapper, IEfBaseRepository<Teacher> teacherRepository, IEfBaseRepository<Department> departmentRepository, IEfBaseRepository<Schedule> scheduleRepository) : base(repository, mapper)
    {
        _teacherRepository = teacherRepository;
        _departmentRepository = departmentRepository;
        _scheduleRepository = scheduleRepository;
    }

    public override async Task<StudentDto> Create(StudentCreateDto input)
    {
        using (var transaction = _repository.BeginTransaction())
        {
            try
            {
                // var entity = _mapper.Map<Student>(input);
                var entity = new Student(input.Name,input.LastName,input.PhoneNumber,input.Birthday,input.Address,input.Description,input.Gender);

                if (!string.IsNullOrEmpty(input.MothersName) && !string.IsNullOrEmpty(input.MothersPhoneNumber))
                {
                   var nameSplit =  input.MothersName.Split(" ");  
                   
                   string lastName = nameSplit[nameSplit.Length - 1];
                   string firstName = string.Join(" ", nameSplit.Take(nameSplit.Length - 1));
                   
                    entity.Parents.Add(new Parent(ParentType.Mother,firstName,lastName,input.MothersPhoneNumber,null,null));
                }
                
                if (!string.IsNullOrEmpty(input.FathersName) && !string.IsNullOrEmpty(input.FathersPhoneNumber))
                {
                    var nameSplit =  input.FathersName.Split(" ");  
                   
                    string lastName = nameSplit[nameSplit.Length - 1];
                    string firstName = string.Join(" ", nameSplit.Take(nameSplit.Length - 1));
                   
                    entity.Parents.Add(new Parent(ParentType.Father,firstName,lastName,input.FathersPhoneNumber,null,null));
                }

                _repository.Add(entity);
                
                // if (input.Lessons.Any())
                // {
                //     foreach (var lesson in entity.Lessons)
                //     {
                //         var times = CreateMonthlySchedule((DayOfWeek)lesson.DayOfTheWeek, lesson.TimeOfDay, DateTime.Now);
                //
                //         List<Schedule> schedules = new List<Schedule>();
                //
                //         times.ForEach(item =>
                //         {
                //             _scheduleRepository.Add(new Schedule()
                //             {
                //                 ScheduleStatusType = ScheduleStatusType.Pending,
                //                 ScheduleTime = item.Date,
                //                 DayOfTheWeek = lesson.DayOfTheWeek,
                //                 TimeOfDay = lesson.TimeOfDay,
                //                 Duration = lesson.Duration,
                //                 TeacherId = lesson.TeacherId,
                //                 DepartmentId = lesson.DepartmentId,
                //                 LessonId = lesson.Id,
                //                 StudentId = entity.Id
                //             });
                //         });
                //     }
                //
                //     // input.Lessons.ForEach(lesson =>
                //     // {
                //     //     var times = CreateMonthlySchedule((DayOfWeek)lesson.DayOfTheWeek, TimeSpan.Parse(lesson.TimeOfDay), DateTime.Now);
                //     //     
                //     //     List<Schedule> schedules = new List<Schedule>();
                //     //     
                //     //     times.ForEach(item =>
                //     //     {
                //     //         schedules.Add(new Schedule()
                //     //         {
                //     //             ScheduleStatusType = ScheduleStatusType.Pending,
                //     //             ScheduleTime = item.Date,
                //     //             DayOfTheWeek = lesson.DayOfTheWeek,
                //     //             TimeOfDay = TimeSpan.Parse(lesson.TimeOfDay),
                //     //             Duration = lesson.Duration,
                //     //             TeacherId = lesson.TeacherId,
                //     //             DepartmentId = lesson.DepartmentId,
                //     //             LessonId = lesson.
                //     //         });
                //     //     });
                //     //     
                //     //    
                //     // });
                // }
                
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

    // if (input.Departments.Any())
    // {
    //     input.Departments.ForEach(x =>
    //     {
    //         var department = _departmentRepository.GetById(x);
    //         StudentDepartment studentDepartment = new StudentDepartment(entity, department);
    //
    //         entity.AddStudentDepartment(studentDepartment);
    //         // _repository.Update(entity);
    //     });
    // }

    // if (input.Teachers.Any())
    // {
    //     input.Teachers.ForEach(x =>
    //     {
    //         var teacher = _teacherRepository.GetById(x);
    //         TeacherStudent teacherStudent = new TeacherStudent(teacher, entity);
    //
    //         entity.AddTeacherStudents(teacherStudent);
    //         // _repository.Update(entity);
    //     });
    // }

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
