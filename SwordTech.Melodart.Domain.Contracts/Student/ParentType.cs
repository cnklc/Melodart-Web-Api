using System.ComponentModel.DataAnnotations;

namespace SwordTech.Melodart.Domain.Contracts.Student
{
    public enum ParentType
    {
        [Display(Name = "Anne")]
        Mother = 1,
        
        [Display(Name = "Baba")]
        Father = 2,
    }

}
