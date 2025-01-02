using FluentValidation;
using SwordTech.Melodart.Application.Contract.Lessons.Models;
using SwordTech.Melodart.Application.Contract.Students.Models;

namespace SwordTech.Melodart.Application.Contract.Lessons.Validators;

public class LessonUpdateValidator : AbstractValidator<LessonUpdateDto>
{
    public LessonUpdateValidator()
    {
        // RuleFor(x => x.Name).NotEmpty();
        // RuleFor(x => x.LastName).NotEmpty();
        // RuleFor(x => x.Birthday).NotEmpty();
    }
}
