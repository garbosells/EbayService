using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ListingService.Models.EbayClasses
{
    public class EbayInventoryItem
    {
        public Availability availability { get; set; }
        public string condition { get; set; }
        public Product product { get; set; }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
