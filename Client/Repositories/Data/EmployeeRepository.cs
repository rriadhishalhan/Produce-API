using API.Models;
using API.ViewModel;
using Client.Base;
using Client.ViewModel;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;

        public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _contextAccessor.HttpContext.Session.GetString("JWToken"));
        }

        public async Task<List<EmployeeProfileVM>> GetAllProfile()
        {
            /// isi codingan kalian disini
            List<EmployeeProfileVM> entities = new List<EmployeeProfileVM>();

            using (var response = await httpClient.GetAsync(address.link + request + "MasterEmployeeData/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<EmployeeProfileVM>>(apiResponse);
            }

            return entities;
        }
        public async Task<List<EmployeeProfileVM>> GetProfile(string nik)
        {
            List<EmployeeProfileVM> entities = new List<EmployeeProfileVM>();

            using (var response = await httpClient.GetAsync(address.link + request + "MasterEmployeeData/"+nik))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<EmployeeProfileVM>>(apiResponse);
            }

            return entities;
        }
        public async Task<List<ChartVM>> GetCountGender()
        {
            /// isi codingan kalian disini
            List<ChartVM> entities = new List<ChartVM>();

            using (var response = await httpClient.GetAsync(address.link + request + "CountGender/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<ChartVM>>(apiResponse);
            }

            return entities;
        }
        public async Task<List<ChartVM>> GetCountUniversities()
        {
            /// isi codingan kalian disini
            List<ChartVM> entities = new List<ChartVM>();

            using (var response = await httpClient.GetAsync(address.link + request + "CountUniversities/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<ChartVM>>(apiResponse);
            }

            return entities;
        }






        public HttpStatusCode Register(RegisterVM entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + "Accounts/Register", content).Result;
            return result.StatusCode;
        }

    }
}
