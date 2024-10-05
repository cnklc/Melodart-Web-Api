using AutoMapper;
using SwordTech.Melodart.Application.Base;
using SwordTech.Melodart.Application.Contract.Teachers;
using SwordTech.Melodart.Application.Contract.Teachers.Models;
using SwordTech.Melodart.Domain._ManyToMany;
using SwordTech.Melodart.Domain.Departments;
using SwordTech.Melodart.Domain.Teachers;
using SwordTech.Melodart.EFCore.Repositories.Base;
using System.Net;

namespace SwordTech.Melodart.Application.Teachers
{
    public class TeacherAppService : AppService<Teacher, TeacherDto, TeacherDto, TeacherCreateDto, TeacherUpdateDto>, ITeacherAppService
    {

        private IEfBaseRepository<Department> _departmentepository;


        public TeacherAppService(IEfBaseRepository<Teacher> repository, IMapper mapper, IEfBaseRepository<Department> departmentepository) : base(repository, mapper)
        {
            _departmentepository = departmentepository;

        }

        public async override Task<TeacherDto> Create(TeacherCreateDto input)
        {
            var entity = _mapper.Map<Teacher>(input);

            _repository.Add(entity);

            if (input.Departments.Any())
            {
                input.Departments.ForEach(x =>
                {
                    var department = _departmentepository.GetById(x);
                    TeacherDepartment teacherDepartment = new TeacherDepartment(entity, department);

                    entity.AddTeacherDepartments(teacherDepartment);
                    _repository.Update(entity);
                });
            }

            return await GetById(entity.Id);
        }

        public async override Task<TeacherDto> Update(Guid id, TeacherUpdateDto input)
        {
            var entity = _repository.GetById(id);

            _mapper.Map(input, entity);

            if (input.Departments.Any())
            {
                input.Departments.ForEach(x =>
                {
                    var department = _departmentepository.GetById(x);
                    TeacherDepartment teacherDepartment = new TeacherDepartment(entity, department);

                    if (!entity.TeacherDepartments.Any() || !entity.TeacherDepartments.Any(x => x.DepartmentId == department.Id))
                    {
                        entity.AddTeacherDepartments(teacherDepartment);
                    }

                });
            }
            else
            {
                if (entity.TeacherDepartments.Any())
                {
                    foreach (var teacherDepartment in entity.TeacherDepartments.ToList())
                    {
                        entity.TeacherDepartments.Remove(teacherDepartment);
                    }
                }
            }

            _repository.Update(entity);

            return await GetById(entity.Id);
        }
    }
}
