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
	internal class RatingsRepository : RepositoryBase<Ratings>, IRatingsRepository
	{
		public RatingsRepository(RepositoryContext repositoryContext)
			: base(repositoryContext)
		{
		}

		public void CreateRating(Ratings rating) => Create(rating);

		public void DeleteRating(Ratings rating) => Delete(rating);

		public IEnumerable<Ratings> GetAllRatings()
		{
			return FindAll()
				.ToList();
		}

		public IEnumerable<Ratings> GetRatingsByRecipeId(int recipeId)
		{
			return FindByCondition(rating => rating.RecipeId.Equals(recipeId))
				.ToList();
		}

		public Ratings GetRatingsByUserIdAndRecipeId(int userId, int recipeId)
		{
			return FindByCondition(rating => rating.UserId.Equals(userId) && rating.RecipeId.Equals(recipeId))
				.FirstOrDefault();
		}
	}
}
