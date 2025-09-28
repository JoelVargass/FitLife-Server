namespace Domain.Entities;

public class Plan
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public required string Name { get; set; }
    public string? Description { get; set; }
    
    public required TypeOfTraining TypeOfTraining { get; set; }
    
    public required PhysicalCondition PhysicalCondition { get; set; }
    
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdateDate { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeleteDate { get; set; }
}

public enum TypeOfTraining
{
    Cardio,
    Strength,
    Functional,
    Mixed,
    Other
}

public enum PhysicalCondition
{
    Beginner,
    Intermediate,
    Advance
}