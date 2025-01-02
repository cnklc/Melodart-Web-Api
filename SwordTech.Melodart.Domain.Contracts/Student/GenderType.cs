using System.ComponentModel.DataAnnotations;

namespace SwordTech.Melodart.Domain.Contracts.Student
{
    public enum GenderType
    {
        [Display(Name = "Erkek")]
        Male = 1,
        
        [Display(Name = "Kadin")]
        Female = 2,
    }
}
