using System.Text.Json.Serialization;

namespace WellnessMassageCenter.Backend.DTO.Yclients;

/// <summary>
/// DTO сотрудника, оказывающего данную услугу
/// </summary>
/// <param name="Id"></param>
public record YclientsServiceEmployeeDto(
    [property: JsonPropertyName("id")]
    int Id
);
