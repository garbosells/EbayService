using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayService
{
    public class AppSettings
    {
        public string EbayBaseURL { get; set; }
        public Dictionary<string,string> EbayAuth { get; set; }
    }
}
