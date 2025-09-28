using FluentValidation;

namespace Application.UseCases.Plans.Commands.Create;

public class CreatePlanCommandValidator : AbstractValidator<CreatePlanCommand>
{
    public CreatePlanCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(50).WithMessage("El nombre no debe exceder los 50 caracteres.");

        RuleFor(x => x.Description)
            .MaximumLength(150).WithMessage("La descripción no debe exceder los 150 caracteres.");
    }
}