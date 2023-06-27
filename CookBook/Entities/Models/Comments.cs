using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
	[Table("comments")]
	public class Comments
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Content is required")]
		public string Content { get; set; }

		[ForeignKey(nameof(Users))]
		public int UserId { get; set; }
		public Users User { get; set; }

		[ForeignKey(nameof(Recipes))]
		public int RecipeId { get; set; }
		public Recipes Recipe { get; set; }
	}
}
