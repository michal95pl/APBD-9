using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTO;

public class RefreshTokenRequestDto
{
    [Required]
    public string AccessToken { get; set; }
    [Required]
    public string RefreshToken { get; set; }
}