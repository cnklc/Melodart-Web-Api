using AutoMapper;
using SwordTech.Melodart.Application.Base;
using SwordTech.Melodart.Application.Contract.Students;
using SwordTech.Melodart.Application.Contract.Students.Models;
using SwordTech.Melodart.Domain.Students;
using SwordTech.Melodart.EFCore.Repositories;
using SwordTech.Melodart.EFCore.Repositories.Base;

namespace SwordTech.Melodart.Application.Students;

public class StudentAppService : AppService<Student, StudentDto, StudentDto, StudentCreateDto, StudentUpdateDto>, IStudentAppService
{
    public StudentAppService(IEfBaseRepository<Student> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    public override async Task<StudentDto> Create(StudentCreateDto input)
    {
        var student = new Student(input.Name, input.LastName, input.PhoneNumber, input.Email, input.Birthday, input.Address, input.Description);

        if (input.Parents.Any())
        {
            foreach (var studentParent in input.Parents)
            {
                var parents = new Parent(studentParent.Name, studentParent.LastName, studentParent.PhoneNumber, studentParent.Email, studentParent.Description);

                student.AddParent(parents);
            }
         }

        _repository.Add(student);

        return await GetById(student.Id);
    }
}
 