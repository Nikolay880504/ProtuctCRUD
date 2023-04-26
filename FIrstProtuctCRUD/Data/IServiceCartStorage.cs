using FIrstProductCRUD.Models;
using FIrstProductCRUD.Pages.UserPages;
using FIrstProtuctCRUD.Models;

namespace FIrstProductCRUD.Data
{
    public interface IServiceCartStorage
    {
        void AddCartProduct(CartProduct cartProduct);
        List<CartProduct> GetCartProductForUser(int id);
        void DeleteProductFromCart(int cartProductId);
    }
}
