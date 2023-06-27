using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
	public class UserForCreationDto
	{
		[Required(ErrorMessage = "Name is required")]
		[StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Email is required")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		public string Password { get; set; }

		public DateTime DateOfBirth { get; set; }
	}
}
