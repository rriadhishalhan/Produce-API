using API.Context;
using API.Models;
using API.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {

        private readonly MyContext myContext;
        public EmployeeRepository(MyContext myContext):base(myContext)
        {
            this.myContext = myContext;

        }

        public ICollection MasterEmployee()
        {
            var data = myContext.Employees
                .Join(myContext.Accounts, e => e.NIK, a => a.NIK, (e, a) => new { e, a })
                .Join(myContext.AccountRoles, ea => ea.a.NIK, acr => acr.NIK, (ea, acr) => new { ea, acr })
                .Join(myContext.Roles, eaacr => eaacr.acr.Role_Id, r => r.Id, (eaacr, r) => new { eaacr, r })
                .Join(myContext.Profilings, eaacrr => eaacrr.eaacr.acr.NIK, p => p.NIK, (eaacrr, p) => new { eaacrr, p })
                .Join(myContext.Educations, eaacrrp => eaacrrp.p.Education_Id, ed => ed.Id, (eaacrrp, ed) => new { eaacrrp, ed })
                .Join(myContext.Universities, eaacrrped => eaacrrped.ed.University_Id, u => u.Id, (eaacrrped, u) => new { eaacrrped, u })
                .Select(d => new
                {
                    NIK = d.eaacrrped.eaacrrp.eaacrr.eaacr.ea.e.NIK.ToString(),
                    Role = d.eaacrrped.eaacrrp.eaacrr.r.JobRole,
                    FullName = $"{d.eaacrrped.eaacrrp.eaacrr.eaacr.ea.e.FirstName} {d.eaacrrped.eaacrrp.eaacrr.eaacr.ea.e.LastName}",
                    Gender = d.eaacrrped.eaacrrp.eaacrr.eaacr.ea.e.Gender.ToString(),
                    Phone = d.eaacrrped.eaacrrp.eaacrr.eaacr.ea.e.Phone,
                    Email = d.eaacrrped.eaacrrp.eaacrr.eaacr.ea.e.Email,
                    BirthDate = d.eaacrrped.eaacrrp.eaacrr.eaacr.ea.e.BirthDate,
                    Salary = d.eaacrrped.eaacrrp.eaacrr.eaacr.ea.e.Salary,
                    Education_Id = d.eaacrrped.eaacrrp.p.Education_Id,
                    GPA = d.eaacrrped.ed.GPA,
                    Degree = d.eaacrrped.ed.Degree,
                    UniversityName = d.u.Name
                });
            

            return data.ToList();
        }


    }
}
