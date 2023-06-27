using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
	public interface IRatingsRepository : IRepositoryBase<Ratings>
	{
		IEnumerable<Ratings> GetAllRatings();
		IEnumerable<Ratings> GetRatingsByRecipeId(int recipeId);
		Ratings GetRatingsByUserIdAndRecipeId(int userId, int recipeId);
		void CreateRating(Ratings rating);
		void DeleteRating(Ratings rating);
	}
}
