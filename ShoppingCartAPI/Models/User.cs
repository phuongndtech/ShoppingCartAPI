namespace ShoppingCartAPI.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
