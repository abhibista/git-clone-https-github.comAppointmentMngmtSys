using Book.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Book.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ArticleContext _context;
        public HomeController(ILogger<HomeController> logger, ArticleContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    UserName = model.Email,
                    UserTypeId = 1,
                    Password = CreateHash(model.Password),
                    CreatedOn = DateTime.Now
                };
                ModelState.AddModelError("", "Invalid Register.");

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            return Ok(model);
        }    

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return Ok();
            // return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        public string CreateHash(string password)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }
    }
}
