using System.ComponentModel.DataAnnotations;

namespace FIrstProductCRUD.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public double PriceTotal { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<OrderElement> Elements { get; set; }

    }
}
