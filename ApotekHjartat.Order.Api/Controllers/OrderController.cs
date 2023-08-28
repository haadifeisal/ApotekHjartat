using ApotekHjartat.Order.Api.DataTransferObjects.RequestDTOs;
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(OrderResponseDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequestDTO orderRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _orderService.CreateOrder(orderRequestDTO);

            if (order == null)
            {
                return BadRequest();
            }

            var mappedResult = _mapper.Map<OrderResponseDTO>(order);

            return Ok(mappedResult);
        }

        [HttpPut("{orderId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(OrderResponseDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateOrder([FromRoute] Guid orderId, [FromBody] OrderRequestDTO orderRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _orderService.UpdateOrder(orderId, orderRequestDTO);

            if (order == null)
            {
                return BadRequest();
            }

            var mappedResult = _mapper.Map<OrderResponseDTO>(order);

            return Ok(mappedResult);
        }

        [HttpPut("status/{orderId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(OrderResponseDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateOrderStatus([FromRoute] Guid orderId, [FromBody] UpdateOrderStatusRequestDTO updateOrderStatusRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _orderService.UpdateOrderStatus(orderId, updateOrderStatusRequestDTO);

            if (order == null)
            {
                return BadRequest();
            }

            var mappedResult = _mapper.Map<OrderResponseDTO>(order);

            return Ok(mappedResult);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAllOrders()
        {
            var order = await _orderService.DeleteAllOrders();

            if (!order)
            {
                return BadRequest();
            }

            return Ok(order);
        }
    }
}
