using FluentValidation;
using SwordTech.Melodart.Application.Contract.Departments.Models;

namespace SwordTech.Melodart.Application.Contract.Departments.Validators;

public class DepartmentCreateValidator : AbstractValidator<DepartmentCreatDto>
{
    public DepartmentCreateValidator()
    {
        // RuleFor(x => x.Name)
        //     .NotEmpty().WithMessage("İsim alanı zorunludur.");
    }
}
