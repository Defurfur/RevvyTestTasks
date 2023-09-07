using RevvyTasks.Abstractions;
using System.Collections;

namespace RevvyTasks.Models;

public class ClerkCollection : IClerkCollection
{
    public ClerkCollection(List<IClerk> clerks)
    {
        Clerks = clerks;
    }
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public List<IClerk> Clerks { get; set; } = new List<IClerk>();
    
 }
