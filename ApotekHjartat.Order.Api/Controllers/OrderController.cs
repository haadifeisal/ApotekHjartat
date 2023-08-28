using ApotekHjartat.Order.Api.DataTransferObjects.ResponseDTOs;
using ApotekHjartat.Order.Api.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApotekHjartat.Order.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public OrderController(IMapper mapper, IOrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IEnumerable<OrderResponseDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetOrders();

            if (!orders.Any())
            {
                return NoContent();
            }

            var mappedResult = _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);

            return Ok(mappedResult);
        }

        [HttpGet("{orderId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(OrderResponseDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrderById([FromRoute] Guid orderId)
        {
            var order = await _orderService.GetOrderById(orderId);

            if (order == null)
            {
                return NotFound();
            }

            var mappedResult = _mapper.Map<OrderResponseDTO>(order);

            return Ok(mappedResult);
        }
    }
}
