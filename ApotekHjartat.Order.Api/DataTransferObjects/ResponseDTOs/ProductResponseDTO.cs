namespace ApotekHjartat.Order.Api.DataTransferObjects.ResponseDTOs
{
    public class ProductResponseDTO
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; } = null!;

        public int Price { get; set; }
    }
}
