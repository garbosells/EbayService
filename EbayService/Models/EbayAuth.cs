using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayService.Models
{
    public class EbayAuth
    {
        public EbayOAuthToken UserToken { get; set; }
        public EbayOAuthToken RefreshToken { get; set; }
    }
}
