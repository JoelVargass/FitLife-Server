using Domain.Entities;

namespace Application.UseCases.Plans.Common;

public static class PlanExtensions
{
    public static PlanResult? ToResult(this Plan plan)
    {
        return new PlanResult(
            plan.Id,
            plan.Name,
            plan.Description,
            plan.TypeOfTraining,
            plan.PhysicalCondition);
    }
}