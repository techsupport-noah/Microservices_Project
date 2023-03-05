using Microsoft.AspNetCore.Mvc;
using System.Net;
using MicroservicesProject.Students.Domain.Dto;

namespace MicroservicesProject.Students.Service.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class StudentController : ControllerBase
	{
		private List<StudentDetailsDto> _studentsList = new();

		private readonly ILogger<StudentController> _logger;

		public StudentController(ILogger<StudentController> logger)
		{
			_logger = logger;
		}

		[HttpGet(Name = "GetAll")] //doesn't need a template path because it is the only get without a specific path
		[ProducesResponseType(typeof(IEnumerable<StudentDetailsDto>), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult<IEnumerable<StudentDetailsDto>> GetAll()
		{
			return Ok(_studentsList);
		}

		[HttpGet("GetStudentsByCourseId/{id:guid}", Name = "GetStudentsByCourseId")]
		[ProducesResponseType(typeof(IEnumerable<StudentDetailsDto>), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult<IEnumerable<StudentDetailsDto>> GetStudentsByCourseId(Guid id)
		{
			var x = _studentsList.FindAll(x => x.CourseId == id);
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
		public ActionResult AddStudent(StudentDetailsDto student)
		{
			_studentsList.Add(student);
			return Ok();
		}

		[HttpPost(Name = "UpdateStudent")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult UpdateStudent(StudentDetailsDto student)
		{
			var x = _studentsList.FirstOrDefault(x => x.Id == student.Id);
			if (x != null)
			{
				_studentsList.Remove(x);
				_studentsList.Add(student);
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
			var x = _studentsList.FirstOrDefault(x => x.Id == id);
			if (x != null)
			{
				_studentsList.Remove(x);
				return Ok();
			}

			return NotFound();
		}
	}
}