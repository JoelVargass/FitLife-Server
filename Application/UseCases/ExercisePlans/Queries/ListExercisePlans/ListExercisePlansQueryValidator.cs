using Application.UseCases.Exercises.Queries.ListExercises;
using FluentValidation;

namespace Application.UseCases.ExercisePlans.Queries.ListExercisePlans;

public class ListExercisePlansQueryValidator : AbstractValidator<ListExercisePlansQuery>
{
    public ListExercisePlansQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("La página debe ser mayor o igual a 1.");
    }
}