using RevvyTasks.Dtos;
using RevvyTasks.Models;

namespace RevvyTasks.Models;

public class ClerkCollectionDto
{
    public static ClerkCollectionDto Create(params ClerkDto[] args)
    {
        return new ClerkCollectionDto() { Clerks = args.ToList() };
    }
    public List<ClerkDto> Clerks { get; set; } = new List<ClerkDto>();

}
