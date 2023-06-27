using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
	public interface ICommentsRepository : IRepositoryBase<Comments>
	{
		IEnumerable<Comments> GetAllComments();
		IEnumerable<Comments> GetCommentsByRecipeId(int recipeId);
		Comments GetCommentByUserIdAndRecipeId(int userId, int recipeId);
		void CreateComment(Comments comment);
		void DeleteComment(Comments comment);
	}
}
