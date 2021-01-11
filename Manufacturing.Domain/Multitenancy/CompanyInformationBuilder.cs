using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Manufacturing.Domain.Multitenancy
{
    public class CompanyInformationBuilder<T> where T : Client
    {
        private readonly IServiceCollection _services;

        public CompanyInformationBuilder(IServiceCollection services)
        {
            services.AddTransient<CompanyInformationAccessService<T>>();
            _services = services;
        }

        /// <summary>
        /// Register the tenant resolver implementation
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public CompanyInformationBuilder<T> WithResolutionStrategy<V>(ServiceLifetime lifetime = ServiceLifetime.Transient) where V : class, ICompanyInformationResolutionStrategy
        {
            _services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            _services.Add(ServiceDescriptor.Describe(typeof(ICompanyInformationResolutionStrategy), typeof(V), lifetime));
            return this;
        }

        /// <summary>
        /// Register the tenant store implementation
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public CompanyInformationBuilder<T> WithStore<V>(ServiceLifetime lifetime = ServiceLifetime.Transient) where V : class, ICompanyInformationStore<T>
        {
            _services.Add(ServiceDescriptor.Describe(typeof(ICompanyInformationStore<T>), typeof(V), lifetime));
            return this;
        }
    }
}
