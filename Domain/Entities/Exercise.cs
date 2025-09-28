namespace Domain.Entities;

public class Exercise
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    
    public required string Name { get; set; }
    
    public MuscleType MuscleType { get; set; } = MuscleType.Other;
    
    public string? Description { get; set; }
    
    public string? Duration { get; set; }
    
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdateDate { get; set; }
    
    public bool IsDeleted { get; set; }
    public DateTime? DeleteDate { get; set; }
}

public enum MuscleType
{
Chest,
Back,
Legs,
Shoulders,
Biceps,
Triceps,
Abs,
FullBody,
Other
}
