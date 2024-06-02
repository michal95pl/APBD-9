using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.DTO;
using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IDoctorService
{
    public Task<List<DoctorDto>> GetDoctors();
    public Task DeleteDoctor(int id);
    public Task AddDoctor(DoctorDto doctorDto);
    public Task<bool> DoctorExists(int id);
    public Task ModifyDoctor(int idDoctor, DoctorDto doctor);
}

public class DoctorService : IDoctorService
{
    
    private readonly AppDbContext _context;
    public DoctorService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<DoctorDto>> GetDoctors()
    {
        var data = await _context.Doctors.Select(d => new DoctorDto
        {
            FirstName = d.FirstName!,
            LastName = d.LastName!,
            Email = d.Email!
        }).ToListAsync();
        
        return data;
    }

    public async Task DeleteDoctor(int id)
    {
        var doctorToRemove = new Doctor()
        {
            IdDoctor = id
        };

        _context.Doctors.Attach(doctorToRemove);
        
        _context.Doctors.Remove(doctorToRemove);

        await _context.SaveChangesAsync();
    }

    public async Task AddDoctor(DoctorDto doctorDto)
    {
        var doctorToAdd = new Doctor()
        {
            FirstName = doctorDto.FirstName,
            LastName = doctorDto.LastName,
            Email = doctorDto.Email
        };

        await _context.Doctors.AddAsync(doctorToAdd);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DoctorExists(int id)
    {
        var cnt = await _context.Doctors.CountAsync(d => d.IdDoctor == id);

        return cnt >= 1;
    }

    public async Task ModifyDoctor(int idDoctor, DoctorDto doctor)
    {
        var dataToChange = await _context.Doctors.Where(d => d.IdDoctor == idDoctor).FirstAsync();

        dataToChange.FirstName = doctor.FirstName;
        dataToChange.LastName = doctor.LastName;
        dataToChange.Email = doctor.Email;

        await _context.SaveChangesAsync();
    }
}