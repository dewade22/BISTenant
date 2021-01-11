using Manufacturing.Domain.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manufacturing.Domain.Multitenancy
{
    public class CompanyInformationDbStore : ICompanyInformationStore<Client>
    {
        private readonly SystemDbContext _SystemDbContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CompanyInformationDbStore> _logger;

        public CompanyInformationDbStore(SystemDbContext SystemDbContext, IConfiguration configuration, ILogger<CompanyInformationDbStore> logger)
        {
            _SystemDbContext = SystemDbContext;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<Client> GetClientAsync(string domainName, string ipAddress, string name)
        {
            Client client = null;

            //if (string.IsNullOrWhiteSpace(name))
            //{
            //    CompanyInformation = TryGetCompanyInformationFromDomainName(domainName, ipAddress);
            //}
            //else
            //{
                try
                {
                client = _SystemDbContext.CompanyInformation.SingleOrDefault(Client => Client.CompanyCode.ToLower() == name.ToLower());
                }
                catch (Exception ex)
                {
                    _logger.LogError($"There were multiple CompanyInformations which have the same domain name that was being looked up. Domain names must be unique.", ex);
                    //CompanyInformation = TryGetCompanyInformationFromDomainName(domainName, ipAddress);
                }
            //}

            if (client == null)
            {
                client = GetDefaultClient();
            }

            if (client == null)
            {
                throw new NullReferenceException($"The CompanyInformation could not be found in the store. With DomainName: {domainName}, IpAddress: {ipAddress}, Name: {name}");
            }

            return await Task.FromResult(client);
        }

        //private CompanyInformation TryGetCompanyInformationFromDomainName(string domainName, string ipAddress)
        //{
        //    if (string.IsNullOrWhiteSpace(domainName))
        //    {
        //        return TryGetCompanyInformationFromIp(ipAddress);
        //    }
        //    else
        //    {
        //        CompanyInformation CompanyInformation = null;

        //        try
        //        {
        //            CompanyInformation = _SystemDbContext.CompanyInformations.SingleOrDefault(CompanyInformation => CompanyInformation.DomainNames.CommaDelimitedStringToList().Any(domainName0 => domainName0 == domainName));
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError($"There were multiple CompanyInformations which have the same domain name that was being looked up. Domain names must be unique.", ex);
        //            return TryGetCompanyInformationFromIp(ipAddress);
        //        }


        //        if (CompanyInformation == null)
        //        {
        //            return TryGetCompanyInformationFromIp(ipAddress);
        //        }
        //        else
        //        {
        //            return CompanyInformation;
        //        }
        //    }
        //}

        //private CompanyInformation TryGetCompanyInformationFromIp(string ipAddress)
        //{
        //    if (string.IsNullOrWhiteSpace(ipAddress))
        //    {
        //        return GetDefaultCompanyInformation();
        //    }
        //    else
        //    {
        //        CompanyInformation CompanyInformation = null;

        //        try
        //        {
        //            CompanyInformation = _SystemDbContext.CompanyInformations.SingleOrDefault(CompanyInformation => CompanyInformation.IpAddresses.CommaDelimitedStringToList().Any(ipAddress0 => ipAddress0 == ipAddress));
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError($"There were multiple CompanyInformations which have the same ip address that was being looked up. Ip Addresses must be unique.", ex);
        //            return GetDefaultCompanyInformation();
        //        }


        //        if (CompanyInformation == null)
        //        {
        //            return GetDefaultClient();
        //        }
        //        else
        //        {
        //            return Client;
        //        }
        //    }
        //}

        private Client GetDefaultClient()
        {
            var defaultClientName = _configuration.GetValue<string>("DefaultClient");
            return _SystemDbContext.CompanyInformation.SingleOrDefault(Client => Client.CompanyCode == defaultClientName);
        }
    }
}
