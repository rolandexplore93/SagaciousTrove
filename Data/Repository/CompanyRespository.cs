using System;
using Data.Repository.IRepository;
using Models;

namespace Data.Repository
{
	public class CompanyRespository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRespository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

        public void Update(Company obj)
        {
            _db.Companies.Update(obj);
        }
    }
}

