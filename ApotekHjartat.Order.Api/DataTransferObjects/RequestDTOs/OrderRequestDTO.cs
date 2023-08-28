namespace ApotekHjartat.Order.Api.DataTransferObjects.RequestDTOs
{
    public class OrderRequestDTO
    {
        public Guid UserId { get; set; }
        public List<OrderItemRequestDTO> OrderItems { get; set; }
    }
}
