using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(AuthenticationSchemes = "AzureAd")]
        public IActionResult ProtectedAzureAD()
        {
            return View();
        }

        [Authorize(AuthenticationSchemes = "AzureAdB2C")]
        public IActionResult ProtectedAzureADB2C()
        {
            return View();
        }

        [Authorize(AuthenticationSchemes = "OidcProvider")]
        public IActionResult ProtectedOIDC()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}