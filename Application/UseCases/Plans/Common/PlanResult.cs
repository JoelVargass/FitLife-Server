namespace Application.UseCases.Plans.Common;

public record PlanResult(
    Guid Id,
    string Name,
    string? Description
    );