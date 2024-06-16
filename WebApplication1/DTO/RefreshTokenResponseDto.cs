namespace WebApplication1.DTO;

public class RefreshTokenResponseDto
{
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }
}