using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceApp.Api.Data;
using FreelanceApp.Api.Dtos;
using FreelanceApp.Api.Models;
using FreelanceApp.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreelanceApp.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessionalController(IProfessionalService service) : ControllerBase
    {
        private readonly IProfessionalService _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadProfessionalDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadProfessionalDto>> GetById(int id)
        {
            var professional = await _service.GetByIdAsync(id);

            if (professional == null)
            {
                return NotFound();
            }

            return Ok(professional);
        }

        [HttpPost]
        public async Task<ActionResult<Professional>> Create(CreateProfessionalDto dto)
        {
            var created = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}