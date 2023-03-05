using Microsoft.AspNetCore.Mvc;
using System.Net;
using MicroservicesProject.Students.Domain.Dto;

namespace MicroservicesProject.Students.Service.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class StudentController : ControllerBase
	{
		private List<StudentDetails> studentTestdata = new List<StudentDetails>();

		private readonly ILogger<StudentController> _logger;

		public StudentController(ILogger<StudentController> logger)
		{
			_logger = logger;
		}

		[HttpGet(Name = "GetAll")]
		[ProducesResponseType(typeof(IEnumerable<StudentDetails>), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult<IEnumerable<StudentDetails>> GetAll()
		{
			return Ok(studentTestdata);
		}

		[HttpGet(Name = "GetStudentsByCourseId")]
		[ProducesResponseType(typeof(IEnumerable<StudentDetails>), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult<IEnumerable<StudentDetails>> GetStudentsByCourseId(Guid id)
		{
			var x = studentTestdata.FindAll(x => x.CourseId == id);
			if (x.Any())
			{
				return Ok(x);
			}
			else
			{
				return NotFound();
			}
		}
	}
}