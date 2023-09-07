namespace RevvyTasks.Abstractions;

public interface ICertificateChainCreator
{
    ICertificateChain CreateChain(IClerkCollection clerkCollection);
}