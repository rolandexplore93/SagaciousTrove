using Models;

namespace Data.Repository.IRepository
{
    public interface IShoppingCartRespository : IRepository<ShoppingCart>
    {
        int IncrementCount(ShoppingCart shoppingCart, int count);
        int DecrementCount(ShoppingCart shoppingCart, int count);
    }
}
