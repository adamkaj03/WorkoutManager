namespace WorkoutManager.Application.DTOs;

/// <summary>
/// Bejelentkezési adatokat leíró adatátviteli objektum.
/// Tartalmazza a felhasználó e-mail címét és jelszavát.
/// </summary>
public class LoginDto
{
    /// <summary>
    /// A felhasználó e-mail címe.
    /// </summary>
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// A felhasználó jelszava.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}