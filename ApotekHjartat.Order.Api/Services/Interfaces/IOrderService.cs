using ApotekHjartat.Order.Api.DataTransferObjects.RequestDTOs;

namespace ApotekHjartat.Order.Api.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Repositories.ApotekHjartat.Order>> GetOrders();
        Task<Repositories.ApotekHjartat.Order> GetOrderById(Guid orderId);
        Task<Repositories.ApotekHjartat.Order> CreateOrder(OrderRequestDTO orderRequestDTO);
        Task<Repositories.ApotekHjartat.Order> UpdateOrder(Guid orderId, OrderRequestDTO orderRequestDTO);
        Task<Repositories.ApotekHjartat.Order> UpdateOrderStatus(Guid orderId, UpdateOrderStatusRequestDTO updateOrderStatusRequestDTO);
    }
}
