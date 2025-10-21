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

        public IActionResult Submit()
        {
            var model = new ClaimViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(ClaimViewModel model, IFormFile supportingDocument)
        {
            ModelState.Remove("AdditionalNotes");

            if (ModelState.IsValid)
            {
                string fileName = null;

                if (supportingDocument != null && supportingDocument.Length > 0)
                {
                    var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx", ".jpg", ".png", ".jpeg" };
                    var maxFileSize = 5 * 1024 * 1024;

                    var extension = Path.GetExtension(supportingDocument.FileName).ToLowerInvariant();
                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("", "Only PDF, DOCX, XLSX, JPG, PNG files are allowed.");
                        return View(model);
                    }

                    if (supportingDocument.Length > maxFileSize)
                    {
                        ModelState.AddModelError("", "File size must be less than 5MB.");
                        return View(model);
                    }

                    try
                    {
                        var wwwrootPath = _environment.WebRootPath;
                        if (string.IsNullOrEmpty(wwwrootPath))
                        {
                            wwwrootPath = Path.Combine(_environment.ContentRootPath, "wwwroot");
                        }

                        var uploadsFolder = Path.Combine(wwwrootPath, "uploads");
                        if (!Directory.Exists(uploadsFolder))
                            Directory.CreateDirectory(uploadsFolder);

                        fileName = $"{Guid.NewGuid()}{extension}";
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await supportingDocument.CopyToAsync(stream);
                        }

                        fileName = $"{fileName}|{supportingDocument.FileName}";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"File upload error: {ex.Message}");
                        ModelState.AddModelError("", "Error uploading file. Please try again.");
                        return View(model);
                    }
                }

                try
                {
                    var claim = new Claim
                    {
                        LecturerName = model.LecturerName,
                        Month = model.Month,
                        HoursWorked = model.HoursWorked,
                        HourlyRate = model.HourlyRate,
                        AdditionalNotes = model.AdditionalNotes ?? string.Empty, // Ensure it's never null
                        SupportingDocument = fileName,
                        Status = "Submitted"
                    };

                    _claimService.AddClaim(claim);
                    TempData["SuccessMessage"] = "Claim submitted successfully!";
                    return RedirectToAction("Submit");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving claim: {ex.Message}");
                    ModelState.AddModelError("", "An error occurred while saving your claim. Please try again.");
                    return View(model);
                }
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
            foreach (var claim in pendingClaims.Where(c => c.Status == "Submitted"))
            {
                _claimService.UpdateClaimStatus(claim.Id, "Under Review");
            }

            var updatedClaims = _claimService.GetPendingClaims();
            return View(updatedClaims);
        }

        [HttpPost]
        public IActionResult Approve(int id, string role)
        {
            var status = "Approved";
            _claimService.UpdateClaimStatus(id, status);

            TempData["ApprovalMessage"] = "Claim approved successfully!";
            return RedirectToAction("Approvals");
        }

        [HttpPost]
        public IActionResult Reject(int id, string rejectionReason, string role)
        {
            _claimService.UpdateClaimStatus(id, "Rejected", rejectionReason);

            TempData["ApprovalMessage"] = "Claim rejected successfully!";
            return RedirectToAction("Approvals");
        }       

        public IActionResult TrackStatus()
        {
            var claims = _claimService.GetAllClaims();
            return View(claims);
        }
    }
}