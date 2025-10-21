using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [Range(0.5, 744)]
        public decimal HoursWorked { get; set; }

        [Required]
        [Range(1, 10000)]
        public decimal HourlyRate { get; set; }

        [JsonIgnore]
        public decimal TotalAmount => HoursWorked * HourlyRate;

        public string AdditionalNotes { get; set; }

        public string SupportingDocument { get; set; }

        public string Status { get; set; } = "Submitted";

        public DateTime SubmittedDate { get; set; } = DateTime.Now;

        public string RejectionReason { get; set; }

        public Claim()
        {
            Status ??= "Submitted";
            AdditionalNotes ??= string.Empty;
            SupportingDocument ??= string.Empty;
            RejectionReason ??= string.Empty;
        }
    }
}