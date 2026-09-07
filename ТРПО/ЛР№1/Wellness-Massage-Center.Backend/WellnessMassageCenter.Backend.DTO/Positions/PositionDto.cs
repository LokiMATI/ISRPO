namespace WellnessMassageCenter.Backend.DTO.Positions;

/// <summary>
/// DTO должности
/// </summary>
/// <param name="Id">Идентиикатор</param>
/// <param name="Title">Наименование</param>
public record PositionDto(
    int Id,
    string Title
);
