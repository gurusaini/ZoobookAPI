using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ZoobookAPI.Models;

namespace ZoobookAPI.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Author: Gurjeet Singh
        /// Created On: 29/11/2023
        /// MVC controller
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Employee emp)
        {
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44390/api/employees/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Employee>("PostEmployee", emp);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetEmployeeList");
                }
            }

            //ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(emp);
        }
        public ActionResult GetEmployeeList()
        {
            IEnumerable<Employee> emp = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44390/api/employees/");
                //HTTP GET
                var responseTask = client.GetAsync("GetEmployees");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Employee>>();
                    readTask.Wait();
                    emp = readTask.Result;
                }
                else //web api sent error response 
                {
                    emp = Enumerable.Empty<Employee>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(emp);
        }
        [HttpGet]
        public ActionResult EditEmployee(int Id)
        {
            Employee objEmp = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44390/api/employees/");
                //HTTP GET
                var responseTask = client.GetAsync("GetEmployee?id=" + Id.ToString()); 
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Employee>();
                    readTask.Wait();
                    objEmp = readTask.Result;
                }
                else //web api sent error response 
                {
                    ModelState.AddModelError(string.Empty, "Error Occurred. Please contact administrator.");
                }
            }
            return View(objEmp);
        }
       
        [HttpPost]
        public ActionResult EditEmployee(Employee emp)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44390/api/employees/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Employee>("PutEmployee?id="+emp.Id.ToString(), emp);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("GetEmployeeList");
                }
            }
            return View(emp);
        }
        public ActionResult DeleteEmployee(int Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44390/api/employees/");

                //HTTP POST
                var delTask = client.DeleteAsync("DeleteEmployee?id=" + Id.ToString());
                delTask.Wait();

                var result = delTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("GetEmployeeList");
                }
            }
            return RedirectToAction("GetEmployeeList"); 
        }
    }
}
