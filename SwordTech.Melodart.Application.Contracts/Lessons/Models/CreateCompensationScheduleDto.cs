
 
namespace SwordTech.Melodart.Application.Contract.Lessons.Models
{
    public class CreateCompensationScheduleDto
    {
        public Guid ScheduleId { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
    }

}
