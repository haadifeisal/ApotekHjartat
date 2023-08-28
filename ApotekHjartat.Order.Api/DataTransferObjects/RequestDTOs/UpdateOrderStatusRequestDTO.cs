namespace ApotekHjartat.Order.Api.DataTransferObjects.RequestDTOs
{
    public class UpdateOrderStatusRequestDTO
    {
        public Guid UserId { get; set; }
        public int Status { get; set; }
    }
}
