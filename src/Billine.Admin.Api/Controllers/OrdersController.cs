using AutoMapper;
using Billine.Admin.Api.Authorization;
using Billine.Admin.Contracts.Orders;
using Billine.Admin.Domain.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Billine.Admin.Api.Controllers
{
    [Route("api/v1/orders")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> GetOrder([FromBody] PostOrderRequest request)
        {
            Order response = await _orderService.Create(User.GetId(), request.QrCodeId);

            return Created($"api/v1/orders/{response.QrCodeId}", _mapper.Map<OrderResponse>(response));
        }
    }
}
