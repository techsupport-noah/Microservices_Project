using AutoMapper;
using MicroservicesProject.Events.Domain.Dto;
using MicroservicesProject.Events.Service.Model;

namespace MicroservicesProject.Events.Service.Mapping
{
	public class EventProfile : Profile
	{
		public EventProfile()
		{
			CreateMap<EventDetailsDto, Event>();
			CreateMap<Event, EventDetailsDto>();
			//TODO add logic here if both differ
		}
	}
}
