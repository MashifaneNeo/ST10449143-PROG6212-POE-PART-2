using PROG6212_ST10449143_POE_PART_1.Models;
using System.Text.Json;

namespace PROG6212_ST10449143_POE_PART_1.Services
{
    public class ClaimService : IClaimService
    {
        private readonly string _dataFilePath;
        private List<Claim> _claims;
        private int _nextId = 1;

        public ClaimService(IWebHostEnvironment environment)
        {
            var dataFolder = Path.Combine(environment.ContentRootPath, "Data");
            if (!Directory.Exists(dataFolder))
                Directory.CreateDirectory(dataFolder);

            _dataFilePath = Path.Combine(dataFolder, "claims.json");
            _claims = LoadClaims();
            _nextId = _claims.Any() ? _claims.Max(c => c.Id) + 1 : 1;
        }

        private List<Claim> LoadClaims()
        {
            if (!File.Exists(_dataFilePath))
                return new List<Claim>();

            try
            {
                var json = File.ReadAllText(_dataFilePath);
                var claims = JsonSerializer.Deserialize<List<Claim>>(json) ?? new List<Claim>();
                return claims;
            }
            catch (Exception)
            {
                return new List<Claim>();
            }
        }

        private void SaveClaims()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(_claims, options);
                File.WriteAllText(_dataFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving claims: {ex.Message}");
            }
        }

        public void AddClaim(Claim claim)
        {
            claim.Id = _nextId++;
            claim.SubmittedDate = DateTime.Now;
            claim.Status = "Submitted"; // Set initial status
            _claims.Add(claim);
            SaveClaims();
        }

        public List<Claim> GetAllClaims()
        {
            return _claims.OrderByDescending(c => c.SubmittedDate).ToList();
        }

        public Claim GetClaimById(int id)
        {
            return _claims.FirstOrDefault(c => c.Id == id);
        }

        public void UpdateClaimStatus(int id, string status, string rejectionReason = null)
        {
            var claim = GetClaimById(id);
            if (claim != null)
            {
                claim.Status = status;
                claim.RejectionReason = rejectionReason;
                SaveClaims();
            }
        }

        public List<Claim> GetPendingClaims()
        {
            return _claims.Where(c => c.Status == "Submitted" || c.Status == "Under Review").OrderByDescending(c => c.SubmittedDate).ToList();
        }
    }
}