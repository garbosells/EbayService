using EbayService.Models;

namespace EbayService
{
    public class AppSettings
    {
        public string EbayBaseURL { get; set; }
        public EbayAuth EbayAuth { get; set; }
        public string EBayAuthCallbackKey { get; set; }
    }
}
