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
	public class RepositoryWrapper : IRepositoryWrapper
	{
		private RepositoryContext _repoContext;
		private IUsersRepository _users;
		private	ICategoriesRepository _categories;
		private IRecipesRepository _recipes;
		private IFavoriteRecipesRepository _favoriteRecipes;
		private ICommentsRepository _comments;
		private IRatingsRepository _ratings;

		public IUsersRepository Users
		{
			get
			{
				if (_users == null)
				{
					_users = new UsersRepository(_repoContext);
				}
				return _users;
			}
		}

		public ICategoriesRepository Categories
		{
			get
			{
				if (_categories == null)
				{
					_categories = new CategoriesRepository(_repoContext);
				}
				return _categories;
			}
		}

		public IRecipesRepository Recipes
		{
			get
			{
				if (_recipes == null)
				{
					_recipes = new RecipesRepository(_repoContext);
				}
				return _recipes;
			}
		}

		public IFavoriteRecipesRepository FavoriteRecipes
		{
			get
			{
				if (_favoriteRecipes == null)
				{
					_favoriteRecipes = new FavoriteRecipesRepository(_repoContext);
				}
				return _favoriteRecipes;
			}
		}

		public ICommentsRepository Comments
		{
			get
			{
				if (_comments == null)
				{
					_comments = new CommentsRepository(_repoContext);
				}
				return _comments;
			}
		}

		public IRatingsRepository Ratings
		{
			get
			{
				if (_ratings == null)
				{
					_ratings = new RatingsRepository(_repoContext);
				}
				return _ratings;
			}
		}

		public RepositoryWrapper(RepositoryContext repositoryContext)
		{
			_repoContext = repositoryContext;
		}

		public void Save()
		{
			_repoContext.SaveChanges();
		}
	}
}
