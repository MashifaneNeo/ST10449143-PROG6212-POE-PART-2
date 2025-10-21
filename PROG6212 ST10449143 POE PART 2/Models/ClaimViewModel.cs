namespace PROG6212_ST10449143_POE_PART_1.Models
{
    public class ClaimViewModel
    {
        public string LecturerName { get; set; }
        public string Month { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public string AdditionalNotes { get; set; }
        public List<string> AvailableMonths => new List<string>
        {
            "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        };
    }
}