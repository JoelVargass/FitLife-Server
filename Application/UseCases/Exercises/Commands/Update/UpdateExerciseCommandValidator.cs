using FluentValidation;

namespace Application.UseCases.Exercises.Commands.Update;

public class UpdateExerciseCommandValidator : AbstractValidator<UpdateExerciseCommand>
{
    public UpdateExerciseCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("El ID del ejercicio es obligatorio.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del ejercicio es obligatorio.")
            .MaximumLength(50).WithMessage("El nombre no debe exceder los 50 caracteres.");

        RuleFor(x => x.MuscleType)
            .MaximumLength(50).WithMessage("El tipo de músculo no debe exceder los 50 caracteres.");

        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("La descripción no debe exceder los 200 caracteres.");

        RuleFor(x => x.Duration)
            .Matches(@"^\d{1,2}:\d{2}(:\d{2})?$")
            .When(x => !string.IsNullOrEmpty(x.Duration))
            .WithMessage("La duración debe tener el formato HH:mm o HH:mm:ss.");
    }
}