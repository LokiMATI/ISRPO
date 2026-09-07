using AutoMapper;
using AutoMapper.EquivalencyExpression;
using WellnessMassageCenter.Backend.DAL.Models;
using WellnessMassageCenter.Backend.DTO.Categories;
using WellnessMassageCenter.Backend.DTO.Employees;
using WellnessMassageCenter.Backend.DTO.Positions;
using WellnessMassageCenter.Backend.DTO.Services;
using WellnessMassageCenter.Backend.DTO.Yclients;

namespace WellnessMassageCenter.Backend.Services.Mappers;

/// <summary>
/// Профиль маппинга
/// </summary>
public class AutoMapperProfile : Profile
{
    /// <summary>
    /// Конструктор профиля
    /// </summary>
    public AutoMapperProfile()
    {
        // CreateMap<Class, ClassDto>();

        #region YclientsDTO
        CreateMap<YclientsCategoryDto, Category>()
            .EqualityComparison((dto, entity) => dto.Id == entity.Id)
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<YclientsPositionDto, Position>()
            .EqualityComparison((dto, entity) => dto.Id == entity.Id);

        CreateMap<YclientsEmployeeDto, Employee>()
            .EqualityComparison((dto, entity) => dto.Id == entity.Id)
            .ForMember(dest => dest.IsHidden, opt => opt.MapFrom(src => src.Hidden == 1))
            .ForMember(dest => dest.PositionId, opt => opt.MapFrom(src => src.Position.Id))
            .ForMember(dest => dest.Position, opt => opt.Ignore())
            .ForMember(dest => dest.Services, opt => opt.Ignore());

        CreateMap<YclientsServiceDto, Service>()
            .EqualityComparison((dto, entity) => dto.Id == entity.Id)
            .ForMember(dest => dest.IsCanRegisteredOnline, opt => opt.MapFrom(src => src.ServiceType == 1))
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Employees, opt => opt.Ignore());

        CreateMap<YclientsServiceEmployeeDto, Employee>()
            .EqualityComparison((dto, entity) => dto.Id == entity.Id);

        CreateMap<YclientsServiceLinkDto, Service>()
            .EqualityComparison((dto, entity) => dto.ServiceId == entity.Id)
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ServiceId))
            .ForAllMembers(opt => opt.Ignore());
        #endregion

        #region PositionDTO
        CreateMap<PositionDto, Position>()
            .EqualityComparison((dto, entity) => dto.Id == entity.Id)
            .ReverseMap();
        #endregion

        #region EmployeeDto
        CreateMap<EmployeeDto, Employee>()
            .EqualityComparison((dto, entity) => dto.Id == entity.Id)
            .ReverseMap();
        #endregion

        #region CategoryDTO
        CreateMap<CategoryDto, Category>()
            .EqualityComparison((dto, entity) => dto.Id == entity.Id)
            .ReverseMap();
        #endregion

        #region ServiceDto
        CreateMap<ServiceDto, Service>()
            .EqualityComparison((dto, entity) => dto.Id == entity.Id)
            .ReverseMap();
        #endregion
    }
}
