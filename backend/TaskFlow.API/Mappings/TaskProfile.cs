using AutoMapper;
using TaskFlow.API.DTOs;
using TaskFlow.Domain.Entities;

namespace TaskFlow.API.Mappings
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            // DTO -> Entity
            CreateMap<TaskDto, TaskItem>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CompletedAt, opt => opt.Ignore());

            // Entity -> DTO
            CreateMap<TaskItem, TaskDto>();

            // Entity -> Entity
            CreateMap<TaskItem, TaskItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}