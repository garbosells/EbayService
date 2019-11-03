using System;
using ListingService.Models.EbayClasses;

namespace EbayService.Controllers.RequestObjects
{
    public class PostListingRequest
    {
        public string categoryId { get; set; }
        public string paymentPolicyId { get; set; }
        public string fulfillmentPolicyId { get; set; }
        public string returnPolicyId { get; set; }
        public string price;
        public string merchantLocationKey { get; set; }
        public EbayInventoryItem inventoryItem { get; set; }
        public PostListingRequest()
        {
        }
    }
}
