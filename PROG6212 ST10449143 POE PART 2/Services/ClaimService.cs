using PROG6212_ST10449143_POE_PART_1.Models;

namespace PROG6212_ST10449143_POE_PART_1.Services
{
    public class ClaimService : IClaimService
    {
        private readonly List<Claim> _claims = new();
        private int _nextId = 1;

        public void AddClaim(Claim claim)
        {
            claim.Id = _nextId++;
            _claims.Add(claim);
        }

        public List<Claim> GetAllClaims() => _claims;

        public Claim GetClaimById(int id) => _claims.FirstOrDefault(c => c.Id == id);

        public void UpdateClaimStatus(int id, string status, string rejectionReason = null)
        {
            var claim = GetClaimById(id);
            if (claim != null)
            {
                claim.Status = status;
                claim.RejectionReason = rejectionReason;
            }
        }

        public List<Claim> GetPendingClaims() => _claims.Where(c => c.Status == "Pending").ToList();
    }
}