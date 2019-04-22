using EbayService.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayService.Util
{
    public class KeyStore
    {
        private readonly IConfiguration configuration;

        public KeyStore(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public EbayOAuthToken UserToken(string companyId)
        {
            
        }
    }
}
