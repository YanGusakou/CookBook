using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class RecipesRepository : RepositoryBase<Recipes>, IRecipesRepository
	{
		public RecipesRepository(RepositoryContext repositoryContext)
			: base(repositoryContext)
		{
		}

		public void CreateRecipe(Recipes recipe) => Create(recipe);

		public void DeleteRecipe(Recipes recipe) => Delete(recipe);

		public IEnumerable<Recipes> GetAllRecipes()
		{
			return FindAll()
				.OrderBy(re => re.Title)
				.ToList();
		}

		public IEnumerable<Recipes> GetRecipesByCategory(int id)
		{
			return FindByCondition(recipe => recipe.CategoryId.Equals(id))
				.OrderBy(re => re.Title)
				.ToList();
		}

		public Recipes GetRecipeWithDetails(int id)
		{
			return FindByCondition(recipe => recipe.Id.Equals(id))
				.FirstOrDefault();
		}
	}
}
