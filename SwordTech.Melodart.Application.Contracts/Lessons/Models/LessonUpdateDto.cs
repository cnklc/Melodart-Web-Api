namespace SwordTech.Melodart.Application.Contract.Lessons.Models;

public class LessonUpdateDto
{
    public decimal Amount { get; set; }
    public int DayOfTheWeek { get; set; }
    public TimeSpan TimeOfDay { get; set; }
    public int Duration { get; set; }
        
    public Guid StudentId { get; set; }
    public Guid TeacherId { get; set; }
    public Guid DepartmentId { get; set; } 
}
