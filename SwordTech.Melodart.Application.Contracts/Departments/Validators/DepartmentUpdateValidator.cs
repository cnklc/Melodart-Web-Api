using FluentValidation;
using SwordTech.Melodart.Application.Contract.Departments.Models;

namespace SwordTech.Melodart.Application.Contract.Departments.Validators;

public class DepartmentUpdateValidator : AbstractValidator<DepartmentUpdateDto>
{
    public DepartmentUpdateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("İsim alanı zorunludur.");
    }
}
