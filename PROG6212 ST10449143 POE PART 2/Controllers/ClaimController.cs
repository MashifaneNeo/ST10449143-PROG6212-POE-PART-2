using Microsoft.AspNetCore.Mvc;

namespace PROG_UI_MVC.Controllers
{
    public class ClaimsController : Controller
    {
        public IActionResult Submit() => View();
        public IActionResult ViewClaims() => View();
        public IActionResult Approvals() => View();
        public IActionResult UploadDocs() => View();
        public IActionResult TrackStatus() => View();
    }
}
