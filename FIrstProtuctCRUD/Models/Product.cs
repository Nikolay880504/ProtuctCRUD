
using FIrstProductCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FIrstProtuctCRUD.Models
{
    public class Product 
    {         
        public string Name { get; set; }     
        public double Price { get; set; }     
        public int Code { get; set; }
        public int Quantity { get; set; }
        [Key]
        public int Id { get; set; }

        public virtual ICollection<CartProduct>  CartProducts  { get; set; }
        public virtual ICollection<OrderElement> OrderElements { get; set; }    
    }
}
