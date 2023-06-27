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
	public class RecipesDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int CategoryId { get; set; }
	}
}
