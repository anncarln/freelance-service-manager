using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceApp.Api.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime ScheduledAt { get; set; }

        [Required]
        public string ClientName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string ClientEmail { get; set; } = string.Empty;

        [Required]
        [ForeignKey("Professional")]
        public int ProfessionalId { get; set; }
        
        public Professional? Professional { get; set; }
    }
}