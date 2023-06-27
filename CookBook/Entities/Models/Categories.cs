using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
	[Table("categories")]
	public class Categories
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Name is required")]
		[StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
		public string Name { get; set; }
	}
}
