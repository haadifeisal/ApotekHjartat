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
            var orders = await _apotekHjartatContext.Orders.AsNoTracking().Include(x => x.OrderDetails).ToListAsync();

            return orders;
        }

        public async Task<Repositories.ApotekHjartat.Order> GetOrderById(Guid orderId)
        {
            var order = await _apotekHjartatContext.Orders.AsNoTracking().Include(x => x.OrderDetails)
                .FirstOrDefaultAsync(x => x.OrderId == orderId);

            return order;
        }
    }
}
