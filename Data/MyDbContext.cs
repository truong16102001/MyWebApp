using Microsoft.EntityFrameworkCore;
using MyWebApp.Data;

namespace MyWebApp.Data
{
    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }

        #region DbSet
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(u =>
            {
                u.ToTable("Users");
                u.HasKey(user => user.UserId);
                u.Property(user => user.Email).IsRequired();
                u.HasIndex(user => user.Email).IsUnique();// set email unique

            });


            modelBuilder.Entity<Order>(o =>
            {
                o.ToTable("Orders");
                o.HasKey(order => order.OrderId);
                o.Property(order => order.OrderTime).
                HasDefaultValueSql("getutcdate()");
                o.Property(order => order.Address).IsRequired();
                o.Property(order => order.Phone).IsRequired();
                o.Property(order => order.Receiver).IsRequired().HasMaxLength(100);


                o.HasOne(order => order.User).
                WithMany(user => user.Orders).
                HasForeignKey(order => order.UserId).HasConstraintName("FK_User_Orders").IsRequired(false).
                OnDelete(DeleteBehavior.SetNull);

            });

            modelBuilder.Entity<OrderDetails>(od =>
            {
                od.ToTable("OrderDetails");
                // set 2 cột làm khóa chính
                od.HasKey(orderDetails => new { orderDetails.OrderId, orderDetails.ProductId });

                // order-orderdetials
                od.HasOne(orderdetails => orderdetails.Order).
                WithMany(order => order.OrderDetails).
                HasForeignKey(orderdetails => orderdetails.OrderId).
                HasConstraintName("FK_Order_OrderDetails");


                // product-orderdetials
                od.HasOne(orderdetails => orderdetails.Product).
                WithMany(product => product.OrderDetails).
                HasForeignKey(orderdetails => orderdetails.ProductId).
                HasConstraintName("FK_Product_OrderDetails");

            });
        }
    }
}
