using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceApp.Api.Data;
using FreelanceApp.Api.Dtos;
using FreelanceApp.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreelanceApp.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadAppointmentDto>>> GetAll()
        {
            return await _context.Appointments
                .Include(a => a.Professional)
                .ThenInclude(p => p.Specialty)
                .Select(a => new ReadAppointmentDto
                {
                    Id = a.Id,
                    ScheduledAt = a.ScheduledAt,
                    CLientName = a.ClientName,
                    CLientEmail = a.ClientEmail,
                    Professional = new ReadProfessionalDto
                    {
                        Id = a.Professional.Id,
                        Name = a.Professional.Name,
                        Email = a.Professional.Email,
                        Specialty = new ReadSpecialtyDto
                        {
                            Id = a.Professional.Specialty.Id,
                            Name = a.Professional.Specialty.Name
                        }
                    }
                })
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateAppointmentDto dto)
        {
            if (dto.ScheduledAt < DateTime.Now)
            {
                return BadRequest("You cannot scheduled an appointent in the past.");    
            }

            var professionalExists = await _context.Professionals.AnyAsync(p => p.Id == dto.ProfessionalId);

            if (!professionalExists)
            {
                return BadRequest("Professional not found.");
            }

            var conflictingAppointment = await _context.Appointments
                .AnyAsync(a =>
                a.ProfessionalId == dto.ProfessionalId &&
                a.ScheduledAt == dto.ScheduledAt);

            if (conflictingAppointment)
            {
                return Conflict("This professional already has an appoiintment at the selected time.");
            }

            var appointment = new Appointment
            {
                ScheduledAt = dto.ScheduledAt,
                ClientName = dto.ClientName,
                ClientEmail = dto.CLientEmail,
                ProfessionalId = dto.ProfessionalId
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, null);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadAppointmentDto>> GetById(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Professional)
                .ThenInclude(p => p.Specialty)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }

            return new ReadAppointmentDto
            {
                Id = appointment.Id,
                ScheduledAt = appointment.ScheduledAt,
                CLientName = appointment.ClientName,
                CLientEmail = appointment.ClientEmail,
                Professional = new ReadProfessionalDto
                {
                    Id = appointment.Professional.Id,
                    Name = appointment.Professional.Name,
                    Email = appointment.Professional.Email,
                    Specialty = new ReadSpecialtyDto
                    {
                        Id = appointment.Professional.Specialty.Id,
                        Name = appointment.Professional.Specialty.Name
                    }
                }
            };
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}