using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WorkoutManager.Application.DTOs;
using WorkoutManager.Models;
using WorkoutManager.Shared.Constants;

namespace WorkoutManager.Controllers;

/// <summary>
/// Autentikációs és regisztrációs műveletek kezelésére szolgáló controller.
/// Kezeli a felhasználói regisztrációt, bejelentkezést és kijelentkezést.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController(UserManager<User> userManager, IConfiguration configuration)
    : ControllerBase
{
    /// <summary>
    /// Új felhasználó regisztrálása az alkalmazásban.
    /// Alapértelmezetten Reader szerepkört kap a felhasználó.
    /// </summary>
    /// <param name="dto">A regisztrációhoz szükséges adatok</param>
    /// <returns>JWT token sikeres regisztráció esetén</returns>
    /// <response code="200">Sikeres regisztráció, visszaadja a JWT tokent</response>
    /// <response code="400">Hibás adatok vagy sikertelen regisztráció</response>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var user = new User
        {
            UserName = dto.Email,
            Email = dto.Email,
            FullName = dto.FullName,
            City = dto.City,
            Country = dto.Country,
            ProfileImageUrl = dto.ProfileImageUrl
        };
        var result = await userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);
        
        await userManager.AddToRoleAsync(user, RoleNames.Reader);

        var token = await GenerateJwtToken(user);
        return Ok(new { token });
    }

    /// <summary>
    /// Bejelentkezés email és jelszó alapján.
    /// </summary>
    /// <param name="dto">A bejelentkezési adatok (email, jelszó)</param>
    /// <returns>JWT token sikeres bejelentkezés esetén</returns>
    /// <response code="200">Sikeres bejelentkezés, visszaadja a JWT tokent</response>
    /// <response code="401">Érvénytelen email vagy jelszó</response>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email);
        if (user == null)
            return Unauthorized("Invalid login attempt.");

        var isPasswordValid = await userManager.CheckPasswordAsync(user, dto.Password);
        if (!isPasswordValid)
            return Unauthorized("Invalid login attempt.");

        var token = await GenerateJwtToken(user);
        return Ok(new { token });
    }

    /// <summary>
    /// Kijelentkezés a rendszerből.
    /// JWT alapú autentikáció esetén a kliens oldali token törlése szükséges.
    /// </summary>
    /// <returns>Üzenet a sikeres kijelentkezésről</returns>
    /// <response code="200">Sikeres kijelentkezés</response>
    /// <response code="401">Nincs bejelentkezve</response>
    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        // JWT esetén a kliens oldali token törlése elegendő
        // A szerver nem tárol session-t, ezért itt nincs teendő
        // Esetleg lehetőség van a token érvénytelenítésére egy blacklist használatával
        return Ok(new { message = "Logout successful. Please remove the token from client." });
    }

    /// <summary>
    /// JWT token generálása a felhasználó számára.
    /// A token tartalmazza a felhasználó azonosítóját, nevét, email címét és szerepköreit.
    /// Todo: Ezt esetleg valami service-be kéne rakni
    /// </summary>
    /// <param name="user">A felhasználó, akinek a tokent generáljuk</param>
    /// <returns>Generált JWT token string formátumban</returns>
    private async Task<string> GenerateJwtToken(User user)
    {
        var roles = await userManager.GetRolesAsync(user);
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Szerepkörök hozzáadása claim-ként
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiryInHours = configuration.GetValue<int>("Jwt:ExpiryInHours");

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(expiryInHours),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}