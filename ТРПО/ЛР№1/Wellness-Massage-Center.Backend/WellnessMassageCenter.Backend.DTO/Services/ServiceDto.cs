using WellnessMassageCenter.Backend.DTO.Employees;

namespace WellnessMassageCenter.Backend.DTO.Services;

/// <summary>
/// DTO услуги
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="CategoryId">Идентификатор категории</param>
/// <param name="Title">Наименование</param>
/// <param name="Comment">Комментарий</param>
/// <param name="Duration">Продолжительность</param>
/// <param name="IsCanRegisteredOnline">Возможна ли регистрация онлайн</param>
/// <param name="Employees">Сотрудники</param>
public record ServiceDto(
    int Id,
    int CategoryId,
    string Title,
    string Comment,
    short Duration,
    bool IsCanRegisteredOnline,
    EmployeeDto[] Employees
);
