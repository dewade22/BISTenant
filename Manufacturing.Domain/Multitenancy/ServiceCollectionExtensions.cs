using Microsoft.Extensions.DependencyInjection;

namespace Manufacturing.Domain.Multitenancy
{
    /// <summary>
    /// Nice method to create the tenant builder
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the services (application specific tenant class)
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static CompanyInformationBuilder<T> AddMultiClient<T>(this IServiceCollection services) where T : Client
            => new CompanyInformationBuilder<T>(services);

        /// <summary>
        /// Add the services (default tenant class)
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static CompanyInformationBuilder<Client> AddMultiClient(this IServiceCollection services)
            => new CompanyInformationBuilder<Client>(services);
    }
}
