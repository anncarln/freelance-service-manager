using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceApp.Api.Models;

namespace FreelanceApp.Api.Repositories.Interfaces
{
    public interface IProfessionalRepository
    {
        Task<List<Professional>> GetAllAsync();
        Task<Professional?> GetByIdAsync(int id);
        Task AddAsync(Professional professional);
        Task DeleteAsync(Professional professional);
    }
}