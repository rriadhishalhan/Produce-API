using API.Base;
using API.Models;
using API.Repository;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee,EmployeeRepository,string>
    {
        private readonly EmployeeRepository employeeRepository;
        public IConfiguration _configuration;
        public EmployeesController(EmployeeRepository employeeRepository,IConfiguration configuration) : base (employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            this._configuration = configuration;
        }

        //[Authorize(Roles = "Director")]
        [HttpGet("MasterEmployeeData")]
        public ActionResult MasterEmployeeData()
        {
            try
            {
                var dataMaster = employeeRepository.MasterEmployee();
                if (dataMaster.Count != 0)
                {
                    return Ok(dataMaster);
                }
                else
                {
                    return NotFound("Tidak ada data master");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "MASTER DATA Server Error");
            }
        }

        [HttpGet("MasterEmployeeData/{NIK}")]
        public ActionResult MasterEmployeeData(string NIK)
        {
            try
            {
                var dataMaster = employeeRepository.MasterEmployee(NIK);
                if (dataMaster.Count != 0)
                {
                    return Ok(dataMaster);
                }
                else
                {
                    return NotFound("NIK TIDAK DITEMUKAN");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "MASTER DATA Server Error");
            }
        }

        [HttpGet("TestCORS")]
        public ActionResult TestCORS()
        {
            return Ok("TEST CORS BERHASIL!");
        }
    }
}
