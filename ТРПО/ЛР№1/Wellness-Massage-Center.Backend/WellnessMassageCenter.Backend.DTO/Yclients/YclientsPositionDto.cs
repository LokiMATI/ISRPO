using System.Text.Json.Serialization;

namespace WellnessMassageCenter.Backend.DTO.Yclients;

/// <summary>
/// DTO должности Yclients
/// </summary>
/// <param name="Id"></param>
/// <param name="Title"></param>
public record YclientsPositionDto(
    [property: JsonPropertyName("id")]
    int Id,

    [property: JsonPropertyName("title")]
    string Title
);

