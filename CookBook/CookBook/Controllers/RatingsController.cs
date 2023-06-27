using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Controllers
{
	[Route("api/ratings")]
	[ApiController]
	public class RatingsController : ControllerBase
	{
		private ILoggerManager _logger;
		private IRepositoryWrapper _repository;
		private IMapper _mapper;

		public RatingsController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
		{
			_logger = logger;
			_repository = repository;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetAllRatings()
		{
			try
			{
				var ratings = _repository.Ratings.GetAllRatings();

				_logger.LogInfo($"Returned all ratings from database.");

				var ratingsResult = _mapper.Map<IEnumerable<RatingsDto>>(ratings);

				return Ok(ratingsResult);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside GetAllRatings action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpGet("{id}")]
		public IActionResult GetRatingsByRecipeId(int id)
		{
			try
			{
				var ratings = _repository.Ratings.GetRatingsByRecipeId(id);

				if (ratings is null)
				{
					_logger.LogError($"Ratings with recipe id: {id}, hasn't been found in db.");
					return NotFound();
				}
				else
				{
					_logger.LogInfo($"Returned ratings with recipe id: {id}");
					var ratingsResult = _mapper.Map<IEnumerable<RatingsDto>>(ratings);
					return Ok(ratingsResult);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside GetRatingsByRecipeId action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpPost]
		public IActionResult CreateRating([FromBody] RatingsForCreatingDto rating)
		{
			try
			{
				if (rating is null)
				{
					_logger.LogError("Rating object sent from client is null.");
					return BadRequest("Rating object is null");
				}
				if (!ModelState.IsValid)
				{
					_logger.LogError("Invalid rating object sent from client.");
					return BadRequest("Invalid model object");
				}
				var ratingEntity = _mapper.Map<Ratings>(rating);
				_repository.Ratings.CreateRating(ratingEntity);
				_repository.Save();
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside CreateRating action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpDelete("{userid}/{recipeId}")]
		public IActionResult DeleteRating(int userId, int recipeId)
		{
			try
			{
				var rating = _repository.Ratings.GetRatingsByUserIdAndRecipeId(userId, recipeId);
				if (rating == null)
				{
					_logger.LogError($"Rating with user id: {userId} and recipe id: {recipeId}, hasn't been found in db.");
					return NotFound();
				}
				_repository.Ratings.DeleteRating(rating);
				_repository.Save();
				return NoContent();
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside DeleteRating action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}
	}
}
