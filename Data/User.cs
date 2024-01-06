namespace MyWebApp.Data
{
    public class User
    {
        public int UserId { get; set; }
        public string Fullname { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Order> Orders { get; set; }

        public User()
        {
            Orders = new List<Order>();
        }
    }
}
