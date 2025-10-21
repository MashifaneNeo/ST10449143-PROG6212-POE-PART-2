// ClaimsController.cs
using Microsoft.AspNetCore.Mvc;
using PROG6212_ST10449143_POE_PART_1.Models;
using PROG6212_ST10449143_POE_PART_1.Services;

namespace PROG_UI_MVC.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly IClaimService _claimService;
        private readonly IWebHostEnvironment _environment;

        public ClaimsController(IClaimService claimService, IWebHostEnvironment environment)
        {
            _claimService = claimService;
            _environment = environment;
        }

        // GET: Submit Claim
        public IActionResult Submit()
        {
            var model = new ClaimViewModel();
            return View(model);
        }

        // POST: Submit Claim with file upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(ClaimViewModel model, IFormFile supportingDocument)
        {
            if (ModelState.IsValid)
            {
                string fileName = null;

                if (supportingDocument != null && supportingDocument.Length > 0)
                {
                    var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx", ".jpg", ".png" };
                    var maxFileSize = 5 * 1024 * 1024; 

                    var extension = Path.GetExtension(supportingDocument.FileName).ToLowerInvariant();
                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("supportingDocument", "Only PDF, DOCX, XLSX, JPG, PNG files are allowed.");
                        return View(model);
                    }

                    if (supportingDocument.Length > maxFileSize)
                    {
                        ModelState.AddModelError("supportingDocument", "File size must be less than 5MB.");
                        return View(model);
                    }

                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    fileName = Guid.NewGuid().ToString() + extension;
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await supportingDocument.CopyToAsync(stream);
                    }
                }

                var claim = new Claim
                {
                    LecturerName = model.LecturerName,
                    Month = model.Month,
                    HoursWorked = model.HoursWorked,
                    HourlyRate = model.HourlyRate,
                    AdditionalNotes = model.AdditionalNotes,
                    SupportingDocument = fileName,
                    Status = "Pending"
                };

                _claimService.AddClaim(claim);
                TempData["SuccessMessage"] = "Claim submitted successfully!";
                return RedirectToAction("Submit");
            }

            return View(model);
        }

        public IActionResult ViewClaims()
        {
            var claims = _claimService.GetAllClaims();
            return View(claims);
        }

        public IActionResult Approvals()
        {
            var pendingClaims = _claimService.GetPendingClaims();
            return View(pendingClaims);
        }

        [HttpPost]
        public IActionResult Approve(int id, string role)
        {
            var status = role == "coordinator" ? "ApprovedByCoordinator" : "ApprovedByManager";
            _claimService.UpdateClaimStatus(id, status);

            TempData["ApprovalMessage"] = $"Claim {status.Replace("By", " by ")} successfully!";
            return RedirectToAction("Approvals");
        }

        [HttpPost]
        public IActionResult Reject(int id, string rejectionReason, string role)
        {
            _claimService.UpdateClaimStatus(id, "Rejected", rejectionReason);

            TempData["ApprovalMessage"] = "Claim rejected successfully!";
            return RedirectToAction("Approvals");
        }

        public IActionResult UploadDocs()
        {
            return View();
        }

        public IActionResult TrackStatus()
        {
            var claims = _claimService.GetAllClaims();
            return View(claims);
        }
    }
}