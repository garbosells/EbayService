using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eBay.ApiClient.Auth.OAuth2.Model;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Options;

namespace EbayService.Managers
{
    public class AuthorizationManager
    {
        TelemetryClient telemetryClient = new TelemetryClient();
        private readonly IOptions<AppSettings> settings;

    }
}
