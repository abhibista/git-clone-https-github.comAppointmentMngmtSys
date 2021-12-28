using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public int UserTypeId { get; set; }

        public string Address { get; set; }

        public string MobileNo { get; set; }

        public string ContactNumber { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
