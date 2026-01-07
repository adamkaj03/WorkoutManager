using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using WorkoutManager.Domain.Entities;

namespace WorkoutManager.Models;

public class User : IdentityUser 
{
    [Required(ErrorMessage = "FullName is required")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "FullName must be between 2 and 200 characters")]
    public string FullName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "City is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "City must be between 2 and 100 characters")]
    public string City { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Country is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Country must be between 2 and 100 characters")]
    public string Country { get; set; } = string.Empty;
    
    [Url(ErrorMessage = "ProfileImageUrl must be a valid URL")]
    public string ProfileImageUrl { get; set; } = string.Empty;
    
    public ICollection<WorkoutProgram> WorkoutPrograms { get; set; } = new List<WorkoutProgram>();
    
    public ICollection<UserRole> UserRoles { get; set; } = [];
}