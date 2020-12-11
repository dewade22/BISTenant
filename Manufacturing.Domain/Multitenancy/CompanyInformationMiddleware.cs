using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Manufacturing.Domain.Multitenancy
{
    internal class CompanyInformationMiddleware<T> where T : Client
    {
        private readonly RequestDelegate next;

        public CompanyInformationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Items.ContainsKey(MultiCompanyInformationConstants.HttpContextCompanyInformationKey))
            {
                var CompanyInformationService = context.RequestServices.GetService(typeof(CompanyInformationAccessService<T>)) as CompanyInformationAccessService<T>;
                context.Items.Add(MultiCompanyInformationConstants.HttpContextCompanyInformationKey, await CompanyInformationService.GetClient());
            }

            //Continue processing
            if (next != null)
                await next(context);
        }
    }
}
