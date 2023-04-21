using AutoMapper;
using BysinessServices.Models;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BysinessServices
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile() 
        {
            CreateMap<Resource, ResourceModel>()
                .ForMember(dest => dest.ResourceTypeName, opt => opt.MapFrom(src => src.ResourceType.TypeName))
                .ForMember(dest => dest.Schedules, opt => opt.MapFrom(src => src.Schedules))
                .ForMember(dest => dest.Requests, opt => opt.MapFrom(src => src.Requests));
            CreateMap<ResourceModel, Resource>()
                .ForMember(dest => dest.ResourceType, opt => opt.Ignore())
                .ForMember(dest => dest.Schedules, opt => opt.Ignore())
                .ForMember(dest => dest.Requests, opt => opt.Ignore());

            CreateMap<Schedule, ScheduleModel>()
                .ForMember(dest => dest.ResourceName, opt => opt.MapFrom(src => src.Resource.Name));
            CreateMap<ScheduleModel, Schedule>();

            CreateMap<Request, RequestModel>()
                .ForMember(dest => dest.ResourceName, opt => opt.MapFrom(src => src.Resource.Name));
            CreateMap<RequestModel, Request>();

            CreateMap<ResourceType, ResourceTypeModel>()
                .ForMember(dest => dest.Resources, opt => opt.MapFrom(src => src.Resources));
            CreateMap<ResourceTypeModel, ResourceType>()
                .ForMember(dest => dest.Resources, opt => opt.Ignore());
        }
    }
}
