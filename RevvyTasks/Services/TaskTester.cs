using RevvyTasks.Abstractions;
using RevvyTasks.Dtos;
using RevvyTasks.Models;
using System.Net.Http;

namespace RevvyTasks.Services;

public class TaskTester : ITaskTester
{
    List<ClerkCollectionDto> _clerksTestCases = new()
    {
        ClerkCollectionDto.Create(
            ClerkDto.Create(1, 2),
            ClerkDto.Create(2, 3, 4)
            ),
        ClerkCollectionDto.Create(
            ClerkDto.Create(1, 2),
            ClerkDto.Create(3, 4)
            ),
        ClerkCollectionDto.Create(
            ClerkDto.Create(1, 2),
            ClerkDto.Create(2, 3, 4),
            ClerkDto.Create(3, 5, 7)
            ),
        ClerkCollectionDto.Create(
            ClerkDto.Create(3, 2, 4),
            ClerkDto.Create(5, 3)
            ),
        ClerkCollectionDto.Create(
            ClerkDto.Create(5, 3),
            ClerkDto.Create(3, 2, 4)
            ),
        ClerkCollectionDto.Create(
            ClerkDto.Create(2, 3, 4),
            ClerkDto.Create(1, 2)
            ),
        ClerkCollectionDto.Create(
            ClerkDto.Create(2, 3, 4),
            ClerkDto.Create(1, 2),
            ClerkDto.Create(3, 5, 4)
            ),
        };

    List<FindSumRepresentationRequest> _secondTaskRequests = new()
        {
            new(new int[] {3, 1, 8, 5, 4}, 10),
            new(new int[] {3, 1, 8, 5, 4}, 2),
            new(new int[] {3, 1, 8, 5, 4, 16, 20, 100, 56}, 133),
            new(new int[] {3, 1, 8, 5, 4, 16, 20, 100, 56}, 176),
            new(new int[] {3, 1, 8, 5, 4, 16, 20, 100, 56}, 7),
            new(new int[] {3, 1, 8, 5, 4, 16, 20, 100, 56}, 1002),
        };
    public async Task SetupFirstTask(HttpClient client)
    {
        Console.WriteLine("First task tests: \r\n \r\n");

        foreach (var clerkCollectionDto in _clerksTestCases)
        {
            var response = await client.PostAsJsonAsync("https://localhost:7187/CertificateChain", clerkCollectionDto);

            var message = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Case: " + String.Join(",",
                clerkCollectionDto.Clerks
                .Select(x => "{" + x.Id + "[" + String.Join(",", x.ClerkCertificatesRequired!) + "]} ")) + " Result: " + message);
        }
    }

    public async Task SetupSecondTask(HttpClient client)
    {
        Console.WriteLine("\r\nSecond task tests: \r\n \r\n");


        foreach (var request in _secondTaskRequests)
        {
            var response = await client.PostAsJsonAsync("https://localhost:7187/SumRepresentation", request);

            var message = await response.Content.ReadAsStringAsync();

            Console.WriteLine(message);
        }

    }
}
