using Microsoft.AspNetCore.Mvc;
using System.Net;
using MicroservicesProject.Students.Domain.Dto;

namespace MicroservicesProject.Students.Service.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class StudentController : ControllerBase
	{
		private List<StudentDetails> studentTestdata = new();

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

		[HttpGet("GetStudentsByCourseId/{id:guid}", Name = "GetStudentsByCourseId")]
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

		[HttpPut(Name = "AddStudent")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult AddStudent(StudentDetails student)
		{
			studentTestdata.Add(student);
			return Ok();
		}

		[HttpPost(Name = "UpdateStudent")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult UpdateStudent(StudentDetails student)
		{
			var x = studentTestdata.FirstOrDefault(x => x.Id == student.Id);
			if (x != null)
			{
				studentTestdata.Remove(x);
				studentTestdata.Add(student);
				return Ok();
			}

			return NotFound();
		}

		[HttpDelete(Name = "DeleteStudent")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult DeleteStudent(Guid id)
		{
			var x = studentTestdata.FirstOrDefault(x => x.Id == id);
			if (x != null)
			{
				studentTestdata.Remove(x);
				return Ok();
			}

			return NotFound();
		}
	}
}