﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eBay.ApiClient.Auth.OAuth2.Model;
using EbayService.Models;

namespace EbayService.Managers.Interfaces
{
    public interface IAuthorizationManager
    {
        void SetEbayAuth(OAuthToken userToken, OAuthToken refreshToken, long companyId);
        Task<EbayOAuthToken> GetTokenByCompanyId(long companyId);
    }
}
