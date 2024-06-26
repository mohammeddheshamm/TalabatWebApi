﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Product :BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        //[ForeignKey("ProductBrand")] We Do not have to make it like that.
        public int ProductBrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        //[ForeignKey("ProductType")] We Do not have to make it like that.
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

    }
}
