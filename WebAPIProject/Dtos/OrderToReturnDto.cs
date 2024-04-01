using System.Collections.Generic;
using System;
using Talabat.Core.Entities.Order_Aggregate;

namespace TalabatAPIS.Dtos
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } // Dy ana 4ylt mnha al value 3l4an dh al return type m4 baddy value.
        public string Status { get; set; } // Dy hasta2bilha fy string 3l4an t return al value pending aw failed etc..
        public Address ShippingAddress { get; set; }
        //public DeliveryMethod DeliveryMethod { get; set; } //Navigational property [One]
        public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } 
        public string PaymentIntentId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; } // Implicitly will get the value from the function get total at Order Class
    }
}
