using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Controllers
{
	[Route("api/categories")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private ILoggerManager _logger;
		private IRepositoryWrapper _repository;
		private IMapper _mapper;

		public CategoriesController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
		{
			_logger = logger;
			_repository = repository;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetAllCategories()
		{
			try
			{
				var categories = _repository.Categories.GetAllCategories();

				_logger.LogInfo($"Returned all categories from database.");

				var categoriesResult = _mapper.Map<IEnumerable<CategoriesDto>>(categories);

				return Ok(categoriesResult);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside GetAllCategories action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpGet("{id}")]
		public IActionResult GetCategoryById(int id)
		{
			try
			{
				var categories = _repository.Categories.GetCategoryById(id);

				if (categories is null)
				{
					_logger.LogError($"category with id: {id}, hasn't been found in db.");
					return NotFound();
				}
				else
				{
					_logger.LogInfo($"Returned ingredient with id: {id}");
					var categoriesResult = _mapper.Map<CategoriesDto>(categories);
					return Ok(categoriesResult);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside GetCategoryById action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}
	}
}
