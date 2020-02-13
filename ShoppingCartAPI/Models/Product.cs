namespace ShoppingCartAPI.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public string ImageFileName { get; set; }
        public int? BrandId { get; set; }
        public bool? IsHot { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
