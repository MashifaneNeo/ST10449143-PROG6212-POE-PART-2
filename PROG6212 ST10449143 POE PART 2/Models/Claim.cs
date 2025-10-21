using System.ComponentModel.DataAnnotations;

namespace PROG6212_ST10449143_POE_PART_1.Models
{
    public class Claim
    {
        public int Id { get; set; }

        [Required]
        public string LecturerName { get; set; }

        [Required]
        public string Month { get; set; }

        [Required]
        public decimal HoursWorked { get; set; }

        [Required]
        public decimal HourlyRate { get; set; }

        public decimal TotalAmount => HoursWorked * HourlyRate;

        public string AdditionalNotes { get; set; }

        public string SupportingDocument { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime SubmittedDate { get; set; } = DateTime.Now;

        public string RejectionReason { get; set; }
    }
}