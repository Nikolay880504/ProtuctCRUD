using FIrstProductCRUD.Models;
using FIrstProtuctCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace FIrstProductCRUD.Data
{
    public class OrderStorage : IServiceOrderStorage
    {
        ApplicationContext _context;
        public OrderStorage(ApplicationContext context)
        {
            _context = context;
        }

        public void AddOrder(int userId)
        {         
            var order = new Order();
            double priceTotal = 0;
            var orderElements = new List<OrderElement>();
            var products = _context.Carts.Where(c => c.UserId == userId).
                Include(c => c.Product).ToList();

            foreach (CartProduct product in products)
            {
                var orderElement = new OrderElement();

                priceTotal += product.Product.Price * product.QuantityProducts;            
                orderElement.ProductId = product.ProductId;
                orderElement.Quantity = product.QuantityProducts;
                orderElement.PriceProduct = product.ProductId;
                orderElement.Order = order;

                orderElements.Add(orderElement);
            }

            order.UserId = userId;
            order.PriceTotal = priceTotal;
            order.Elements = orderElements;
            _context.Add(order);
            _context.SaveChanges();       
        }
    }
}
