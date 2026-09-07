namespace WellnessMassageCenter.Backend.DTO.Yclients;

/// <summary>
/// DTO синхранизации базы данных с данными с Yclients
/// </summary>
/// <param name="IsSynchronizeCategories">Синхронизовать ли данные категорий услуг</param>
/// <param name="IsSynchronizeEmployees">Синхронизовать ли данные сотрудников</param>
/// <param name="IsSynchronizePositions">Синхронизовать ли данные должностей</param>
/// <param name="IsSynchronizeServices">Синхронизовать ли данные услуг</param>
public record YclientsSynchronizationDto(
    bool IsSynchronizeCategories = true,
    bool IsSynchronizeEmployees = true,
    bool IsSynchronizePositions = true,
    bool IsSynchronizeServices = true);
