using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Services;
using TalabatAPIS.Dtos;
using TalabatAPIS.Errors;

namespace TalabatAPIS.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        // Service calls more than one repository 
        public OrdersController(
            IOrderService orderService,
            IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost] // POST : /api/Orders
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var orderAddress = _mapper.Map<AddressDto,Address>(orderDto.ShippingAddress);

            var order = await _orderService.CreateOrderAsync(buyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, orderAddress);

            if (order == null) return BadRequest(new ApiResponse(400));

            //var mappedOrder = _mapper.Map<Order,OrderToReturnDto>(order);

            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }

        [HttpGet] // GET : /api/Orders
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var orders = await _orderService.GetOrderForUserAsync(buyerEmail);

            //var mappedOrders = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders);

            return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders));
        }

        [HttpGet("{id}")] // GET : /api/Orders/id
        public async Task<ActionResult<OrderToReturnDto>> GetOrdersForUser(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.getOrderByIdForUser(id,buyerEmail);
            if (order == null) return BadRequest(new ApiResponse(400));
            //var mappedOrder = _mapper.Map<Order, OrderToReturnDto>(order);
            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }

        [HttpGet("deliverymethod")] // GET : /api/Orders/deliverymethod
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _orderService.GetDeliveryMethodAsync();
            return Ok(deliveryMethods); 
        }


    }

}
