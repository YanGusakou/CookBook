using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
	public interface IRepositoryWrapper
	{
		IUsersRepository Users { get; }
		ICategoriesRepository Categories { get; }
		IRecipesRepository Recipes { get; }
		IFavoriteRecipesRepository FavoriteRecipes { get; }
		ICommentsRepository Comments { get; }	
		IRatingsRepository Ratings { get; }
		void Save();
	}
}
