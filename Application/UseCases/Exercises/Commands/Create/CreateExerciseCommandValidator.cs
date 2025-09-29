using FluentValidation;

namespace Application.UseCases.Exercises.Commands.Create;

public class CreateExerciseCommandValidator : AbstractValidator<CreateExerciseCommand>
{
    public CreateExerciseCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(50).WithMessage("El nombre no debe exceder los 50 caracteres.");

        RuleFor(x => x.Description)
            .MaximumLength(150).WithMessage("La descripción no debe exceder los 150 caracteres.");

        RuleFor(x => x.MuscleType)
            .IsInEnum().WithMessage("Debe especificar un tipo de músculo válido.");

        RuleFor(x => x.Duration)
            .MaximumLength(50).WithMessage("Debe proporcionar un valor válido.");
        
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("El usuario es obligatorio.");
    }
}