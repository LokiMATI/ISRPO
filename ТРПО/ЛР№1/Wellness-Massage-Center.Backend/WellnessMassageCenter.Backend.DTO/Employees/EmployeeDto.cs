using WellnessMassageCenter.Backend.DTO.Positions;

namespace WellnessMassageCenter.Backend.DTO.Employees;

/// <summary>
/// DTO работника
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="Name">Имя</param>
/// <param name="Specialization">Специализация</param>
/// <param name="Position">Должность</param>
/// <param name="Rating">Рейтинг</param>
/// <param name="Avatar">Путь к файлу аватарки сотрудника</param>
/// <param name="AvatarBig">Путь к файлу аватарки сотрудника в более высоком разрешении</param>
/// <param name="Information">Дополнительная информация о сотруднике (HTML формат)</param>
/// <param name="IsHidden">Скрыт ли от онлайн записи</param>
public record EmployeeDto(
    int Id,
    string Name,
    string Specialization,
    PositionDto Position,
    decimal Rating,
    string Avatar,
    string AvatarBig,
    string Information,
    bool IsHidden
);
