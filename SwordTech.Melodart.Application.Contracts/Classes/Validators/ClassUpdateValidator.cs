using FluentValidation;
using SwordTech.Melodart.Application.Contract.Classes.Models;

namespace SwordTech.Melodart.Application.Contract.Classes.Validators;

public class ClassUpdateValidator : AbstractValidator<ClassUpdateDto>
{
    public ClassUpdateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("İsim alanı zorunludur.");
    }
}
