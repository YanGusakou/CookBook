using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Controllers
{
	[Route("api/comments")]
	[ApiController]
	public class CommentsController : ControllerBase
	{
		private ILoggerManager _logger;
		private IRepositoryWrapper _repository;
		private IMapper _mapper;

		public CommentsController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
		{
			_logger = logger;
			_repository = repository;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetAllComments()
		{
			try
			{
				var comments = _repository.Comments.GetAllComments();

				_logger.LogInfo($"Returned all comments from database.");

				var commentsResult = _mapper.Map<IEnumerable<CommentsDto>>(comments);

				return Ok(commentsResult);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside GetAllComments action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpGet("{id}")]
		public IActionResult GetCommentsByRecipeId(int id)
		{
			try
			{
				var comments = _repository.Comments.GetCommentsByRecipeId(id);

				if (comments is null)
				{
					_logger.LogError($"Comments with recipe id: {id}, hasn't been found in db.");
					return NotFound();
				}
				else
				{
					_logger.LogInfo($"Returned comments with recipe id: {id}");
					var commentssResult = _mapper.Map<IEnumerable<CommentsDto>>(comments);
					return Ok(commentssResult);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside GetCommentsByRecipeId action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}

		//[HttpGet("{Userid}/{recipeId}")]
		//public IActionResult GetCommentByUserIdAndRecipeId(int userId, int recipeId)
		//{
		//	try
		//	{
		//		var comments = _repository.Comments.GetCommentByUserIdAndRecipeId(userId, recipeId);

		//		if (comments is null)
		//		{
		//			_logger.LogError($"Comments with user id: {userId} and recipeId: {recipeId}, hasn't been found in db.");
		//			return NotFound();
		//		}
		//		else
		//		{
		//			_logger.LogInfo($"Returned comments user id: {userId} and recipeId: {recipeId}");
		//			var commentssResult = _mapper.Map<CommentsDto>(comments);
		//			return Ok(commentssResult);
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		_logger.LogError($"Something went wrong inside GetCommentByUserIdAndRecipeId action: {ex.Message}");
		//		return StatusCode(500, "Internal server error");
		//	}
		//}

		[HttpPost]
		public IActionResult CreateComment([FromBody] CommentForCreatingDto comment)
		{
			try
			{
				if (comment is null)
				{
					_logger.LogError("Comment object sent from client is null.");
					return BadRequest("Comment object is null");
				}
				if (!ModelState.IsValid)
				{
					_logger.LogError("Invalid comment object sent from client.");
					return BadRequest("Invalid model object");
				}
				var commentEntity = _mapper.Map<Comments>(comment);
				_repository.Comments.CreateComment(commentEntity);
				_repository.Save();
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside CreateComment action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpDelete("{userId}/{recipeId}")]
		public IActionResult DeleteComment(int userId, int recipeId)
		{
			try
			{
				var comment = _repository.Comments.GetCommentByUserIdAndRecipeId(userId, recipeId);
				if (comment == null)
				{
					_logger.LogError($"Comment with user id: {userId} and recipe id: {recipeId}, hasn't been found in db.");
					return NotFound();
				}
				_repository.Comments.DeleteComment(comment);
				_repository.Save();
				return NoContent();
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside DeleteComment action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}
	}
}
