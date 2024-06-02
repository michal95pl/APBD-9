using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTO;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorController : ControllerBase
{

    private readonly IDoctorService _doctorService;
    public DoctorController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetDoctors()
    {
        var data = await _doctorService.GetDoctors();
        return Ok(data);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteDoctor([FromQuery] int idDoctor)
    {
        if (!await _doctorService.DoctorExists(idDoctor))
            return NotFound("Doctor not exists");

        await _doctorService.DeleteDoctor(idDoctor);
        
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> AddDoctor(DoctorDto doctorDto)
    {
        await _doctorService.AddDoctor(doctorDto);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> ModifyDoctor([FromQuery] int id, DoctorDto dto)
    {
        if (await _doctorService.DoctorExists(id))
            await _doctorService.ModifyDoctor(id, dto);
        else
            await _doctorService.AddDoctor(dto);
        
        return NoContent();
    }
    
}