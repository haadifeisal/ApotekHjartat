namespace ApotekHjartat.Order.Api.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Repositories.ApotekHjartat.Order>> GetOrders();
        Task<Repositories.ApotekHjartat.Order> GetOrderById(Guid orderId);
    }
}
