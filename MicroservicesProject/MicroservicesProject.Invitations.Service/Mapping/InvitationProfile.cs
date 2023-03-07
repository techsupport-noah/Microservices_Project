using AutoMapper;
using MicroservicesProject.Invitations.Domain.Dto;
using MicroservicesProject.Invitations.Service.Model;

namespace MicroservicesProject.Invitations.Service.Mapping
{
	public class InvitationProfile : Profile
	{
		public InvitationProfile()
		{
			CreateMap<InvitationDetailsDto, Invitation>();
			CreateMap<Invitation, InvitationDetailsDto>();
			//TODO add logic here if both differ
		}
	}
}
