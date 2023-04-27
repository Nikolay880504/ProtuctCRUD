using FIrstProductCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace FIrstProductCRUD.Data
{
    public class CartStorage : IServiceCartStorage
    {
        ApplicationContext _context;

        public CartStorage(ApplicationContext context)
        {
            _context = context;
        }
        public void AddCartProduct(CartProduct cartProduct)
        {
            _context.Carts.Add(cartProduct);
            _context.SaveChanges();
        }

        public List<CartProduct> GetCartProductForUser(int userId)
        {
            return _context.Carts.Where(c => c.UserId == userId).Include(c => c.Product).ToList();
        }

        public void RemoveProductFromCart(int cartProductId)
        {
            var product = _context.Carts.FirstOrDefault(c => c.ID == cartProductId);

            if (product != null)
            {
                _context.Carts.Remove(product);
                _context.SaveChanges();
            }

        }

        public void RemoveCart(int? userId)
        {
            _context.Carts.
                Where(c => c.UserId == userId).
                ToList().
                ForEach(c => _context.Remove(c));
            _context.SaveChanges();
        }
    }
}
