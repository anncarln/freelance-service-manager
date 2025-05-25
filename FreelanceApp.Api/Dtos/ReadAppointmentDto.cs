using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceApp.Api.Dtos
{
    public class ReadAppointmentDto
    {
        public int Id { get; set; }
        public DateTime ScheduledAt { get; set; }
        public string CLientName { get; set; } = string.Empty;
        public string CLientEmail { get; set; } = string.Empty;

        public ReadProfessionalDto Professional { get; set; } = new();
    }
}