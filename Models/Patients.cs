using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PatientName { get; set; }
        public string Password { get; set; }
        public int MobileNumber { get; set; }
        public string Gender { get; set; }
        public int DateOfBirth { get; set; }
        public int CreatedBy { get; set; }
        public int CreatedOn { get; set; }

    }
}
