using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApi.Models;
using WebMvc.Models;

namespace WebMvc.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            IList<MvcEmoloyeeModel> employeeList;
            HttpResponseMessage response = GlobalVeriables.WebApiClient.GetAsync("Employees").Result;
            employeeList = response.Content.ReadAsAsync<IList<MvcEmoloyeeModel>>().Result;

            return View(employeeList);
        }

        [HttpGet]
        public ActionResult CreateOrEdit(int id=0)
        {
            if (id == 0)
            {
                return View(new MvcEmoloyeeModel());
            }
            else
            {
                HttpResponseMessage response =
                    GlobalVeriables.WebApiClient.GetAsync("Employees/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MvcEmoloyeeModel>().Result);
            }
        }

        [HttpPost]
        public ActionResult CreateOrEdit(MvcEmoloyeeModel employee)
        {
            if (employee.Id == 0)
            {
                HttpResponseMessage response = GlobalVeriables.WebApiClient.PostAsJsonAsync("Employees", employee).Result;
                TempData["successMessage"] = "Save Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVeriables.WebApiClient
                    .PutAsJsonAsync("Employees/" + employee.Id, employee).Result;
                TempData["successMessage"] = "Update Successfully";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVeriables.WebApiClient.GetAsync("Employees/" + id.ToString()).Result;
            return View(response.Content.ReadAsAsync<MvcEmoloyeeModel>().Result);
        }

        [HttpPost]
        public ActionResult Delete(MvcEmoloyeeModel emoloyee)
        {
            HttpResponseMessage response = GlobalVeriables.WebApiClient
                .DeleteAsync("Employees/" + emoloyee.Id).Result;

            return RedirectToAction("Index");
        }
    }
}