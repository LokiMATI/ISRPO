namespace StudService.DTO;

public class RegistrationDto
{
    public required string Login { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public required RoleDto Role { get; set; }

}
