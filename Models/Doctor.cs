using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Models
{
    public class Doctor
    {
            public int Id { get; set; }
            public string DoctorName { get; set; }
            public string Specification { get; set; }
            public int CreatedBy { get; set; }

            public DateTime CreatedOn { get; set; }

            public string Password { get; set; }
        }

    public class DoctorModel
    {
        public int Id { get; set; }
        public string DoctorName { get; set; }
        public string Specification { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int CreatedBy { get; set; }

        //public DateTime CreatedOn { get; set; } 

    }
}
