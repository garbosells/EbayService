using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
namespace ListingService.Models.EbayClasses
{
    public class Product
    {
        public Dictionary<string, string[]> aspects { get; set; }
        public string description { get; set; }
        public string title { get; set; }
    }
}
