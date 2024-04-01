using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Core.Specifications;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IGenericRepository<Product> _productsRepo;
        //private readonly IGenericRepository<DeliveryMethod> _deliveryMethodsRepo;
        //private readonly IGenericRepository<Order> _orderRepo;

        public OrderService(
            IBasketRepository basketRepo,
            //IGenericRepository<Product> productsRepo,
            //IGenericRepository<DeliveryMethod> deliveryMethodsRepo,
            //IGenericRepository<Order> orderRepo
            IUnitOfWork unitOfWork)
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
            //_productsRepo = productsRepo;
            //_deliveryMethodsRepo = deliveryMethodsRepo;
            //_orderRepo = orderRepo;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            //1. Get Basket from Basket Repo

            var basket = await _basketRepo.GetBasketAsync(basketId);
            
            //2. Get Selected items in the basket from Product Repo

            var orderItems = new List<OrderItem>();

            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);

                var productItemOrdered = new ProductItemOrdered(product.Id,product.Name,product.PictureUrl);

                var orderItem = new OrderItem(productItemOrdered,item.Quantity,product.Price);

                orderItems.Add(orderItem);
            }

            //3. Calculate SubTotal

            var subTotal = orderItems.Sum(item => item.Quantity * item.Price);

            //4. Get DeliveryMethod from delivery Method Repo

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            //5. Create Order

            var order = new Order(buyerEmail,shippingAddress,deliveryMethod,orderItems,subTotal);

            await _unitOfWork.Repository<Order>().CreateAsync(order);

            //6. Save to DataBase.

            var result = await _unitOfWork.Complete();

            if (result <= 0)
                return null;
            return order;

        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            var deliveryMethods = _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();

            return deliveryMethods;
        }

        public async Task<Order> getOrderByIdForUser(int orderId, string buyerEmail)
        {
            var spec = new OrderWithItemAndDeliveryMethodsSpecifications(buyerEmail ,orderId);
            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
        {
            var spec = new OrderWithItemAndDeliveryMethodsSpecifications(buyerEmail);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return orders;
        }
    }
}
