using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceApp.Api.Dtos
{
    public class CreateAppointmentDto
    {
        [Required]
        public DateTime ScheduledAt { get; set; }

        [Required]
        [StringLength(100)]
        public string ClientName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string CLientEmail { get; set; } = string.Empty;

        [Required]
        public int ProfessionalId { get; set; }
    }
}