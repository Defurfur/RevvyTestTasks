using RevvyTasks.Models;

namespace RevvyTasks.Abstractions;

public interface IClerk
{
    public int Id { get; set; }
    public List<int>? ClerkCertificatesRequired{ get; set; }
}
