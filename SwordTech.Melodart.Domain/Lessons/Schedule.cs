using SwordTech.Melodart.Domain.Contracts.Lessons;
using SwordTech.Melodart.Domain.Departments;
using SwordTech.Melodart.Domain.Students;
using SwordTech.Melodart.Domain.Teachers;
using SwordTech.Melodart.Helper.Entity;

namespace SwordTech.Melodart.Domain.Lessons
{
    public class Schedule : Entity
    {
        public DateTime ScheduleTime { get; set; }
        public ScheduleStatusType ScheduleStatusType { get; set; }
        
     
        public int DayOfTheWeek { get; set; }
        public TimeSpan TimeOfDay { get; set; }
        public int Duration { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; }  

        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; } 

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } 

        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
