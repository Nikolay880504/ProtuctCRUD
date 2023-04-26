using FIrstProtuctCRUD.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FIrstProductCRUD.Models
{
   
    public class OrderElement
    {
        [Key]
        public int Id;
        [ForeignKey("Order")]
        public int OrderId { get; set; }
       
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double PriceProduct { get; set; }

        public virtual Order Order { get; set;}
        public virtual Product Product { get; set; }
    }
}
