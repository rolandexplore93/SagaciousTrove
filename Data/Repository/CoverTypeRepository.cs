using Data.Repository.IRepository;
using Models;

namespace Data.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private ApplicationDbContext _db;

        public CoverTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CoverType obj)
        {
            _db.CoverTypes.Update(obj);
        }
    }
}




//Repository<Category>, ICategoryRepository
//private ApplicationDbContext _db;

//public CategoryRepository(ApplicationDbContext db) : base(db)
//        {
//    _db = db;
//}
//public void Save()
//{
//    _db.SaveChanges();
//}

//public void Update(Category obj)
//{
//    _db.Categories.Update(obj);
//}
