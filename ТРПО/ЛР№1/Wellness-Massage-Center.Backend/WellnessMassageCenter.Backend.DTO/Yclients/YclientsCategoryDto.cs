namespace WellnessMassageCenter.Backend.DTO.Yclients;

using System.Text.Json.Serialization;

/// <summary>
/// DTO категории услуг Yclients
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="CategoryId">Идентификатор услуги в организации</param>
/// <param name="SalonServiceId">Идентификатор филиала</param>
/// <param name="Title">Наименование</param>
/// <param name="Weight">Вес</param>
/// <param name="ApiId">API идентификатор</param>
/// <param name="Staff">Персонал</param>
/// <param name="BookingTitle">Отображаемое наименование</param>
/// <param name="PriceMin">Минимальная цена</param>
/// <param name="PriceMax">Максимальная цена</param>
/// <param name="Sex">Пол клиента</param>
/// <param name="IsChain">Услуги предоставляются только в филиале</param>
public record YclientsCategoryDto(
    [property: JsonPropertyName("id")]
    int Id,

    [property: JsonPropertyName("category_id")]
    int CategoryId,

    [property: JsonPropertyName("salon_service_id")]
    int SalonServiceId,

    [property: JsonPropertyName("title")]
    string Title,

    [property: JsonPropertyName("weight")]
    int Weight,

    [property: JsonPropertyName("api_id")]
    string ApiId,

    [property: JsonPropertyName("staff")]
    int[] Staff,

    [property: JsonPropertyName("booking_title")]
    string BookingTitle,

    [property: JsonPropertyName("price_min")]
    decimal PriceMin,

    [property: JsonPropertyName("price_max")]
    decimal PriceMax,

    [property: JsonPropertyName("sex")]
    int Sex,

    [property: JsonPropertyName("is_chain")]
    bool IsChain
);
