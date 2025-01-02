using SwordTech.Melodart.Domain.Contracts.Lessons;

namespace SwordTech.Melodart.Application.Contract.Lessons.Models
{
    public class ScheduleUpdateDto
    {
        public DateTime? ScheduleTime { get; set; }
        public string? Description { get; set; } 
        public ScheduleStatusType ScheduleStatusType { get; set; }
    }
}
