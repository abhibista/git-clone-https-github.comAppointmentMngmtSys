using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;

namespace Book.Models
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
        
        public IList<AuthenticationScheme> LoginWithGoogle { get; set; }
    }
}
