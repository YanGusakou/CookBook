using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class UsersRepository : RepositoryBase<Users>, IUsersRepository
	{
		public UsersRepository(RepositoryContext repositoryContext) 
			: base(repositoryContext) 
		{
		}

		public void CreateUser(Users user) => Create(user);

		public void DeleteUser(Users user) => Delete(user);

		public Users GerUserWithRecipes(int id)
		{
			return FindByCondition(user => user.Id.Equals(id))
				.Include(recipe => recipe.Recipes)
				.FirstOrDefault();
		}

		public IEnumerable<Users> GetAllUsers()
		{
			return FindAll()
				.OrderBy(us => us.Name)
				.ToList();
		}

		public Users GetUserById(int id)
		{
			return FindByCondition(user => user.Id.Equals(id))
				.FirstOrDefault();
		}

		public Users GetUserByName(string name)
		{
			return FindByCondition(user => user.Name.Equals(name))
				.FirstOrDefault();
		}
	}
}