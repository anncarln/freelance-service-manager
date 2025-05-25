using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceApp.Api.Dtos
{
    public class ReadProfessionalDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public ReadSpecialtyDto Specialty { get; set; } = new();
    }
}