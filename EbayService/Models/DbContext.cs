﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EbayService.Models
{
    public class DbModelContext : DbContext
    {
        public DbModelContext(DbContextOptions<DbModelContext> options)
    : base(options)
        { }

        public DbModelContext() { }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyEbayAuth> CompanyEbayAuths { get; set; }
    }

    public class Company
    {
        public Company() { }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long companyId { get; set; }
        public long companyEbayAuthId { get; set; }
    }

    public class CompanyEbayAuth
    {
        public CompanyEbayAuth() { }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long companyEbayAuthId { get; set; }
        public string userToken { get; set; }
        public DateTime userTokenExpiration { get; set; }
        public string refreshToken { get; set; }
        public DateTime refreshTokenExpiration { get; set; }
    }
}

