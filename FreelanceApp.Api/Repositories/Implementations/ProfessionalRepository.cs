using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceApp.Api.Data;
using FreelanceApp.Api.Models;
using FreelanceApp.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FreelanceApp.Api.Repositories.Implementations
{
    public class ProfessionalRepository(ApplicationDbContext context) : IProfessionalRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<List<Professional>> GetAllAsync()
        {
            return await _context.Professionals
                .Include(p => p.Specialty)
                .ToListAsync();
        }

        public async Task<Professional?> GetByIdAsync(int id)
        {
            return await _context.Professionals
                .Include(p => p.Specialty)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Professional professional)
        {
            _context.Professionals.Add(professional);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Professional professional)
        {
            _context.Professionals.Remove(professional);
            await _context.SaveChangesAsync();
        }
    }
}