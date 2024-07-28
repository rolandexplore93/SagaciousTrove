using Data.Repository.IRepository;
using Models;

namespace Data.Repository
{
    public class ShoppingCartRespository : Repository<ShoppingCart>, IShoppingCartRespository
    {
        private readonly ApplicationDbContext _db;

        public ShoppingCartRespository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public int DecrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count -= count;
            return shoppingCart.Count;
        }

        public int IncrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count += count;
            return shoppingCart.Count;
        }

        //public void Update(ShoppingCart obj)
        //{
        //    _db.ShoppingCarts.Update(obj);
        //}
    }
}