using AutoMapper;
using MicroservicesProject.Courses.Domain.Dto;
using MicroservicesProject.Courses.Service.Model;

namespace MicroservicesProject.Courses.Service.Mapping
{
	public class CourseProfile : Profile
	{
		public CourseProfile()
		{
			CreateMap<CourseDetailsDto, Course>();
			CreateMap<Course, CourseDetailsDto>();
			//TODO add logic here if both differ
		}
	}
}
