namespace Domain.Entities;

public class Plan
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    
    public required string Name { get; set; }
    
    public required string? Description { get; set; }
    
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdateDate { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public DateTime? DeleteDate { get; set; }
    
}