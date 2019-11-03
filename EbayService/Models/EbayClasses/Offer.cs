using System;
namespace EbayService.Models.EbayClasses
{
    public class Offer
    {
        public string categoryId { get; set; }
        public ListingPolicies listingPolicies { get; set; }
        public string merchantLocationKey { get; set; }
        public string sku { get; set; }
        public string format { get; set; }
        public string marketplaceId { get; set; }
        public PricingSummary pricingSummary { get; set; }

        public Offer(string paymentPolicyId, string fulfillmentPolicyId, string returnPolicyId, string merchantLocationKey, string price, string sku)
        {
            listingPolicies = new ListingPolicies(paymentPolicyId, fulfillmentPolicyId, returnPolicyId);
            format = "FIXED_PRICE";
            marketplaceId = "EBAY_US";
            this.merchantLocationKey = merchantLocationKey;
            pricingSummary = new PricingSummary
            {
                price = new Price
                {
                    value = price,
                    currency = "USD"
                }
            };
            this.sku = sku;
        }
    }

    public class ListingPolicies
    {
        public string paymentPolicyId { get; set; }
        public string fulfillmentPolicyId { get; set; }
        public string returnPolicyId { get; set; }
        public bool eBayPlusIfEligible { get; set; }

        public ListingPolicies(string paymentPolicyId, string fulfillmentPolicyId, string returnPolicyId)
        {
            this.paymentPolicyId = paymentPolicyId;
            this.fulfillmentPolicyId = fulfillmentPolicyId;
            this.returnPolicyId = returnPolicyId;
            eBayPlusIfEligible = false;
        }
    }

    public class PricingSummary
    {
        public Price price { get; set; }
    }

    public class Price
    {
        public string value { get; set; }
        public string currency { get; set; }
    }
}
