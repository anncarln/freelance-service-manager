using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceApp.Api.Data;
using FreelanceApp.Api.Dtos;
using FreelanceApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreelanceApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessionalController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadProfessionalDto>>> GetAll()
        {
            return await _context.Professionals
                .Include(p => p.Specialty)
                .Select(p => new ReadProfessionalDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Email = p.Email,
                    Specialty = new ReadSpecialtyDto
                    {
                        Id = p.Specialty.Id,
                        Name = p.Specialty.Name
                    }
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadProfessionalDto>> GetById(int id)
        {
            var professional = await _context.Professionals
                .Include(p => p.Specialty)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (professional == null)
            {
                return NotFound();
            }

            return new ReadProfessionalDto
            {
                Id = professional.Id,
                Name = professional.Name,
                Email = professional.Email,
                Specialty = new ReadSpecialtyDto
                {
                    Id = professional.Specialty.Id,
                    Name = professional.Specialty.Name
                }
            };
        }

        [HttpPost]
        public async Task<ActionResult<Professional>> Create(CreateProfessionalDto dto)
        {
            var professional = new Professional
            {
                Name = dto.Name,
                Email = dto.Email,
                SpecialtyId = dto.SpecialtyId
            };
            
            _context.Professionals.Add(professional);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = professional.Id }, professional);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var professional = await _context.Professionals.FindAsync(id);

            if (professional == null)
            {
                return NotFound();
            }

            _context.Professionals.Remove(professional);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}