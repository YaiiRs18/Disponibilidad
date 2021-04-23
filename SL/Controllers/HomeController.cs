using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using SL.Controllers;
using System.Net.Http.Formatting;

namespace SL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index1()
        {
            ViewBag.Title = "Home Page";


            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri("https://localhost:44372/");

            var request = clienteHttp.GetAsync("api/GetAll").Result;

            if (request.IsSuccessStatusCode)
            {
                var resultString = request.Content.ReadAsStringAsync().Result;

                var Listado = JsonConvert.DeserializeObject<List<ML.Entities.Datos_Personales>>(resultString);

                return View(Listado);
            }



            return View();
        }
        
        public ActionResult Agregar()
        {
            return View(new ML.Entities.Datos_Personales());



        }
        [HttpPost]
        public ActionResult Agregar(ML.Entities.Datos_Personales datos_Personales)
        {

            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri("https://localhost:44372/");

            var request = clienteHttp.PostAsync("api/Add", datos_Personales, new JsonMediaTypeFormatter()).Result;

            if (request.IsSuccessStatusCode)
            {
                var resultString = request.Content.ReadAsStringAsync().Result;

                var Listado = JsonConvert.DeserializeObject<ML.Entities.Datos_Personales>(resultString);

                if (Listado != null)
                {
                    return RedirectToAction("Index1");

                }



                return View(datos_Personales);

            }
            return View(datos_Personales);



        }
        [HttpGet]
        public ActionResult Eliminar(int ID)
        {


            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri("https://localhost:44372/");

            

            var request = clienteHttp.DeleteAsync("api/Delete/" + ID).Result;

            if (request.IsSuccessStatusCode)
            {
                var resultString = request.Content.ReadAsStringAsync().Result;

                var Listado = JsonConvert.DeserializeObject<ML.Entities.Datos_Personales>(resultString);

                if (Listado != null )
                {
                    return RedirectToAction("Index1");

                }





            }
            return View();


        }
    }
}

