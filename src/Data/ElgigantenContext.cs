using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreCodeCamp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CoreCodeCamp.Data
{
    public class ElgigantenContext : DbContext
    {
        private readonly IConfiguration _config;

        public ElgigantenContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        public DbSet<ProductInfo> ProductInfo { get; set; }
        public DbSet<Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("Elgiganten"));
        }

        //protected override void OnModelCreating(ModelBuilder bldr)
        //{
        //    bldr.Entity<ProductInfo>()
        //      .HasData(new
        //      {
        //          ProductInfoID = 1,
        //          ProductDescription = "Amazing phone omg apple",
        //          NetDimensions = "0",
        //          NetWeight = 0.193,
        //          GrossDimensions = "2.8 x 9.0 x 16.4",
        //          GrossWeight = 0.324,
        //          EAN = 194252707470,
        //          GTIN = 194252707470
        //      }) ;
    //}
    }
}
