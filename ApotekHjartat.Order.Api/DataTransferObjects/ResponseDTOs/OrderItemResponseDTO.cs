namespace ApotekHjartat.Order.Api.DataTransferObjects.ResponseDTOs
{
    public class OrderItemResponseDTO
    {
        public Guid OrderItemId { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
