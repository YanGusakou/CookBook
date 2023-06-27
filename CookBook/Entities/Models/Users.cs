using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
	[Table("users")]
	public class Users
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Name is required")]
		[StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Email is required")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		public string Password { get; set; }

		public DateTime DateOfBirth { get; set; }



		public ICollection<Recipes> Recipes { get; set; } // чтобы видеть список созданных рецептов?
		public ICollection<FavoriteRecipes> FavoriteRecipes { get; set; } 
		public ICollection<Comments> Comments { get; set; } // чтобы витеть список оставленных комментариев?
		public ICollection<Ratings> Ratings { get; set; } // рейтинг рецепта
	}
}
