using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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

        public LocationController(IOptions<AppSettings> settings)
        {
            this.settings = settings;
        }

        // GET api/GetLocations
        [HttpGet]
        [Route("api/Location/GetAllLocations")]
        public async Task<ActionResult<EbayLocationResponse>> GetLocationsAsync()
        {
            telemetryClient.TrackEvent("GetLocations");
            string baseUrl = settings.Value.EbayBaseURL;
            string authToken = string.Empty; //settings.Value.EbayAuth["UserToken"];

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
                //}
            }
        }
    }
}