using FluentValidation;
using SwordTech.Melodart.Application.Contract.Teachers.Models;

namespace SwordTech.Melodart.Application.Contract.Teachers.Validators;

public class TeacherCreateValidator : AbstractValidator<TeacherCreateDto>
{
    public TeacherCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad alanı zorunludur.");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyad alanı zorunludur.");
    }
}
