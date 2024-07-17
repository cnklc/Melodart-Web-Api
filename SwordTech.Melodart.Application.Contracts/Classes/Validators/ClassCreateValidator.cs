using FluentValidation;
using SwordTech.Melodart.Application.Contract.Classes.Models;

namespace SwordTech.Melodart.Application.Contract.Classes.Validators;

public class ClassCreateValidator : AbstractValidator<ClassCreateDto>
{
    public ClassCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("İsim alanı zorunludur.");
    }
}
