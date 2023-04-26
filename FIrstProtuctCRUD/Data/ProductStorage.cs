using FIrstProtuctCRUD.Models;
using Microsoft.Graph;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace FIrstProductCRUD.Data
{
    public class ProductStorage : IServiceStorage
    {
        private static int _idIncrement;
        private static List<Product> allProducts = new List<Product>();
        public void AddProduct(Product product)
        {
            product.Id = ++_idIncrement;
            allProducts.Add(product);
        }

        public void RemoveProduct(int productId)
        {
            var result = allProducts.FirstOrDefault(e => e.Id == productId);
            if (result != null)
            {
                allProducts.Remove(result);
            }
        }
        public List<Product> GetProducts()
        {
            return new List<Product>(allProducts);
        }

        public Product GetByIdOrNull(int productId)
        {
            Product product = null;
            var result = allProducts.FirstOrDefault(e => e.Id == productId);
            if (result != null)
            {
                product = result;
            }

            return product;
        }

        public bool IsCodeExists(int code, int? id)
        {
            return allProducts.Where(p => p.Id != id).Any(c => c.Code == code);
        }

        public void Update(Product product)
        {
            var result = allProducts.FirstOrDefault(e => e.Id == product.Id);
            if (result != null)
            {
                result.Name = product.Name;
                result.Price = product.Price;
                result.Quantity = product.Quantity;
            }
        }

     
    }
}

