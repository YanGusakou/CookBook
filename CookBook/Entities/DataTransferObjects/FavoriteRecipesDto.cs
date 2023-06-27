using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
	public class FavoriteRecipesDto
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int RecipeId { get; set; }
	}
}
