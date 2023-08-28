using ApotekHjartat.Order.Api.Repositories.ApotekHjartat;

namespace ApotekHjartat.Order.Api.DataTransferObjects.ResponseDTOs
{
    public class OrderResponseDTO
    {
        public Guid OrderId { get; set; }

        public Guid UserId { get; set; }

        public string CreatedAt { get; set; }

        public string? UpdatedAt { get; set; }

        public ICollection<OrderDetailResponseDTO> OrderDetails { get; set; }
    }
}
