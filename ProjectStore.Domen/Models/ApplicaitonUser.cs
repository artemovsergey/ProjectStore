namespace ProjectStore.Domen.Models;

public class ApplicationUser : Base
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}