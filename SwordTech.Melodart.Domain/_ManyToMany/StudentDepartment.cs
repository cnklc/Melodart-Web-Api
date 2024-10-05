using SwordTech.Melodart.Domain.Departments;
using SwordTech.Melodart.Domain.Students;
using SwordTech.Melodart.Helper.Entity;

namespace SwordTech.Melodart.Domain._ManyToMany
{
    public class StudentDepartment : Entity
    {
        public StudentDepartment(Student student, Department department)
        {
            Student = student;
            StudentId = student.Id;

            Department = department;
            DepartmentId = department.Id;
        }

        public StudentDepartment()
        {
        }

        public Guid StudentId { get; private set; }
        public Student Student { get; private set; }

        public Guid DepartmentId { get; private set; }
        public Department Department { get; private set; }
    }
}
