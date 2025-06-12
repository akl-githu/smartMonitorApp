using Microsoft.AspNetCore.Mvc;
using smartMonitorApp.Models;
using smartMonitorApp.Data;
using System.Collections.Generic;
using System.Linq;

namespace smartMonitorApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                return RedirectToAction("Systems");
            }
            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        public IActionResult Systems()
        {
            var systems = new List<SystemStatus>
            {
                new SystemStatus { Name = "System A", Status = "Operational", Color = "green" },
                new SystemStatus { Name = "System B", Status = "Maintenance", Color = "orange" },
                new SystemStatus { Name = "System C", Status = "Down", Color = "red" }
            };

            return View(systems);
        }
    }
}