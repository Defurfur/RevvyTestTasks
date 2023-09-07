using RevvyTasks.Abstractions;

namespace RevvyTasks.Models;

public class CertificateChain : ICertificateChain
{
    public CertificateChain(List<int> chain)
    {
        Chain = chain;
    }
    public List<int> Chain { get; private set; }
}
