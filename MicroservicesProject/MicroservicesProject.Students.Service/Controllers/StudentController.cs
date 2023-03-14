using Microsoft.AspNetCore.Mvc;
using System.Net;
using MicroservicesProject.Students.Domain.Dto;
using AutoMapper;
using FluentValidation;
using MicroservicesProject.Students.Service.DataAccess;
using MicroservicesProject.Students.Service.Model;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace MicroservicesProject.Students.Service.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class StudentController : ControllerBase
	{
		private readonly ILogger<StudentController> _logger;
		private readonly StudentDbContext _dbContext;
		private readonly IMapper _mapper;
		private readonly IValidator<StudentDetailsDto> _studentValidator;

		public StudentController(ILogger<StudentController> logger, StudentDbContext dbContext, IMapper mapper, IValidator<StudentDetailsDto> validator)
		{
			_dbContext = dbContext;
			_logger = logger;
			_mapper = mapper;
			_studentValidator = validator;
		}

		[HttpGet(Name = "GetAll")] //doesn't need a template path because it is the only get without a specific path
		[ProducesResponseType(typeof(IEnumerable<StudentDetailsDto>), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
		[ProducesResponseType((int)HttpStatusCode.Forbidden)]
		[Authorize(Roles = "Administrator")]
		public ActionResult<IEnumerable<StudentDetailsDto>> GetAll()
		{
			var students = _dbContext.Students.ToList();
			var returnList = _mapper.Map<List<StudentDetailsDto>>(students);
			return Ok(returnList);
		}

		[HttpGet("GetStudentsByCourseId/{id:guid}", Name = "GetStudentsByCourseId")]
		[ProducesResponseType(typeof(IEnumerable<StudentDetailsDto>), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
		[ProducesResponseType((int)HttpStatusCode.Forbidden)]
		[Authorize(Roles = "Administrator")]
		public ActionResult<IEnumerable<StudentDetailsDto>> GetStudentsByCourseId(Guid id)
		{
			var student = _dbContext.Students.FirstOrDefault(s => s.CourseId == id);
			var returnValue = _mapper.Map<StudentDetailsDto>(student);
			return Ok(returnValue);
		}

		[HttpPut(Name = "AddStudent")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
		[ProducesResponseType((int)HttpStatusCode.Forbidden)]
		[Authorize(Roles = "Administrator")]
		public ActionResult AddStudent(StudentDetailsDto student)
		{
			var result = _studentValidator.Validate(student);
			if (!result.IsValid)
			{
				//couldn't validate incomming data
				return ValidationProblem();
			}

			var studentModel = _mapper.Map<Student>(student);
			_dbContext.Students.Add(studentModel);
			if (_dbContext.SaveChanges() == 0)
			{
				//no data was written to the db --> error
				//possible constraints problem
				return Conflict();
			};
			return Ok();
		}

		[HttpPost(Name = "UpdateStudent")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
		[ProducesResponseType((int)HttpStatusCode.Forbidden)]
		[Authorize(Roles = "Administrator")]
		public ActionResult UpdateStudent(StudentDetailsDto student)
		{
			var result = _studentValidator.Validate(student);
			if (!result.IsValid)
			{
				//couldn't validate incomming data
				return ValidationProblem();
			}

			var studentModel = _mapper.Map<Student>(student);

			var existingStudent = _dbContext.Students.FirstOrDefault(u => u.Id == studentModel.Id);
			if (existingStudent == null)
			{
				_logger.LogDebug("Tried updating student which doesn't exist.");
				return NotFound();
			}

			_dbContext.Entry(existingStudent).CurrentValues.SetValues(studentModel);

			_dbContext.SaveChanges();
			return Ok();
		}

		[HttpDelete(Name = "DeleteStudent")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
		[ProducesResponseType((int)HttpStatusCode.Forbidden)]
		[Authorize(Roles = "Administrator")]
		public ActionResult DeleteStudent(Guid id)
		{
			var existingStudent = _dbContext.Students.FirstOrDefault(u => u.Id == id);
			if (existingStudent == null)
			{
				_logger.LogDebug("Element not found");
				return NotFound();
			}

			_dbContext.Students.Remove(existingStudent);
			_dbContext.SaveChanges();
			return Ok();
		}
	}
}