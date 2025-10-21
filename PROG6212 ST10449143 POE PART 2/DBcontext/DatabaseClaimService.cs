using Microsoft.EntityFrameworkCore;
using PROG6212_ST10449143_POE_PART_1.Models;

namespace PROG6212_ST10449143_POE_PART_1.Services
{
    public class DatabaseClaimService : IClaimService
    {
        private readonly AppDbContext _context;

        public DatabaseClaimService(AppDbContext context)
        {
            _context = context;
        }

        public async void AddClaim(Claim claim)
        {
            try
            {
                claim.SubmittedDate = DateTime.Now;
                claim.Status = "Submitted";

                _context.Claims.Add(claim);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding claim to database: {ex.Message}");
                throw;
            }
        }

        public List<Claim> GetAllClaims()
        {
            try
            {
                return _context.Claims
                    .OrderByDescending(c => c.SubmittedDate)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting claims from database: {ex.Message}");
                return new List<Claim>();
            }
        }

        public Claim GetClaimById(int id)
        {
            try
            {
                return _context.Claims.FirstOrDefault(c => c.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting claim by ID from database: {ex.Message}");
                return null;
            }
        }

        public void UpdateClaimStatus(int id, string status, string rejectionReason = null)
        {
            try
            {
                var claim = _context.Claims.FirstOrDefault(c => c.Id == id);
                if (claim != null)
                {
                    claim.Status = status;
                    claim.RejectionReason = rejectionReason;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating claim status in database: {ex.Message}");
                throw;
            }
        }

        public List<Claim> GetPendingClaims()
        {
            try
            {
                return _context.Claims
                    .Where(c => c.Status == "Submitted" || c.Status == "Under Review")
                    .OrderByDescending(c => c.SubmittedDate)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting pending claims from database: {ex.Message}");
                return new List<Claim>();
            }
        }

        public bool DeleteClaim(int id)
        {
            try
            {
                var claim = _context.Claims.FirstOrDefault(c => c.Id == id);
                if (claim != null)
                {
                    _context.Claims.Remove(claim);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting claim from database: {ex.Message}");
                return false;
            }
        }
    }
}