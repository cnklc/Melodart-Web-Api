using SwordTech.Melodart.Domain.Departments;
using SwordTech.Melodart.Domain.Teachers;
using SwordTech.Melodart.Helper.Entity; 

namespace SwordTech.Melodart.Domain._ManyToMany
{
    public class TeacherDepartment : Entity
    {
        public TeacherDepartment(Teacher teacher, Department department)
        {
            Teacher = teacher;
            TeacherId = teacher.Id;

            Department = department;
            DepartmentId = department.Id;
        }

        public TeacherDepartment()
        {
        }

        public Guid TeacherId { get; private set; }
        public Teacher Teacher { get; private set; }

        public Guid DepartmentId { get; private set; }
        public Department Department { get; private set; }
    }

}
