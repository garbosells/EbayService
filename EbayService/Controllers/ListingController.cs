using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EbayService.Controllers.ResponseObjects;
using EbayService.Managers.Interfaces;
using EbayService.Models;
using ListingService.Models.EbayClasses;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace EbayService.Controllers
{
    [ApiController]
    public class ListingController : ControllerBase
    {
        TelemetryClient telemetryClient = new TelemetryClient();
        private readonly IOptions<AppSettings> settings;
        private readonly IAuthorizationManager authorizationManager;
        private readonly IConfiguration configuration;

        public ListingController(IOptions<AppSettings> settings, IAuthorizationManager authorizationManager, IConfiguration configuration)
        {
            this.settings = settings;
            this.authorizationManager = authorizationManager;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("api/Listing/")]
        public async Task<PostListingResponse> PostListing()
        {
            PostListingResponse response = null;
            try
            {

            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex);
                return new PostListingResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
            return response;
        }

        [HttpPost]
        [Route("api/Listing/CreateInventoryItem")]
        public async Task<Response> CreateInventoryItem([FromBody] EbayInventoryItem item)
        {
            try
            {
                var myitem = new EbayInventoryItem
                {
                    condition = item.condition,
                    product = item.product,
                    availability = item.availability
                };
                var sku = Guid.NewGuid().ToString("N").ToUpper();
                var uri = $"{settings.Value.EbayBaseURL}inventory_item/{sku}";
                var auth = await authorizationManager.GetTokenByCompanyId(0);
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + auth.Token);
                var payload = JsonConvert.SerializeObject(myitem);
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                content.Headers.Add("Content-Language", "en-US");

                var httpResponseMessage = client.PutAsync(uri, content).Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return new CreateInventoryItemResponse
                    {
                        IsSuccess = true,
                        Sku = sku
                    };
                }
                else
                {
                    return new Response
                    {
                        IsSuccess = false,
                        ErrorMessage = await httpResponseMessage.Content.ReadAsStringAsync() + "\n\n" + payload
                    };
                }
            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex);
                return new PostListingResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
