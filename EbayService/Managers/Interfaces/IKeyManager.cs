using EbayService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayService.Managers.Interfaces
{
    public interface IKeyManager
    {
        bool SetEbayAuthByCompanyId(long companyId, EbayAuth auth);
        Task<EbayOAuthToken> GetEbayUserTokenByCompanyId(long companyId);
        Task<EbayOAuthToken> GetEbayRefreshTokenByCompanyId(long companyId);
        Task<bool> SetEbayTokenByCompanyId(long companyId, EbayOAuthToken token);
    }
}
