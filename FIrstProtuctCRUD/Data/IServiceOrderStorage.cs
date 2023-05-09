using FIrstProductCRUD.Models;

namespace FIrstProductCRUD.Data
{
    public interface IServiceOrderStorage
    {
        Order  AddOrder(int userId);
    }
}
