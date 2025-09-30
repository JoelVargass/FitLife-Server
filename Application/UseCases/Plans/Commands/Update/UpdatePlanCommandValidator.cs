using FluentValidation;

namespace Application.UseCases.Plans.Commands.Update;

public class UpdatePlanCommandValidator : AbstractValidator<UpdatePlanCommand>
{
    public UpdatePlanCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("El ID del ejercicio es obligatorio.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del ejercicio es obligatorio.")
            .MaximumLength(50).WithMessage("El nombre no debe exceder los 50 caracteres.");

        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("La descripción no debe exceder los 200 caracteres.");

    }
}