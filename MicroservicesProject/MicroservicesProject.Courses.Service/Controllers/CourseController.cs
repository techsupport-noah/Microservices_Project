using System.Net;
using AutoMapper;
using FluentValidation;
using MicroservicesProject.Courses.Domain.Dto;
using MicroservicesProject.Courses.Service.DataAccess;
using MicroservicesProject.Courses.Service.Model;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicesProject.Courses.Service.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CourseController : ControllerBase
	{
		private readonly ILogger<CourseController> _logger;
		private readonly CourseDbContext _dbContext;
		private readonly IMapper _mapper;
		private readonly IValidator<CourseDetailsDto> _courseValidator;

		public CourseController(ILogger<CourseController> logger, CourseDbContext dbContext, IMapper mapper, IValidator<CourseDetailsDto> validator)
		{
			_dbContext = dbContext;
			_logger = logger;
			_mapper = mapper;
			_courseValidator = validator;
		}

		[HttpGet(Name = "GetAll")]
		[ProducesResponseType(typeof(IEnumerable<CourseDetailsDto>), (int) HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult<IEnumerable<CourseDetailsDto>> GetAll()
		{
			var courses = _dbContext.Courses.ToList();
			var returnList = _mapper.Map<List<CourseDetailsDto>>(courses);
			return Ok(returnList);
		}

		[HttpPut(Name = "AddCourse")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult AddCourse(CourseDetailsDto course)
		{
			var result = _courseValidator.Validate(course);
			if (!result.IsValid)
			{
				//couldn't validate incomming data
				return ValidationProblem();
			}

			var courseModel = _mapper.Map<Course>(course);
			_dbContext.Courses.Add(courseModel);
			if (_dbContext.SaveChanges() == 0)
			{
				//no data was written to the db --> error
				//possible constraints problem
				return Conflict();
			};
			return Ok();
		}

		[HttpPost(Name = "UpdateCourse")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult UpdateCourse(CourseDetailsDto course)
		{
			var result = _courseValidator.Validate(course);
			if (!result.IsValid)
			{
				//couldn't validate incomming data
				return ValidationProblem();
			}

			var courseModel = _mapper.Map<Course>(course);

			var existingCourse = _dbContext.Courses.FirstOrDefault(u => u.Id == courseModel.Id);
			if (existingCourse == null)
			{
				_logger.LogDebug("Tried updating course which doesn't exist.");
				return NotFound();
			}

			_dbContext.Entry(existingCourse).CurrentValues.SetValues(courseModel);

			_dbContext.SaveChanges();
			return Ok();
		}

		[HttpDelete(Name = "DeleteCourse")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult DeleteCourse(Guid id)
		{
			var existingCourse = _dbContext.Courses.FirstOrDefault(u => u.Id == id);
			if (existingCourse == null)
			{
				_logger.LogDebug("Element not found");
				return NotFound();
			}

			_dbContext.Courses.Remove(existingCourse);
			_dbContext.SaveChanges();
			return Ok();
		}
	}
}