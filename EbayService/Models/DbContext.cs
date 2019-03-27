using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayService.Models
{
    public class DbModelContext : DbContext
    {
        private readonly IOptions<AppSettings> settings;
        public DbModelContext(DbContextOptions<DbModelContext> options, IOptions<AppSettings> settings)
    : base(options)
        { this.settings = settings; }

        public DbModelContext() { }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyEbayAuth> CompanyEbayAuths { get; set; }
    }

    public class Company
    {
        public Company() { }
        public long companyId { get; set; }
        public long companyEbayAuthId { get; set; }
    }

    public class CompanyEbayAuth
    {
        public CompanyEbayAuth() { }
        public long companyEbayAuthId { get; set; }
        public string userToken { get; set; }
        public DateTime userTokenExpiration { get; set; }
        public string refreshToken { get; set; }
        public DateTime refreshTokenExpiration { get; set; }
    }
}

