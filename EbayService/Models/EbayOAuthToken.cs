using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayService.Models
{
    public class EbayOAuthToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
