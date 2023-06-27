using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class RepositoryContext : DbContext
	{
		public RepositoryContext(DbContextOptions options)
		   : base(options)
		{
		}

		public DbSet<Categories> Users { get; set; }
		public DbSet<Categories> Categories { get; set; }
		public DbSet<Recipes> Recipes { get; set; }
		public DbSet<FavoriteRecipes> FavoriteRecipes { get; set; }
		public DbSet<Comments> Comments { get; set; }
	}
}
