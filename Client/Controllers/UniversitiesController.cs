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
    public class UniversitiesController : BaseController<University, UniversityRepository, int>
    {

        private readonly UniversityRepository repository;
        public UniversitiesController(UniversityRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
