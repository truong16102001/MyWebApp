using Microsoft.EntityFrameworkCore;

namespace MyWebApp.Data
{
    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }

        #region DbSet
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        #endregion
    }
}
