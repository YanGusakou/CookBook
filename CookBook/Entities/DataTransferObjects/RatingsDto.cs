﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
	public class RatingsDto
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int RecipeId { get; set; }
		public int Rating { get; set; }
	}
}
