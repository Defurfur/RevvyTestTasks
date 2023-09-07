namespace RevvyTasks.Dtos;

public class ClerkDto
{
    public static ClerkDto Create(int id, params int[] args)
    {
        return new ClerkDto() {Id = id, ClerkCertificatesRequired = new List<int>(args) };
    }
    public int Id { get; set; }
    public List<int>? ClerkCertificatesRequired { get; set; }
}
