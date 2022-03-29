using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class EmployeeProfileVM
    {
        public string NIK { get; set; }
        public string role { get; set; }
        public string fullname { get; set; }
        public string Gender { get; set; }
        public int Salary { get; set; }
        public string phone { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Degree { get; set; }
        public string GPA { get; set; }
        public string universityName { get; set; }

    }
}
