using SwordTech.Melodart.Domain.Departments;
using SwordTech.Melodart.Domain.Students;
using SwordTech.Melodart.Domain.Teachers;
using SwordTech.Melodart.Helper.Entity;
using System.Collections.ObjectModel;

namespace SwordTech.Melodart.Domain.Lessons
{
    public class Lesson : Entity
    {
        public Lesson(Student student, Teacher teacher, Department department, int dayOfTheWeek, TimeSpan timeOfDay, int duration, decimal amount)
        {
            StudentId = student.Id;
            Student = student;

            TeacherId = teacher.Id;
            Teacher = teacher;

            DepartmentId = department.Id;
            Department = department;

            DayOfTheWeek = dayOfTheWeek;
            TimeOfDay = timeOfDay;
            Duration = duration;
            Amount = amount;
        }
        public Lesson()
        {

        }

        public decimal Amount { get; set; }
        public int DayOfTheWeek { get; set; }
        public TimeSpan TimeOfDay { get; set; }
        public int Duration { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }

        public Collection<Schedule> Schedules { get; set; } = new Collection<Schedule>();
    }
}
