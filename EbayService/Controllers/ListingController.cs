using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbayService.Controllers.ResponseObjects;
using EbayService.Managers.Interfaces;
using EbayService.Models;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

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
    }
}
