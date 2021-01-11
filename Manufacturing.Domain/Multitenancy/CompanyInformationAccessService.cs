using System.Threading.Tasks;

namespace Manufacturing.Domain.Multitenancy
{
    public class CompanyInformationAccessService<T> where T : Client
    {
        private readonly ICompanyInformationResolutionStrategy _CompanyInformationResolutionStrategy;
        private readonly ICompanyInformationStore<T> _CompanyInformationStore;

        public CompanyInformationAccessService(ICompanyInformationResolutionStrategy CompanyInformationResolutionStrategy, ICompanyInformationStore<T> CompanyInformationStore)
        {
            _CompanyInformationResolutionStrategy = CompanyInformationResolutionStrategy;
            _CompanyInformationStore = CompanyInformationStore;
        }

        /// <summary>
        /// Get the current CompanyInformation
        /// </summary>
        /// <returns></returns>
        public async Task<T> GetClient()
        {
            var (domainName, ipAddress, name) = await _CompanyInformationResolutionStrategy.GetCompanyInformationIdentifierAsync();
            return await _CompanyInformationStore.GetClientAsync(domainName, ipAddress, name);
        }
    }
}
