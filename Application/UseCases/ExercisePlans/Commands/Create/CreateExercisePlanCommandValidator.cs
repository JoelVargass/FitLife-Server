using FluentValidation;

namespace Application.UseCases.ExercisePlans.Commands.Create;

public class CreateExercisePlanCommandValidator : AbstractValidator<CreateExercisePlanCommand>
{
    public CreateExercisePlanCommandValidator()
    {
        RuleFor(x => x.PlanId)
            .NotEmpty().WithMessage("El PlanId es obligatorio.");

        RuleFor(x => x.ExerciseId)
            .NotEmpty().WithMessage("El ExerciseId es obligatorio.");

        RuleFor(x => x.DayOfWeek)
            .IsInEnum().WithMessage("Debe especificar un día de la semana válido.");

        RuleFor(x => x.Series)
            .GreaterThan(0).WithMessage("El número de series debe ser mayor a 0.");

        RuleFor(x => x.Repetitions)
            .GreaterThan(0).WithMessage("El número de repeticiones debe ser mayor a 0.");
    }
}