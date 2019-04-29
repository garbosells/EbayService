using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EbayService.Managers.Interfaces;
using EbayService.Models;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EbayService.Controllers
{
    [ApiController]
    public class LocationController : ControllerBase
    {
        TelemetryClient telemetryClient = new TelemetryClient();
        private readonly IOptions<AppSettings> settings;
        private readonly IAuthorizationManager authorizationManager;

        public LocationController(IOptions<AppSettings> settings, IAuthorizationManager authorizationManager)
        {
            this.settings = settings;
            this.authorizationManager = authorizationManager;
        }

        // GET api/GetLocations
        [HttpGet]
        [Route("api/Location/GetAllLocations")]
        public async Task<ActionResult<EbayLocationResponse>> GetLocationsAsync(long companyId)
        {
            telemetryClient.TrackEvent("GetLocations");
            string baseUrl = settings.Value.EbayBaseURL;
            string authToken = authorizationManager.GetTokenByCompanyId(companyId).Result.Token;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

                var req_uri = baseUrl + "location/?";
                using (HttpResponseMessage res = await client.GetAsync(req_uri))
                using (HttpContent content = res.Content)
                {
                    string data = await content.ReadAsStringAsync();
                    var response = SimpleJson.SimpleJson.DeserializeObject<EbayLocationResponse>(data);
                    return response;
                }
            }
        }
    }
}