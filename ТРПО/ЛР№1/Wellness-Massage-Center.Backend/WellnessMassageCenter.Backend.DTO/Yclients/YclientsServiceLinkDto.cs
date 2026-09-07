using System.Text.Json.Serialization;

namespace WellnessMassageCenter.Backend.DTO.Yclients;

/// <summary>
/// DTO ссылки на услугу, выполняемой сотрудником в Yclients
/// </summary>
/// <param name="ServiceId">Идентификатор услуги</param>
public record YclientsServiceLinkDto(
    [property: JsonPropertyName("service_id")]
    int ServiceId
);
