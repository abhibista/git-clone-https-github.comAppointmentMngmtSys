using Book.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Book.Controllers
{
    public class LoginController : Controller
    {
        private readonly ArticleContext _context;
        private readonly IConfiguration _config;
        //private readonly SignInManager<User> _signInManager;
        public LoginController(ArticleContext context, IConfiguration config
            //SignInManager<User> signInManager
            )
        {
            _context = context;
            _config = config;
            //_signInManager = signInManager;
        }

        // [HttpGet]
        // public async Task<IActionResult> Login(String ReturnUrl)
        // {
        //     LoginViewModel model = new LoginViewModel
        //     {
        //         ReturnUrl = ReturnUrl,
        //         //LoginWithGoogle = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
        //     };

        //     // await _context.Users.AddAsync(User);
        //     // await _context.SaveChangesAsync();

        //      return View(model);
        // }
        

        //[HttpPost]
        //public async Task<ActionResult>  EmailPost( )
        //{
        //}

        [HttpPost]
        public ActionResult<bool> Login(LoginViewModel model)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == model.UserName);
            if (user == null)
            {
                return Ok("NOT Found");
            }
            else
            {
                var isValid = ValidatePassword(model.Password, user.Password);
                if (isValid)
                {
                    var token = GenerateJSONWebToken(model);
                }
                return Ok(true);
            }

            //var Doctor = _context.Doctors.FirstOrDefault(x => x.DoctorName == model.UserName);
            //  if (Doctor == null)
            //    {
            //        return Ok(false);
            //    }
            //    var isvalid = ValidatePassword(model.Password, Doctor.Password);
            //    if (isvalid)
            //    {
            //        var token = GenerateJSONWebToken(model);
            //        return Ok(token);
            //    }
            //    else
            //    {
            //        return Ok(false);
            //    }

            //        var Patient = _context.Patient.FirstOrDefault(x => x.PatientName == model.UserName);
            //        if (Patient == null)
            //        {
            //            return Ok(false);
            //        }
            //        var issValid = ValidatePassword(model.Password, Patient.Password);
            //        if (issValid)
            //        {
            //            var token = GenerateJSONWebToken(model);
            //            return Ok(token);
            //        }
            //        else
            //        {
            //            return Ok(false);
            //        }  
            
        }
        private string GenerateJSONWebToken(LoginViewModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(12),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private LoginViewModel AuthenticateUser(LoginViewModel login)
        {
            LoginViewModel user = null;

            //Validate the User Credentials    
            //Demo Purpose, I have Passed HardCoded User Information    
            if (login.UserName == "abhi")
            {
                user = new LoginViewModel { UserName = "abhi ", Password = "abhi123" };
            }
            return user;
        }

        [HttpPost("api/doctor")]
        public async Task<ActionResult<Doctor>> AddDoctors(DoctorModel model)
        {
            var doctor = new Doctor()
            {
                DoctorName = model.DoctorName,
                Specification = model.Specification,
                Password = CreateHash(model.Password),
                CreatedOn = DateTime.Now,
                CreatedBy = model.CreatedBy
            };
            ModelState.AddModelError("", "Invalid");

            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();

            return View(model);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddDoctors()
        {
            return View();
        }

        public bool ValidatePassword(string plainPassword, string hasedPassword)
        {
            //var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            //var hashed = BCrypt.Net.BCrypt.HashPassword(plainPassword, salt);
            var pass = BCrypt.Net.BCrypt.Verify(plainPassword, hasedPassword);
            return pass;
        }
        public string CreateHash(string password)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }
    }
}
