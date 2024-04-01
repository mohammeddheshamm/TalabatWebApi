﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities;

namespace TalabatAPIS.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }
}
