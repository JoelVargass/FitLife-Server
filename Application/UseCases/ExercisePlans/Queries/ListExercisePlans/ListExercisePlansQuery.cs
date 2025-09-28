using Application.Common.Results;
using Application.UseCases.ExercisePlans.Common;
using ErrorOr;
using MediatR;

namespace Application.UseCases.ExercisePlans.Queries.ListExercisePlans;

public record ListExercisePlansQuery(
    int Page = 1, 
    int PageSize = 10, 
    string? Name = null
) : IRequest<ErrorOr<ListResult<ExercisePlanResult>>>;