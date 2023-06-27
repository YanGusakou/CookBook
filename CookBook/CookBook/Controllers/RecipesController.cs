using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Controllers
{
	[Route("api/recipes")]
	[ApiController]
	public class RecipesController : ControllerBase
	{
		private ILoggerManager _logger;
		private IRepositoryWrapper _repository;
		private IMapper _mapper;

		public RecipesController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
		{
			_logger = logger;
			_repository = repository;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetAllRecipes()
		{
			try
			{
				var recipes = _repository.Recipes.GetAllRecipes();

				_logger.LogInfo($"Returned all recipes from database.");

				var recipesResult = _mapper.Map<IEnumerable<RecipesDto>>(recipes);

				return Ok(recipesResult);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside GetAllRecipes action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpGet("{id}", Name = "RecipeById")]
		public IActionResult GetRecipeWithDetails(int id)
		{
			try
			{
				var recipes = _repository.Recipes.GetRecipeWithDetails(id);

				if (recipes is null)
				{
					_logger.LogError($"Recipe with id: {id}, hasn't been found in db.");
					return NotFound();
				}
				else
				{
					_logger.LogInfo($"Returned recipe with id: {id}");
					var recipesResult = _mapper.Map<RecipeWithDetailsDto>(recipes);
					return Ok(recipesResult);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside GetRecipeWithDetails action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpGet("{id}/category")]
		public IActionResult GetRecipesByCategory(int id)
		{
			try
			{
				var recipes = _repository.Recipes.GetRecipesByCategory(id);

				if (recipes is null)
				{
					_logger.LogError($"Recipe with category id: {id}, hasn't been found in db.");
					return NotFound();
				}
				else
				{
					_logger.LogInfo($"Returned recipe with category id: {id}");
					var recipesResult = _mapper.Map<IEnumerable<RecipesDto>>(recipes);
					return Ok(recipesResult);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside GetRecipesByCategory action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpPost]
		public IActionResult CreateRecipe([FromBody] RecipeForCreationDto recipe)
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
				var recipeEntity = _mapper.Map<Recipes>(recipe);
				_repository.Recipes.CreateRecipe(recipeEntity);
				_repository.Save();
				var createdRecipe = _mapper.Map<RecipeWithDetailsDto>(recipeEntity);
				return CreatedAtRoute("UserById", new { id = createdRecipe.Id }, createdRecipe);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside CreateUser action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteRecipe(int id)
		{
			try
			{
				var recipe = _repository.Recipes.GetRecipeWithDetails(id);
				if (recipe == null)
				{
					_logger.LogError($"Recipe with id: {id}, hasn't been found in db.");
					return NotFound();
				}
				_repository.Recipes.DeleteRecipe(recipe);
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
