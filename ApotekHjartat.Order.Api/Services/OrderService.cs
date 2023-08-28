using ApotekHjartat.Order.Api.DataTransferObjects.RequestDTOs;
using ApotekHjartat.Order.Api.Enums;
using ApotekHjartat.Order.Api.Exceptions;
using ApotekHjartat.Order.Api.Helpers;
using ApotekHjartat.Order.Api.Repositories.ApotekHjartat;
using ApotekHjartat.Order.Api.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApotekHjartat.Order.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly ApotekHjartatContext _apotekHjartatContext;

        public OrderService(IMapper mapper, ApotekHjartatContext apotekHjartatContext)
        {
            _mapper = mapper;
            _apotekHjartatContext = apotekHjartatContext;
        }

        public async Task<IEnumerable<Repositories.ApotekHjartat.Order>> GetOrders()
        {
            var orders = await _apotekHjartatContext.Orders.AsNoTracking().Include(x => x.OrderItems).ToListAsync();

            return orders;
        }

        public async Task<Repositories.ApotekHjartat.Order> GetOrderById(Guid orderId)
        {
            var order = await _apotekHjartatContext.Orders.AsNoTracking().Include(x => x.OrderItems)
                .FirstOrDefaultAsync(x => x.OrderId == orderId);

            return order;
        }

        public async Task<Repositories.ApotekHjartat.Order> CreateOrder(OrderRequestDTO orderRequestDTO)
        {
            var order = await _apotekHjartatContext.Orders.FirstOrDefaultAsync(x => x.UserId == orderRequestDTO.UserId
                && x.Status == (int)Status.Pending);

            if(order == null)
            {
                order = new Repositories.ApotekHjartat.Order
                {
                    OrderId = Guid.NewGuid(),
                    UserId = orderRequestDTO.UserId,
                    CreatedAt = Helper.ConvertDateTimeToString(DateTime.Now),
                    Status = (int)Status.Pending
                };

                await _apotekHjartatContext.Orders.AddAsync(order);
            }

            foreach (var item in orderRequestDTO.OrderItems)
            {
                var product = await _apotekHjartatContext.Products.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ProductId == item.ProductId);

                if(product == null)
                {
                    throw new Exceptions.KeyNotFoundException($"Failed to Create Order. Product with Id {item.ProductId} was not found");
                }

                var orderItem = new OrderItem
                {
                    OrderItemId = Guid.NewGuid(),
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                order.UpdatedAt = Helper.ConvertDateTimeToString(DateTime.Now);

                order.OrderItems.Add(orderItem);

                await _apotekHjartatContext.SaveChangesAsync();
            }

            return order;
        }

        public async Task<Repositories.ApotekHjartat.Order> UpdateOrder(Guid orderId, OrderRequestDTO orderRequestDTO)
        {
            var order = await _apotekHjartatContext.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId 
                && x.UserId == orderRequestDTO.UserId && x.Status == (int)Status.Pending);

            if(order == null)
            {
                throw new NotFoundException($"Order with Id {orderId} was not found.");
            }

            foreach (var item in orderRequestDTO.OrderItems)
            {
                var product = await _apotekHjartatContext.Products.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ProductId == item.ProductId);

                if (product == null)
                {
                    throw new Exceptions.KeyNotFoundException($"Failed to Update Order. Product with Id {item.ProductId} was not found");
                }

                var orderItem = await _apotekHjartatContext.OrderItems.FirstOrDefaultAsync(x => x.OrderId == order.OrderId
                    && x.ProductId == product.ProductId);

                if (orderItem == null)
                {
                    throw new Exceptions.KeyNotFoundException($"Failed to Update Order. OrderItem For Order with Id {orderId} and Product with Id {product.ProductId} was not found");
                }

                if (item.Quantity < 1 && orderItem.Quantity < 1)
                {
                    _apotekHjartatContext.OrderItems.Remove(orderItem);
                }
                else
                {
                    orderItem.Quantity += item.Quantity;
                }

                order.UpdatedAt = Helper.ConvertDateTimeToString(DateTime.Now);

                await _apotekHjartatContext.SaveChangesAsync();
            }

            return order;
        }

        public async Task<Repositories.ApotekHjartat.Order> UpdateOrderStatus(Guid orderId, UpdateOrderStatusRequestDTO updateOrderStatusRequestDTO)
        {
            var order = await _apotekHjartatContext.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId
                && x.UserId == updateOrderStatusRequestDTO.UserId);

            if (order == null)
            {
                throw new NotFoundException($"Order with Id {orderId} was not found.");
            }

            order.Status = updateOrderStatusRequestDTO.Status;

            await _apotekHjartatContext.SaveChangesAsync();

            return order;
        }

    }
}
