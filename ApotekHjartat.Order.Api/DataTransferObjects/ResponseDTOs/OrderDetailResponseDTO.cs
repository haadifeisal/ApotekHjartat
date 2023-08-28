using ApotekHjartat.Order.Api.Repositories.ApotekHjartat;

namespace ApotekHjartat.Order.Api.DataTransferObjects.ResponseDTOs
{
    public class OrderDetailResponseDTO
    {
        public Guid OrderDetailId { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
