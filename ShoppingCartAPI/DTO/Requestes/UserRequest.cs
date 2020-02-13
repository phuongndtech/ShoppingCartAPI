using System.ComponentModel.DataAnnotations;

namespace ShoppingCartAPI.DTO
{
    public class UserRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FullName { get; set; }
    }
}