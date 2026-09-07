using System.Text.Json.Serialization;

namespace WellnessMassageCenter.Backend.DTO.Yclients;

/// <summary>
/// DTO услуги Yclients
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="CategoryId">Идентификатор категории</param>
/// <param name="Title">Наименование</param>
/// <param name="Comment">Комментарий</param>
/// <param name="Duration">ПРодолжительность</param>
/// <param name="ServiceType">Возможна ли регистрация онлайн</param>
/// <param name="Staff">Персонал</param>
public record YclientsServiceDto(
    [property: JsonPropertyName("id")]
    int Id,

    [property: JsonPropertyName("category_id")]
    int CategoryId,

    [property: JsonPropertyName("title")]
    string Title,

    [property: JsonPropertyName("comment")]
    string Comment,

    [property: JsonPropertyName("duration")]
    short Duration,

    [property: JsonPropertyName("service_type")]
    int ServiceType,

    [property: JsonPropertyName("staff")]
    List<YclientsServiceEmployeeDto> Staff
);
