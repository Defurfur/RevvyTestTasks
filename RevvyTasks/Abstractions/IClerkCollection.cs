namespace RevvyTasks.Abstractions;

public interface IClerkCollection
{
    public Guid Id { get; set; } 
    public List<IClerk> Clerks { get; set; }
}
