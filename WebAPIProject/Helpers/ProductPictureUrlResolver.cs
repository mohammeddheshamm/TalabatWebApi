﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.Core.Entities;
using TalabatAPIS.Dtos;

namespace TalabatAPIS.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration Configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{Configuration["BaseApiUrl"]}{source.PictureUrl}";
            return null;
        }
    }
}
