using System.Collections.Generic;

namespace ShoppingCartAPI.Models
{
    public partial class Category
    {
        public Category()
        {
            ProductCategory = new HashSet<ProductCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductCategory> ProductCategory { get; set; }
    }
}
