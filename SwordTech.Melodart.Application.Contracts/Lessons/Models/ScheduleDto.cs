using SwordTech.Melodart.Domain.Contracts.Lessons;

namespace SwordTech.Melodart.Application.Contract.Lessons.Models
{
    public class ScheduleDto
    {
        public Guid Id { get; set; }
        public DateTime ScheduleTime { get; set; }
        public ScheduleStatusType ScheduleStatusType { get; set; }
        
        public int DayOfTheWeek { get; set; }
        public TimeSpan TimeOfDay { get; set; }
        public int Duration { get; set; }

        public Guid StudentId { get; set; }
        public string StudentName { get; set; }

        public Guid TeacherId { get; set; }
        public string TeacherName { get; set; }

        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public Guid LessonId { get; set; }
    }
}
