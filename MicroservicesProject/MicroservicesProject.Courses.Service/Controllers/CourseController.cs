using System.Net;
using MicroservicesProject.Courses.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicesProject.Courses.Service.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CourseController : ControllerBase
	{
		private List<CourseDetailsDto> _coursesList = new();
		private readonly ILogger<CourseController> _logger;

		public CourseController(ILogger<CourseController> logger)
		{
			_logger = logger;
		}

		[HttpGet(Name = "GetAll")]
		[ProducesResponseType(typeof(IEnumerable<CourseDetailsDto>), (int) HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult<IEnumerable<CourseDetailsDto>> GetAll()
		{
			return Ok(_coursesList);
		}

		[HttpPut(Name = "AddCourse")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult AddCourse(CourseDetailsDto course)
		{
			_coursesList.Add(course);
			return Ok();
		}

		[HttpPost(Name = "UpdateCourse")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult UpdateCourse(CourseDetailsDto course)
		{
			var x = _coursesList.FirstOrDefault(x => x.Id == course.Id);
			if (x != null)
			{
				_coursesList.Remove(x);
				_coursesList.Add(course);
				return Ok();
			}

			return NotFound();
		}

		[HttpDelete(Name = "DeleteCourse")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult DeleteCourse(Guid id)
		{
			var x = _coursesList.FirstOrDefault(x => x.Id == id);
			if (x != null)
			{
				_coursesList.Remove(x);
				return Ok();
			}

			return NotFound();
		}
	}
}