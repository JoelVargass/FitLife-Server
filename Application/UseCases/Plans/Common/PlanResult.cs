using Domain.Entities;

namespace Application.UseCases.Plans.Common;

public record PlanResult(
    Guid Id,
    string Name,
    string? Description,
    TypeOfTraining  TypeOfTraining,
    PhysicalCondition  PhysicalCondition
    );