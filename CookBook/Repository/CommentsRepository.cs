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
	internal class CommentsRepository : RepositoryBase<Comments>, ICommentsRepository
	{
		public CommentsRepository(RepositoryContext repositoryContext)
			: base(repositoryContext)
		{
		}

		public IEnumerable<Comments> GetCommentsByRecipeId(int recipeId)
		{
			return FindByCondition(comment => comment.RecipeId.Equals(recipeId))
				.ToList();
		}

		public Comments GetCommentByUserIdAndRecipeId(int userId, int recipeId)
		{
			return FindByCondition(comment => comment.UserId.Equals(userId) && comment.RecipeId.Equals(recipeId))
				.FirstOrDefault();
		}

		public void CreateComment(Comments comment) => Create(comment);

		public void DeleteComment(Comments comment) => Delete(comment);

		public IEnumerable<Comments> GetAllComments()
		{
			return FindAll()
				.ToList();
		}
	}
}
