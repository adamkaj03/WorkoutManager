using WorkoutManager.DTOs;

namespace WorkoutManager.Application.DTOs;

/// <summary>
/// Felhasználót leíró adatátviteli objektum.
/// Tartalmazza a felhasználó nevét, lakóhelyét, profilképét és az edzésprogramjait.
/// </summary>
public class UserDto
{
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
    /// A felhasználóhoz tartozó edzésprogramok listája.
    /// </summary>
    public List<WorkoutProgramDto> WorkoutPrograms { get; set; } = new();
}