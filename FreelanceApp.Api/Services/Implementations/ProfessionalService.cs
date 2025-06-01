using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreelanceApp.Api.Data;
using FreelanceApp.Api.Dtos;
using FreelanceApp.Api.Models;
using FreelanceApp.Api.Repositories.Interfaces;
using FreelanceApp.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FreelanceApp.Api.Services.Implementations
{
    public class ProfessionalService(IProfessionalRepository repository,
        IMapper mapper) : IProfessionalService
    {
        private readonly IProfessionalRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<ReadProfessionalDto>> GetAllAsync()
        {
            var professionals = await _repository.GetAllAsync();
            return _mapper.Map<List<ReadProfessionalDto>>(professionals);
        }

        public async Task<ReadProfessionalDto?> GetByIdAsync(int id)
        {
            var professional = await _repository.GetByIdAsync(id);

            if (professional == null)
            {
                return null;
            }

            return _mapper.Map<ReadProfessionalDto>(professional);
        }

        public async Task<ReadProfessionalDto> CreateAsync(CreateProfessionalDto dto)
        {
            var professional = _mapper.Map<Professional>(dto);

            await _repository.AddAsync(professional);

            return _mapper.Map<ReadProfessionalDto>(professional);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var professional = await _repository.GetByIdAsync(id);

            if (professional == null)
            {
                return false;
            }

            await _repository.DeleteAsync(professional);

            return true;
        }
    }
}