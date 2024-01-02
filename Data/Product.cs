// folder data chứa các entities map với các bảng trong db
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MyWebApp.Data
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        [Range(0, double.MaxValue)]
        public double UnitPrice { get; set; }

        [Range(0, int.MaxValue)]
        public int UnitInStock { get; set; }

        [MaybeNull]
        [Column("CategoryId", TypeName = "int")]
        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
      
    }
}
