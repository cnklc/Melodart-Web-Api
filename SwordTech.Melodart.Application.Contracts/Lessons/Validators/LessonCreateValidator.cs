using FluentValidation;
using SwordTech.Melodart.Domain.Lessons;

namespace SwordTech.Melodart.Application.Contract.Lessons.Validators;

public class LessonCreateValidator : AbstractValidator<Lesson>
{
    public LessonCreateValidator()
    {
        // RuleFor(x => x.Name).NotEmpty();
        // RuleFor(x => x.LastName).NotEmpty();
        // RuleFor(x => x.Birthday).NotEmpty();
    }
}
