using AutoMapper;
using Billine.Admin.Api.Authorization;
using Billine.Admin.Contracts.Orders;
using Billine.Admin.Domain.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(List<OrderResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByUser()
        {
            List<Order> response = await _orderService.GetByLoggedUser(User.GetId());

            return Ok(_mapper.Map<List<OrderResponse>>(response));
        }

        [HttpGet, Route("{qrCodeId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] string qrCodeId)
        {
            Order response = await _orderService.GetById(User.GetId(), qrCodeId);

            return Ok(_mapper.Map<OrderResponse>(response));
        }
    }
}
