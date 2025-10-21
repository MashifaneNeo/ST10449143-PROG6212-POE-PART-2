// Services/IClaimService.cs
using PROG6212_ST10449143_POE_PART_1.Models;

namespace PROG6212_ST10449143_POE_PART_1.Services
{
    public interface IClaimService
    {
        void AddClaim(Claim claim);
        List<Claim> GetAllClaims();
        Claim GetClaimById(int id);
        void UpdateClaimStatus(int id, string status, string rejectionReason = null);
        List<Claim> GetPendingClaims();
        bool DeleteClaim(int id);
    }
}