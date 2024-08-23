using FluentValidation;
using SwordTech.Melodart.Application.Contract.Students.Models;

namespace SwordTech.Melodart.Application.Contract.Students.Validators;

public class StudentCreateValidator : AbstractValidator<StudentCreateDto>
{
    public StudentCreateValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Birthday).NotEmpty();
    }
}
