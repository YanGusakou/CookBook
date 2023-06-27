using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class CategoriesRepository : RepositoryBase<Categories>, ICategoriesRepository
	{
		public CategoriesRepository(RepositoryContext repositoryContext)
			: base(repositoryContext)
		{
		}

		public IEnumerable<Categories> GetAllCategories()
		{
			return FindAll()
				.OrderBy(cat => cat.Name)
				.ToList();
		}

		public Categories GetCategoryById(int id)
		{
			return FindByCondition(cat => cat.Id.Equals(id))
				.FirstOrDefault();
		}
	}
}
