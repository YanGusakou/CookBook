using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
	[Table("ratings")]
	public class Ratings
	{
		public int Id { get; set; }

		[ForeignKey(nameof(Users))]
		public int UserId { get; set; }
		public Users User { get; set; }

		[ForeignKey(nameof(Recipes))]
		public int RecipeId { get; set; }
		public Recipes Recipe { get; set; }

		[Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
		public int Rating { get; set; }
	}
}
