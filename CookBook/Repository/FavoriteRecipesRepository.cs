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
	internal class FavoriteRecipesRepository : RepositoryBase<FavoriteRecipes>, IFavoriteRecipesRepository
	{
		public FavoriteRecipesRepository(RepositoryContext repositoryContext)
			: base(repositoryContext)
		{
		}

		public void CreateFavoriteRecipe(FavoriteRecipes favoriteRecipe) => Create(favoriteRecipe);

		public IEnumerable<FavoriteRecipes> GetAllFavoriteRecipes()
		{
			return FindAll()
				.ToList();
		}

		public void DeleteFavoriteRecipe(FavoriteRecipes favoriteRecipe) => Delete(favoriteRecipe);

		public IEnumerable<FavoriteRecipes> GetFavoriteByUserId(int id)
		{
			return FindByCondition(recipe => recipe.UserId.Equals(id))
				.ToList();
		}

		public FavoriteRecipes GetFavoriteByUserIdAndRecipeId(int userId, int recipeId)
		{
			return FindByCondition(recipe => recipe.UserId.Equals(userId) && recipe.RecipeId.Equals(recipeId))
				.FirstOrDefault();
				
		}
	}
}
