using System.Threading.Tasks;

namespace Manufacturing.Domain.Multitenancy
{
    public interface ICompanyInformationStore<T> where T : Client
    {
        Task<T> GetClientAsync(string domainName, string ipAddress, string name);
    }
}
