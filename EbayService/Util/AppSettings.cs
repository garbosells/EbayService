using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbayService.Models;

namespace EbayService
{
    public class AppSettings
    {
        public string EbayBaseURL { get; set; }
        public EbayAuth EbayAuth { get; set; }
        public string EBayAuthCallbackKey { get; set; }
        public Dictionary<string,string> ConnectionStrings { get; set; }
    }
}
