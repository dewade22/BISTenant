using System.Threading.Tasks;

namespace Manufacturing.Domain.Multitenancy
{
    public interface ICompanyInformationResolutionStrategy
    {
        Task<(string domainName, string ipAddresss, string name)> GetCompanyInformationIdentifierAsync();
    }
}
