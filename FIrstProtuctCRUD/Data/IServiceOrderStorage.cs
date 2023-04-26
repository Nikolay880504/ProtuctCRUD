using FIrstProductCRUD.Models;

namespace FIrstProductCRUD.Data
{
    public interface IServiceOrderStorage
    {
        void AddOrder(int userId);
    }
}
