using System.Net;
using MicroservicesProject.Courses.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicesProject.Courses.Service.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CourseController : ControllerBase
	{
		//Testdata //TODO remove
		private static List<CourseDetails> courseTestdata = new List<CourseDetails>()
		{
			new CourseDetails
			{
				Id = Guid.NewGuid(),
				Name = "Test 1"
			},
			new CourseDetails
			{
				Id = Guid.NewGuid(),
				Name = "Test 2"
			},
			new CourseDetails
			{
				Id = Guid.NewGuid(),
				Name = "Test 3"
			}
		};


		private readonly ILogger<CourseController> _logger;

		public CourseController(ILogger<CourseController> logger)
		{
			_logger = logger;
		}

		[HttpGet(Name = "GetAll")]
		[ProducesResponseType(typeof(IEnumerable<CourseDetails>), (int) HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult<IEnumerable<CourseDetails>> GetAll()
		{
			return Ok(courseTestdata);
		}
	}
}