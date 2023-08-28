namespace ApotekHjartat.Order.Api.DataTransferObjects.RequestDTOs
{
    public class OrderItemRequestDTO
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
