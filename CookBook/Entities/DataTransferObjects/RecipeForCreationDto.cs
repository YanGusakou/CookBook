using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
	public class RecipeForCreationDto
	{
		[Required(ErrorMessage = "Title is required")]
		[StringLength(100, ErrorMessage = "Title can't be longer than 100 characters")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Ingredients are required")]
		public string Ingredients { get; set; }

		[Required(ErrorMessage = "Instructions are required")]
		public string Instructions { get; set; }

		[Required(ErrorMessage = "UserId are required")]
		public int UserId { get; set; }

		[Required(ErrorMessage = "CategoryId are required")]
		public int CategoryId { get; set; }
	}
}
