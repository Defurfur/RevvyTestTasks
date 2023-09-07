using RevvyTasks.Models;
using RevvyTasks.Abstractions;
using RevvyTasks.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace RevvyTasks.Services;

public class CertificateChainCreator : ICertificateChainCreator
{
    private readonly ILogger<CertificateChainCreator> _logger;
    private readonly Stopwatch _stopwatch = new();
    public CertificateChainCreator(ILogger<CertificateChainCreator> logger)
    {
        _logger = logger;
    }
    //I made this method asynchronous just for demonstration purposes
    public ICertificateChain CreateChain(IClerkCollection clerkCollection)
    {
        List<(int, int)> dependancies = clerkCollection.Clerks
            .SelectMany(
                x => x.ClerkCertificatesRequired?
                        .Select(
                            y => (x.Id, y)) ?? Enumerable.Empty<(int, int)>())
                                .ToList();

        var graphTable = new DirectedGraphTable(dependancies);

        var chain = graphTable.CreateChain();

        _stopwatch.Stop();

        _logger.LogInformation("{Service}'s method 'CreateChainAsync' " +
            "has been executed within {ElapsedTime} with the result: {Result}",
            GetType().Name,
            _stopwatch.Elapsed,
            chain.ToArray());


        return new CertificateChain(chain);

    }
}
