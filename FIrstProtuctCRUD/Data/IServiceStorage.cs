using FIrstProtuctCRUD.Models;
using Microsoft.AspNetCore.Components.Web;

namespace FIrstProductCRUD.Data
{
    public interface IServiceStorage
    {
        void AddProduct(Product product);
        void RemoveProduct(int productId);
        Product GetByIdOrNull(int productId);
        List<Product> GetProducts();
        void Update(Product product);

        bool IsCodeExists(int code, int? id);
    }
}
