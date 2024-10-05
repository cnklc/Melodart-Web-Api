using AutoMapper;
using SwordTech.Melodart.Application.Base;
using SwordTech.Melodart.Application.Contract.Students;
using SwordTech.Melodart.Application.Contract.Students.Models;
using SwordTech.Melodart.Domain._ManyToMany;
using SwordTech.Melodart.Domain.Departments;
using SwordTech.Melodart.Domain.Students;
using SwordTech.Melodart.Domain.Teachers;
using SwordTech.Melodart.EFCore.Repositories;
using SwordTech.Melodart.EFCore.Repositories.Base;

namespace SwordTech.Melodart.Application.Students;

public class StudentAppService : AppService<Student, StudentDto, StudentDto, StudentCreateDto, StudentUpdateDto>, IStudentAppService
{
    private IEfBaseRepository<Department> _departmentRepository;
    private IEfBaseRepository<Teacher> _teacherRepository;


    public StudentAppService(IEfBaseRepository<Student> repository, IMapper mapper, IEfBaseRepository<Teacher> teacherRepository, IEfBaseRepository<Department> departmentRepository) : base(repository, mapper)
    {
        _teacherRepository = teacherRepository;
        _departmentRepository = departmentRepository;
    }

    public override async Task<StudentDto> Create(StudentCreateDto input)
    {
        var entity = _mapper.Map<Student>(input);

        _repository.Add(entity);

        if (input.Departments.Any())
        {
            input.Departments.ForEach(x =>
            {
                var department = _departmentRepository.GetById(x);
                StudentDepartment studentDepartment = new StudentDepartment(entity, department);

                entity.AddStudentDepartment(studentDepartment);
                // _repository.Update(entity);
            });
        }

        if (input.Teachers.Any())
        {
            input.Teachers.ForEach(x =>
            {
                var teacher = _teacherRepository.GetById(x);
                TeacherStudent teacherStudent = new TeacherStudent(teacher, entity);

                entity.AddTeacherStudents(teacherStudent);
                // _repository.Update(entity);
            });
        }
        
        _repository.Update(entity);
        return await GetById(entity.Id);
    }
}
