using RevvyTasks.Abstractions;

namespace RevvyTasks.Models;

public class Clerk : IClerk
{
    public Clerk(int id, List<int>? clerkCertificatesRequired)
    {
        Id = id;
        ClerkCertificatesRequired = clerkCertificatesRequired;

    }
    public int Id { get; set; }
    public List<int>? ClerkCertificatesRequired { get; set; }
}
