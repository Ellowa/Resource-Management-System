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
            //AutoMapper for Entities.Resource <--> Models.RecourceModel
            CreateMap<Resource, ResourceModel>()
                .ForMember(dest => dest.ResourceTypeName, opt => opt.MapFrom(src => src.ResourceType.TypeName))
                .ForMember(dest => dest.Schedules, opt => opt.MapFrom(src => src.Schedules))
                .ForMember(dest => dest.Requests, opt => opt.MapFrom(src => src.Requests));
            CreateMap<ResourceModel, Resource>()
                .ForMember(dest => dest.ResourceType, opt => opt.Ignore())
                .ForMember(dest => dest.Schedules, opt => opt.Ignore())
                .ForMember(dest => dest.Requests, opt => opt.Ignore());

            //AutoMapper for Entities.Schedule <--> Models.ScheduleModel
            CreateMap<Schedule, ScheduleModel>()
                .ForMember(dest => dest.ResourceName, opt => opt.MapFrom(src => src.Resource.Name));
            CreateMap<ScheduleModel, Schedule>();

            //AutoMapper for Entities.Request <--> Models.RequestModel
            CreateMap<Request, RequestModel>()
                .ForMember(dest => dest.ResourceName, opt => opt.MapFrom(src => src.Resource.Name));
            CreateMap<RequestModel, Request>();

            //AutoMapper for Entities.ResourceType <--> Models.ResourceTypeModel
            CreateMap<ResourceType, ResourceTypeModel>()
                .ForMember(dest => dest.Resources, opt => opt.MapFrom(src => src.Resources));
            CreateMap<ResourceTypeModel, ResourceType>()
                .ForMember(dest => dest.Resources, opt => opt.Ignore());

            //AutoMapper for Entities.RequestModel <--> Models.Schedule
            //  as the data often transfer from Request to Schedule
            //  without changing any fields
            CreateMap<RequestModel, Schedule>();

            //AutoMapper for Entities.AditionalRole <--> Models.RoleModel
            CreateMap<AdditionalRole, RoleModel>();
            CreateMap<RoleModel, AdditionalRole>();

            //AutoMapper for Entities.User --> Models.UserModel
            //ATTENTION! Backward automapping is not allowd!!!
            CreateMap<User, UserProtectedModel>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.Requests, opt => opt.MapFrom(src => src.Requests))
                .ForMember(dest => dest.Schedules, opt => opt.MapFrom(src => src.Schedules));

            //AutoMapper for Models.UserWithAuthInfoModel --> Models.UserModel
            //ATTENTION! Backward automapping is not allowd!!!
            CreateMap<UserWithAuthInfoModel, UserProtectedModel>();

            //AutoMapper for Entities.User <--> Models.UserWithAuthInfoModel
            CreateMap<User, UserWithAuthInfoModel>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.Requests, opt => opt.MapFrom(src => src.Requests))
                .ForMember(dest => dest.Schedules, opt => opt.MapFrom(src => src.Schedules));
            CreateMap<UserWithAuthInfoModel, User>()
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.Requests, opt => opt.Ignore())
                .ForMember(dest => dest.Schedules, opt => opt.Ignore());

            CreateMap<UserProtectedModel, UserUnsafeModel>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
        }
    }
}
