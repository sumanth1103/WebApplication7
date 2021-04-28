using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class HomeController : Controller
    {
        //GetAllEmployees
        //static List<Employee> emp = new List<Employee>();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllEmployees()
        {
            IList<Employee> person;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://dummy.restapiexample.com/");
                var responseTask = client.GetAsync("api/v1/employees");
                responseTask.Wait();
                //To store result of web api response.   
                var result = responseTask.Result;
                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync().Result;
                    var s = JsonConvert.DeserializeObject(readTask);
                    JObject o = JObject.Parse(s.ToString());
                    JArray a = (JArray)o["data"];
                     person = a.ToObject<IList<Employee>>();
                    if(person == null)
                    {
                        ViewBag.msg = "no data";
                        
                    }
                    else
                    {
                        return View(person.ToList());
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View();
        }
        public ActionResult GetSingleEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetSingleEmployee(SingleEmp s1)
        {
            IList<Employee> person;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://dummy.restapiexample.com/");
                var responseTask = client.GetAsync("api/v1/employee/" + s1.emp);
                responseTask.Wait();
                //To store result of web api response.   
                var result = responseTask.Result;
                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync().Result;
                    var s = JsonConvert.DeserializeObject(readTask);
                    //person = new JavaScriptSerializer().Deserialize<List<Employee>>(readTask);
                    JObject o = JObject.Parse(s.ToString());
                    //var brands = s.SelectToken("o[data]")?.ToObject<string>();
                    JArray a = JArray.Parse(o["data"]);
                    person = a.ToObject<IList<Employee>>();
                    //person = JsonConvert.DeserializeObject<Employee>();
                    //person = (IList<Employee>)(JArray)o.ToObject<IList<Employee>>();
                    if (person == null)
                    {
                        ViewBag.msg = "no data";

                    }
                    else
                    {
                        int i = 0;
                        string s2 = "0";
                        foreach(var j in person.ToList())
                        {
                            ViewData[s2] = j;
                            i++;
                            s2 = s2+i;
                        }
                        //ViewData["emp"] = person.ToList();
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}