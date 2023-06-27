using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
	public interface IFavoriteRecipesRepository : IRepositoryBase<FavoriteRecipes>
	{
		IEnumerable<FavoriteRecipes> GetAllFavoriteRecipes();
		IEnumerable<FavoriteRecipes> GetFavoriteByUserId(int id);
		FavoriteRecipes GetFavoriteByUserIdAndRecipeId(int userId, int recipeId);
		void CreateFavoriteRecipe(FavoriteRecipes favoriteRecipe);
		void DeleteFavoriteRecipe(FavoriteRecipes favoriteRecipe);
	}
}
