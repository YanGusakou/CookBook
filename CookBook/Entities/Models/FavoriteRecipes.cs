using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
	[Table("favoriterecipes")]
	public class FavoriteRecipes
	{
		public int Id { get; set; }

		// Внешний ключ на пользователя
		[ForeignKey(nameof(Users))]
		public int UserId { get; set; }
		public Users User { get; set; }

		// Внешний ключ на рецепт
		[ForeignKey(nameof(Recipes))]
		public int RecipeId { get; set; }
		public Recipes Recipe { get; set; }
	}
}
