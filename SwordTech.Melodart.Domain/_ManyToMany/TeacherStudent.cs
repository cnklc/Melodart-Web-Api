using SwordTech.Melodart.Domain.Departments;
using SwordTech.Melodart.Domain.Students;
using SwordTech.Melodart.Domain.Teachers;
using SwordTech.Melodart.Helper.Entity;

namespace SwordTech.Melodart.Domain._ManyToMany
{
    public class TeacherStudent : Entity
    {
        public TeacherStudent(Teacher teacher, Student student)
        {
            Teacher = teacher;
            TeacherId = teacher.Id;

            Student = student;
            StudentId = student.Id;
        }

        public TeacherStudent()
        {
        }

        public Guid TeacherId { get; private set; }
        public Teacher Teacher { get; private set; }

        public Guid StudentId { get; private set; }
        public Student Student { get; private set; }
    }
}
