namespace API.Dtos
{
    public class OrderDetailDtos
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductAmmount { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
