namespace ShoppingCartAPI.DTO.Responses
{
    public class ProductResponse:ResponseBase
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public string ImageFileName { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public int BrandId { get; set; }
        public bool IsHot { get; set; }
    }
}
