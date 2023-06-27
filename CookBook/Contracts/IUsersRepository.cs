using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
	public interface IUsersRepository : IRepositoryBase<Users>
	{
		IEnumerable<Users> GetAllUsers();
		Users GetUserById(int id);
		Users GetUserByName(string name);
		Users GerUserWithRecipes(int id);
		void CreateUser(Users user);
		void DeleteUser(Users user);
	}
}
