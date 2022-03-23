using AutoMapper;
using MedicalAssistant.SurveyCovid.Contracts.Dto;
using MedicalAssistant.SurveyCovid.Entitis;
using MedicalAssistant.SurveyCovid.Services.AccountService;
using System;

namespace MedicalAssistant.SurveyCovid.Mapping
{
    public class SurveyCovidMapper : Profile
    {
        public SurveyCovidMapper()
        {
            CreateMap<RegisterUserDto, User>()
                .ForMember(u => u.HashPassword, opt => opt.MapFrom(src => src.Password))
                .ForMember(u => u.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(u => u.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<UserLoginResult, UserLoginResultDto>();
            CreateMap<Survey, SurveyDto>();
            CreateMap<SurveyDto, Survey>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<DepartmentDto, Department>();
            CreateMap<Department, DepartmentDto>();
        }
    }
}
