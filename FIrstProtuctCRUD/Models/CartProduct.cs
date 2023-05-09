using FIrstProtuctCRUD.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FIrstProductCRUD.Models
{
    public class CartProduct
    {
        [Key]
        public int ID;
        public int UserId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage ="Количество товара  должно быть больше нуля")]
        public int QuantityProducts { get; set; }

        public virtual Product Product { get; set; }

    }
}
