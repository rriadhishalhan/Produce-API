using API.Models;
using API.ViewModel;
using Client.Base;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Client.Controllers
{
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository repository;
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<JsonResult> GetAllProfile()
        {
            var result = await repository.GetAllProfile();
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetCountGender()
        {
            var result = await repository.GetCountGender();
            return Json(result);
        }
        [HttpGet]
        public async Task<JsonResult> GetCountUniversities()
        {
            var result = await repository.GetCountUniversities();
            return Json(result);
        }

        [HttpGet]
        public JsonResult GetProfile(string NIK)
        {
            var result = repository.GetProfile(NIK);
            return Json(result);
        }






        [HttpPost]
        public JsonResult Register([FromBody] RegisterVM entity)
        {
            var result = repository.Register(entity);
            return Json(result);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
