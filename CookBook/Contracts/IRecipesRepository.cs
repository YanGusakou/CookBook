using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
	public interface IRecipesRepository : IRepositoryBase<Recipes>
	{
		IEnumerable<Recipes> GetAllRecipes();
		Recipes GetRecipeWithDetails(int id);
		IEnumerable<Recipes> GetRecipesByCategory(int id);
		void CreateRecipe(Recipes recipe);
		void DeleteRecipe(Recipes recipe);
	}
}
