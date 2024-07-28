using System;
using Models;

namespace Data.Repository.IRepository
{
	public interface ICompanyRepository : IRepository<Company>
	{
		void Update(Company obj);
	}
}

