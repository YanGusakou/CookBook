using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
	public class RatingsForCreatingDto
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int RecipeId { get; set; }
		public int Rating { get; set; }
	}
}
