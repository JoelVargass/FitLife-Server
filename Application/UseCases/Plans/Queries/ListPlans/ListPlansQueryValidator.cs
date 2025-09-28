using Application.UseCases.Exercises.Queries.ListExercises;
using FluentValidation;

namespace Application.UseCases.Plans.Queries.ListPlans;

public class ListPlansQueryValidator : AbstractValidator<ListPlansQuery>
{
    public ListPlansQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("La página debe ser mayor o igual a 1.");
    }
}