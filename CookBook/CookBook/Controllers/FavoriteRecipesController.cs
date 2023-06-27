using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Controllers
{
	[Route("api/favorite")]
	[ApiController]
	public class FavoriteRecipesController : ControllerBase
	{
		private ILoggerManager _logger;
		private IRepositoryWrapper _repository;
		private IMapper _mapper;

		public FavoriteRecipesController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
		{
			_logger = logger;
			_repository = repository;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetAllFavoriteRecipes()
		{
			try
			{
				var recipes = _repository.FavoriteRecipes.GetAllFavoriteRecipes();

				_logger.LogInfo($"Returned all Favorite Recipes from database.");

				var recipesResult = _mapper.Map<IEnumerable<FavoriteRecipesDto>>(recipes);

				return Ok(recipesResult);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside GetAllFavoriteRecipes action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpGet("{id}")]
		public IActionResult GetFavoriteByUserId(int id)
		{
			try
			{
				var recipes = _repository.FavoriteRecipes.GetFavoriteByUserId(id);

				if (recipes is null)
				{
					_logger.LogError($"Favorite Recipes with user id: {id}, hasn't been found in db.");
					return NotFound();
				}
				else
				{
					_logger.LogInfo($"Returned Favorite Recipes with id: {id}");
					var recipesResult = _mapper.Map<IEnumerable<FavoriteRecipesDto>>(recipes);
					return Ok(recipesResult);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside GetFavoriteByUserId action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpPost]
		public IActionResult CreateFavoriteRecipe([FromBody] FavoriteRecipeForCreatingDto recipe)
		{
			try
			{
				if (recipe is null)
				{
					_logger.LogError("Recipe object sent from client is null.");
					return BadRequest("Recipe object is null");
				}
				if (!ModelState.IsValid)
				{
					_logger.LogError("Invalid recipe object sent from client.");
					return BadRequest("Invalid model object");
				}
				var recipeEntity = _mapper.Map<FavoriteRecipes>(recipe);
				_repository.FavoriteRecipes.CreateFavoriteRecipe(recipeEntity);
				_repository.Save();
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside CreateUser action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpDelete("{userid}/{recipeId}")]
		public IActionResult DeleteRecipe(int userId, int recipeId)
		{
			try
			{
				var favoriteRecipe = _repository.FavoriteRecipes.GetFavoriteByUserIdAndRecipeId(userId, recipeId);
				if (favoriteRecipe == null)
				{
					_logger.LogError($"Favorite Recipe with user id: {userId} and recipe id: {recipeId}, hasn't been found in db.");
					return NotFound();
				}
				_repository.FavoriteRecipes.DeleteFavoriteRecipe(favoriteRecipe);
				_repository.Save();
				return NoContent();
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside DeleteRecipe action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}
	}
}
