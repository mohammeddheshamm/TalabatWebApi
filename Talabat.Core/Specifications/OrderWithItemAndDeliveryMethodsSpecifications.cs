using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Specifications
{
    public class OrderWithItemAndDeliveryMethodsSpecifications :BaseSpecification<Order>
    {
        // This constructor is used for getting all Orders to a specific User.
        public OrderWithItemAndDeliveryMethodsSpecifications(string buyerEmail)
            : base(O => O.BuyerEmail == buyerEmail ) // Sending the citeria to the base constructor.
        {
            // In Items we Use eager loading even if it is many not one as the relation is composition not association. 
            Includes.Add(O => O.Items);
            Includes.Add(O => O.DeliveryMethod);

            AddOrderByDescending(O => O.OrderDate);
        }

        // This constructor is used for getting a specific order b id for a specific user.
        public OrderWithItemAndDeliveryMethodsSpecifications(string buyerEmail, int Id)
            : base(O => O.BuyerEmail == buyerEmail && O.Id == Id) // Sending the citeria to the base constructor.
        {
            // In Items we Use eager loading even if it is many not one as the relation is composition not association. 
            Includes.Add(O => O.Items);
            Includes.Add(O => O.DeliveryMethod);
        }
    }
}
