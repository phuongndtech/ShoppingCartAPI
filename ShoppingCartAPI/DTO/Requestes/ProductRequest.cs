using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartAPI.DTO
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public string ImageFileName { get; set; }
        public int? BrandId { get; set; }
        public bool IsHot { get; set; }
    }
}
