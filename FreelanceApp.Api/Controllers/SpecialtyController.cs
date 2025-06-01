using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceApp.Api.Data;
using FreelanceApp.Api.Dtos;
using FreelanceApp.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace FreelanceApp.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SpecialtyController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadSpecialtyDto>>> GetAll()
        {
            return await _context.Specialties
                .Select(s => new ReadSpecialtyDto
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadSpecialtyDto>> GetById(int id)
        {
            var specialty = await _context.Specialties.FindAsync(id);

            if (specialty == null)
            {
                return NotFound();
            }

            return new ReadSpecialtyDto
            {
                Id = specialty.Id,
                Name = specialty.Name
            };
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateSpecialtyDto dto)
        {
            var specialty = new Specialty { Name = dto.Name };
            
            _context.Specialties.Add(specialty);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = specialty.Id }, specialty);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var specialty = await _context.Specialties.FindAsync(id);

            if (specialty == null)
            {
                return NotFound();
            }

            _context.Specialties.Remove(specialty);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}