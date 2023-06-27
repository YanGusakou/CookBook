using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
	public class RecipeWithDetailsDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Ingredients { get; set; }
		public string Instructions { get; set; }
		public int UserId { get; set; }
		//public Users User { get; set; }
		public int CategoryId { get; set; }
		//public Categories Category { get; set; }
	}
}
