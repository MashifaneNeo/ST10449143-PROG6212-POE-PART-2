using Microsoft.AspNetCore.Mvc;
using PROG6212_ST10449143_POE_PART_1.Models;

namespace PROG_UI_MVC.Controllers
{
    public class ClaimsController : Controller
    {        
        public IActionResult Submit()
        {
            var model = new ClaimViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(ClaimViewModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["SuccessMessage"] = "Claim submitted successfully!";
                return RedirectToAction("Submit");
            }

            // If validation fails, return to form with errors
            return View(model);
        }

        public IActionResult ViewClaims() => View();
        public IActionResult Approvals() => View();
        public IActionResult UploadDocs() => View();
        public IActionResult TrackStatus() => View();
    }
}