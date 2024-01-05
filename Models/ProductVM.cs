using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyWebApp.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MyWebApiApp.Models
{
    public class ProductVM
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }    
        public double UnitPrice { get; set; }
      
        public int UnitInStock { get; set; }

        public CategoryVM? CategoryVM { get; set; }
    }

    
}
