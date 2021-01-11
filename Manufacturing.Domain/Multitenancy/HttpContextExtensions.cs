using Microsoft.AspNetCore.Http;


namespace Manufacturing.Domain.Multitenancy
{
    /// <summary>
    /// Extensions to HttpContext to make multi-tenancy easier to use
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Returns the current CompanyInformation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public static T GetClient<T>(this HttpContext context) where T : Client
        {
            if (!context.Items.ContainsKey(MultiCompanyInformationConstants.HttpContextCompanyInformationKey))
                return null;
            return context.Items[MultiCompanyInformationConstants.HttpContextCompanyInformationKey] as T;
        }
        
        /// <summary>
        /// Returns the current CompanyInformation
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Client GetClient(this HttpContext context)
        {
            return context.GetClient<Client>();
        }
    }
}
