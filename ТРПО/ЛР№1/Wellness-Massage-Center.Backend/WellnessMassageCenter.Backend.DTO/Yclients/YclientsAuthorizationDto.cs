using System.Text.Json.Serialization;

namespace WellnessMassageCenter.Backend.DTO.Yclients;

/// <summary>
/// DTO авторизации пользователя Yclients
/// </summary>
/// <param name="Login">Логин</param>
/// <param name="Password">Пароль</param>
public record YclientsAuthorizationDto(
    [property: JsonPropertyName("login")]
    string Login,

    [property: JsonPropertyName("password")]
    string Password
);
