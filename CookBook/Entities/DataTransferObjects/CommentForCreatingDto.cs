using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
	public class CommentForCreatingDto
	{
		public int UserId { get; set; }
		public int RecipeId { get; set; }
		public string Content { get; set; }
	}
}
