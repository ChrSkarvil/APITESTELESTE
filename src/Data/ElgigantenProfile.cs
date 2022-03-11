using AutoMapper;
using CoreCodeCamp.Data.Entities;
using CoreCodeCamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCodeCamp.Data
{
    public class ElgigantenProfile : Profile
    {
        public ElgigantenProfile()
        {
            this.CreateMap<ProductInfo, ProductInfoModel>()
                .ReverseMap();


            this.CreateMap<Product, ProductModel>()
                .ReverseMap()
                .ForMember(t => t.ProductInfo, opt => opt.Ignore());

        }
    }
}
