using System.ComponentModel.DataAnnotations;

namespace SwordTech.Melodart.Domain.Contracts.Lessons
{
    public enum ScheduleStatusType
    {
        [Display(Name = "Bekleniyor")]
        Pending = 0,

        [Display(Name = "Tamamlandı")]
        Completed = 1,

        [Display(Name = "İptal")]
        Cancelled = 2,
        
        [Display(Name = "Ertelendi")]
        Deferred = 3,
    }
}
