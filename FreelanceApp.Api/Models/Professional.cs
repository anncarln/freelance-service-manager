using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceApp.Api.Models
{
    public class Professional
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public int SpecialtyId { get; set; }
        public Specialty? Specialty { get; set; }
    }
}