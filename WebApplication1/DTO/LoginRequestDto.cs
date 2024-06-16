﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTO;

public class LoginRequestDto
{
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}