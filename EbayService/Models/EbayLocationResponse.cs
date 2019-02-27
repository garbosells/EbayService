using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayService.Models
{
    public class EbayLocationResponse
    {
        public string href { get; set; }
        public string limit { get; set; }
        public EbayLocation[] locations { get; set; }
        public string offset { get; set; }
        public string prev { get; set; }
        public string total { get; set; }
    }
}
