using AutoMapper;
using MicroservicesProject.Users.Domain.Dto;
using MicroservicesProject.Users.Service.Model;

namespace MicroservicesProject.Users.Service.Mapping
{
	//defines the way the mapper should map the dto object to the normal object
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<UserDetailsDto, User>();
			CreateMap<User, UserDetailsDto>();
			//TODO add logic here if both differ
		}
	}
}
