using AutoMapper;
using MicroservicesProject.Students.Domain.Dto;
using MicroservicesProject.Students.Service.Model;

namespace MicroservicesProject.Students.Service.Mapping
{
	public class StudentProfile : Profile
	{
		public StudentProfile()
		{
			CreateMap<StudentDetailsDto, Student>();
			CreateMap<Student, StudentDetailsDto>();
			//TODO add logic here if both differ
		}
	}
}
