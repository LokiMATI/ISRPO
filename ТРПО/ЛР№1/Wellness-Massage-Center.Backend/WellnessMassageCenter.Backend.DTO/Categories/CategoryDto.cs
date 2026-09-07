using WellnessMassageCenter.Backend.DTO.Services;

namespace WellnessMassageCenter.Backend.DTO.Categories;

/// <summary>
/// DTO категории
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="Title">Наименование</param>
/// <param name="Services">Услуги</param>
public record CategoryDto(
    int Id,
    string Title,
    ServiceDto[] Services
);
