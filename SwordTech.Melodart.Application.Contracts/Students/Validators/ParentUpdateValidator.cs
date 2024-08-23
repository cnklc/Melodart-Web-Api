using FluentValidation;
using SwordTech.Melodart.Application.Contract.Students.Models;

namespace SwordTech.Melodart.Application.Contract.Students.Validators;

public class ParentUpdateValidator : AbstractValidator<ParentUpdateDto>
{
    public ParentUpdateValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
    }
}
