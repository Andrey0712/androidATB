using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAtb.Data.Entities
{
    [Table("tblCategory")]
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
