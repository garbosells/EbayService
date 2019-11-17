using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EbayService.Controllers.RequestObjects;
using EbayService.Controllers.ResponseObjects;
using EbayService.Managers.Interfaces;
using EbayService.Models;
using EbayService.Models.EbayClasses;
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

        public string EbayBaseURL = "https://api.ebay.com/sell/inventory/v1/";
        public ListingController(IOptions<AppSettings> settings, IAuthorizationManager authorizationManager, IConfiguration configuration)
        {
            this.settings = settings;
            this.authorizationManager = authorizationManager;
            this.configuration = configuration;
        }


        [HttpPost]
        [Route("api/Listing/Postlisting")]
        public async Task<string> PostListingAsync([FromBody] PostListingRequest request)
        {
            try
            {
                var myitem = new EbayInventoryItem
                {
                    condition = request.inventoryItem.condition,
                    product = request.inventoryItem.product,
                    availability = request.inventoryItem.availability
                };
                var createInventoryItemResponse = CreateEbayInventoryItem(myitem).Result;
                if ((bool)createInventoryItemResponse.IsSuccess)
                {
                    var sku = createInventoryItemResponse.Sku;
                    var offer = new Offer(request.paymentPolicyId, request.fulfillmentPolicyId, request.returnPolicyId, request.merchantLocationKey, request.price, sku);
                    offer.categoryId = request.categoryId;
                    var offerId = CreateOffer(offer).Result.offerId;
                    var publishOfferResponse = await PublishOffer(offerId);
                    return publishOfferResponse.ListingId;

                }
                throw new Exception("Problem generating inventory item: " + createInventoryItemResponse.ErrorMessage);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                telemetryClient.TrackException(ex);
                //return new PostListingResponse
                //{
                //    IsSuccess = false,
                //    ErrorMessage = ex.Message
                //};
                return ex.Message;
            }
        }

        private async Task<CreateInventoryItemResponse> CreateEbayInventoryItem(EbayInventoryItem item)
        {
            var sku = Guid.NewGuid().ToString("N").ToUpper();
            var uri = $"{EbayBaseURL}inventory_item/{sku}";
            var auth = await authorizationManager.GetTokenByCompanyId(0);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + auth.Token);
            var payload = JsonConvert.SerializeObject(item);
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
                return new CreateInventoryItemResponse
                {
                    IsSuccess = false,
                    ErrorMessage = await httpResponseMessage.Content.ReadAsStringAsync()
                };
            }
        }

        private async Task<CreateOfferResponse> CreateOffer(Offer offer)
        {
            var uri = $"{EbayBaseURL}/offer";
            var auth = await authorizationManager.GetTokenByCompanyId(0);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + auth.Token);
            var payload = JsonConvert.SerializeObject(offer);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            content.Headers.Add("Content-Language", "en-US");

            var httpResponseMessage = client.PostAsync(uri, content).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var response = await httpResponseMessage.Content.ReadAsAsync<CreateOfferResponse>();
                return response;
            }
            return null;
            //else
            //{
            //    return new CreateInventoryItemResponse
            //    {
            //        IsSuccess = false,
            //        ErrorMessage = await httpResponseMessage.Content.ReadAsStringAsync() + "\n\n" + payload
            //    };
            //}
        }

        private async Task<PublishOfferResponse> PublishOffer(string offerId)
        {
            try
            {
                var uri = $"{EbayBaseURL}offer/{offerId}/publish";
                var auth = await authorizationManager.GetTokenByCompanyId(0);
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + auth.Token);

                var httpResponseMessage = client.PostAsync(uri, null).Result;
                var response = httpResponseMessage.Content.ReadAsAsync<PublishOfferResponse>().Result;
                var stringResponse = httpResponseMessage.Content.ReadAsStringAsync();
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return new PublishOfferResponse
                    {
                        IsSuccess = true,
                        ListingId = response.ListingId
                    };
                }
                else
                {
                    throw new Exception("Problem while publishing offer: " + httpResponseMessage.Content.ReadAsStringAsync().Result);
                }
            } catch (Exception ex)
            {
                return new PublishOfferResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
            
        }
    }
}
