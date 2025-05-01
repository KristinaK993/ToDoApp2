using FluentValidation;

namespace ToDoApp.Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Titel är obligatorisk")
            .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserId måste vara större än 0");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId måste vara större än 0");
        }
    }
}
