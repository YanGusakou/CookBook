using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Controllers
{
	[Route("api/users")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private ILoggerManager _logger;
		private IRepositoryWrapper _repository;
		private IMapper _mapper;

		public UsersController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
		{
			_logger = logger;
			_repository = repository;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetAllUsers()
		{
			try
			{
				var users = _repository.Users.GetAllUsers();

				_logger.LogInfo($"Returned all owners from database.");

				var usersResult = _mapper.Map<IEnumerable<UsersDto>>(users);

				return Ok(usersResult);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside GetAllUsers action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}
		[HttpGet("{id}", Name = "UserById")]
		public IActionResult GetUserById(int id)
		{
			try
			{
				var users = _repository.Users.GetUserById(id);

				if (users is null)
				{
					_logger.LogError($"User with id: {id}, hasn't been found in db.");
					return NotFound();
				}
				else
				{
					_logger.LogInfo($"Returned user with id: {id}");
					var usersResult = _mapper.Map<UsersDto>(users);
					return Ok(usersResult);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside GetUserById action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpGet("name/{name}")]
		public IActionResult GetUserByName(string name)
		{
			try
			{
				var users = _repository.Users.GetUserByName(name);

				if (users is null)
				{
					_logger.LogError($"User with name: {name}, hasn't been found in db.");
					return NotFound();
				}
				else
				{
					_logger.LogInfo($"Returned user with name: {name}");
					var usersResult = _mapper.Map<UsersDto>(users);
					return Ok(usersResult);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside GetUserByName action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpGet("{id}/recipes")]
		public IActionResult GerUserWithRecipes(int id)
		{
			try
			{
				var users = _repository.Users.GerUserWithRecipes(id);

				if (users is null)
				{
					_logger.LogError($"Recipes with UserId: {id}, hasn't been found in db.");
					return NotFound();
				}
				else
				{
					_logger.LogInfo($"Returned recipes with UserId: {id}");
					var usersResult = _mapper.Map<UserWithRecipesDto>(users);
					return Ok(usersResult);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside GerUserWithRecipes action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpPost]
		public IActionResult CreateUser([FromBody] UserForCreationDto user)
		{
			try
			{
				if (user is null)
				{
					_logger.LogError("User object sent from client is null.");
					return BadRequest("User object is null");
				}
				if (!ModelState.IsValid)
				{
					_logger.LogError("Invalid user object sent from client.");
					return BadRequest("Invalid model object");
				}
				var userEntity = _mapper.Map<Users>(user);
				_repository.Users.CreateUser(userEntity);
				_repository.Save();
				var createdUser = _mapper.Map<UsersDto>(userEntity);
				return CreatedAtRoute("UserById", new { id = createdUser.Id }, createdUser);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside CreateUser action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteUser(int id)
		{
			try
			{
				var user = _repository.Users.GetUserById(id);
				if (user == null)
				{
					_logger.LogError($"User with id: {id}, hasn't been found in db.");
					return NotFound();
				}
				_repository.Users.DeleteUser(user);
				_repository.Save();
				return NoContent();
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside DeleteUser action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}
	}
}
