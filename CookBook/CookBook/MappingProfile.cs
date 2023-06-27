using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Repository;
using System.Runtime.InteropServices;

namespace CookBook
{
	public class MappingProfile : Profile
	{
		public MappingProfile() 
		{
			CreateMap<Users, UsersDto>();
			CreateMap<Recipes, RecipesDto>();
			CreateMap<Recipes, RecipeWithDetailsDto>();
			CreateMap<Users, UserWithRecipesDto>();
			CreateMap<Categories, CategoriesDto>();
			CreateMap<UserForCreationDto, Users>();
			CreateMap<RecipeForCreationDto, Recipes>();
			CreateMap<FavoriteRecipes, FavoriteRecipesDto>();
			CreateMap<FavoriteRecipeForCreatingDto, FavoriteRecipes>();
			CreateMap<Comments, CommentsDto>();
			CreateMap<CommentForCreatingDto, Comments>();
			CreateMap<Ratings, RatingsDto>();
			CreateMap<RatingsForCreatingDto, Ratings>();

			//CreateMap<FavoriteRecipesDto, FavoriteRecipes>();
			//CreateMap<Users, RecipeWithDetailsDto>();
			//CreateMap<Categories, RecipeWithDetailsDto>();
			//CreateMap<RecipeIngredients, RecipeWithDetailsDto>();IngredientsDto
		}

	}
}
