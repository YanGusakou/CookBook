using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Entities.Models
{
	[Table("recipes")]
	public class Recipes
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Title is required")]
		[StringLength(100, ErrorMessage = "Title can't be longer than 100 characters")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Ingredients are required")]
		public string Ingredients { get; set; }

		[Required(ErrorMessage = "Instructions are required")]
		public string Instructions { get; set; }

		[ForeignKey(nameof(Users))]
		public int UserId { get; set; }
		public Users User { get; set; }

		[ForeignKey(nameof(Categories))]
		public int CategoryId { get; set; }
		public Categories Category { get; set; }



		public ICollection<FavoriteRecipes> FavoriteRecipes { get; set; }//надо ли?
		public ICollection<Comments> Comments { get; set; }
		public ICollection<Ratings> Ratings { get; set; }
		//[JsonIgnore]
	}
}
