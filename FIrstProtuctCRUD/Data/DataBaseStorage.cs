using Microsoft.EntityFrameworkCore;
using FIrstProtuctCRUD.Models;

namespace FIrstProductCRUD.Data
{
    public class DataBaseStorage : IServiceStorage
    {
        ApplicationContext _context;
        public DataBaseStorage(ApplicationContext context) { _context = context; }
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void RemoveProduct(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
        public Product GetByIdOrNull(int productId)
        {
            return _context.Products.FirstOrDefault(p => p.Id == productId);
        }

        public bool IsCodeExists(int code, int? id)
        {

            return _context.Products.Any(c => c.Id != id && c.Code == code);
        }

        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public void Update(Product product)
        {
            var result = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (result == null)
                return;

            result.Name = product.Name;
            result.Price = product.Price;
            result.Quantity = product.Quantity;
            result.Code = product.Code;
            _context.SaveChanges();
        }
    }
}
