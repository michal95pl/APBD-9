using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Context;
using WebApplication1.DTO;
using WebApplication1.Services;

namespace AuthExample.Controllers;

// You should separate the controller into services, repositories etc
[ApiController]
[Route("/api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
   
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestDto dto)
    {
        await _userService.Register(dto);
        return NoContent();
    }
    
  
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto dto)
    {
        var response = await _userService.Login(dto);
        
        return Ok(response);
    }
    
    
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequestDto dto)
    {
        var response = await _userService.RefreshToken(dto);
        
        return Ok(response);
    }
}