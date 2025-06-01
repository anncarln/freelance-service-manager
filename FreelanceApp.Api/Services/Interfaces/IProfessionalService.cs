using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceApp.Api.Dtos;

namespace FreelanceApp.Api.Services.Interfaces
{
    public interface IProfessionalService
    {
        Task<IEnumerable<ReadProfessionalDto>> GetAllAsync();
        Task<ReadProfessionalDto?> GetByIdAsync(int id);
        Task<ReadProfessionalDto> CreateAsync(CreateProfessionalDto dto);
        Task<bool> DeleteAsync(int id);
    }
}