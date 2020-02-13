using System.Collections.Generic;

namespace ShoppingCartAPI.DTO.Responses
{
    public class ProductCategoryResponse : ResponseBase
    {
        public List<BrandResponseCategory> BrandResponses { get; set; }
    }

    public class BrandResponseCategory
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public List<ProductResponse> Products { get; set; }
    }
    public class ProductCategoryModel
    {
        public int BrandId { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
    }

}