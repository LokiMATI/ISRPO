using System.Text.Json.Serialization;

namespace WellnessMassageCenter.Backend.DTO.Yclients;

/// <summary>
/// DTO сотрудника Yclients
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="ApiId">API идентификатор</param>
/// <param name="Name">Имя</param>
/// <param name="Specialization">Специализация</param>
/// <param name="Rating">Рейтинг</param>
/// <param name="Avatar">Путь к файлу аватарки сотрудника</param>
/// <param name="AvatarBig">Путь к файлу аватарки сотрудника в более высоком разрешении</param>
/// <param name="Information">Дополнительная информация о сотруднике (HTML формат)</param>
/// <param name="Hidden">Скрыт ли от онлайн записи</param>
/// <param name="Position">Должность</param>
/// <param name="ServicesLinks">Услуги, оказываемые сотрудником</param>
public record YclientsEmployeeDto(
    [property: JsonPropertyName("id")]
    int Id,

    [property: JsonPropertyName("api_id")]
    string? ApiId,

    [property: JsonPropertyName("name")]
    string Name,

    [property: JsonPropertyName("specialization")]
    string Specialization,

    [property: JsonPropertyName("rating")]
    decimal Rating,

    [property: JsonPropertyName("avatar")]
    string Avatar,

    [property: JsonPropertyName("avatar_big")]
    string AvatarBig,

    [property: JsonPropertyName("information")]
    string Information,

    [property: JsonPropertyName("hidden")]
    int Hidden,

    [property: JsonPropertyName("position")]
    YclientsPositionDto Position,

    [property: JsonPropertyName("services_links")]
    List<YclientsServiceLinkDto> ServicesLinks
);
