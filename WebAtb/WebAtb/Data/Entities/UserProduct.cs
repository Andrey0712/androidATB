using System.ComponentModel.DataAnnotations.Schema;
using WebAtb.Data.Entities.Identity;

namespace WebAtb.Data.Entities
{
    [Table("tblUserProduct")]
    public class UserProduct
    {
        public long UserId { get; set; }
        public virtual AppUser User { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
