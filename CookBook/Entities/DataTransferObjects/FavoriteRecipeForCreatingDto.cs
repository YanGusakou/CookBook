using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
	public class FavoriteRecipeForCreatingDto
	{
		public int UserId { get; set; }
		public int RecipeId { get; set; }
	}
}
