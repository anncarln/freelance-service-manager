using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceApp.Api.Models
{
    public class Specialty
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Professional>? Professionals { get; set; }
    }
}