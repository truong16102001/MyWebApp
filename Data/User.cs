namespace MyWebApp.Data
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Fullname { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
