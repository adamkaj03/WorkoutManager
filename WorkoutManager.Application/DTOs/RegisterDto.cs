namespace WorkoutManager.Application.DTOs;

/// <summary>
/// Regisztrációs adatokat leíró adatátviteli objektum.
/// Tartalmazza a felhasználó e-mail címét, jelszavát, nevét, városát, országát, profilképét és szerepkörét.
/// </summary>
public class RegisterDto
{
    /// <summary>
    /// A felhasználó e-mail címe.
    /// </summary>
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// A felhasználó jelszava.
    /// </summary>
    public string Password { get; set; } = string.Empty;
    /// <summary>
    /// A felhasználó teljes neve.
    /// </summary>
    public string FullName { get; set; } = string.Empty;
    /// <summary>
    /// A felhasználó lakóhelyének városa.
    /// </summary>
    public string City { get; set; } = string.Empty;
    /// <summary>
    /// A felhasználó lakóhelyének országa.
    /// </summary>
    public string Country { get; set; } = string.Empty;
    /// <summary>
    /// A felhasználó profilképének URL-je.
    /// </summary>
    public string ProfileImageUrl { get; set; } = string.Empty;
    /// <summary>
    /// A felhasználó szerepköre (alapértelmezett: "Buyer").
    /// </summary>
    public string Role { get; set; } = "Buyer";
}