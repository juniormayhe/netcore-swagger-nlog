using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PAC.Business.Contracts;
using PAC.Entities;
using PAC.WebApplication.Models;

namespace PAC.WebApplication.Controllers
{

    public class VehiculoController : Controller
    {
        //TODO: mover para appsettings
        const string BASE_URL = "http://localhost:57524/";

        public async Task<IActionResult> Index()
        {
            var list = new List<Vehiculo>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(BASE_URL);

                //Define request data format  
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(60);

                //Sending request to find web api REST service resource GetAll using HttpClient  
                HttpResponseMessage response = await client.GetAsync("api/v1/vehiculo");

                //Checking the response is successful or not which is sent using HttpClient  
                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var stringResponse = response.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the list  
                    list = JsonConvert.DeserializeObject<List<Vehiculo>>(stringResponse);
                }
                //returning the list to view  
                return View(list);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vehiculo = new Vehiculo();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(BASE_URL);

                //Define request data format  
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(60);

                //Sending request to find web api REST service resource GetAll using HttpClient  
                HttpResponseMessage response = await client.GetAsync($"api/v1/vehiculo/{id}");

                //Checking the response is successful or not which is sent using HttpClient  
                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var stringResponse = response.Content.ReadAsStringAsync().Result;
                    if (stringResponse != string.Empty) {
                        //Deserializing the response recieved from web api and storing into the list  
                        vehiculo = JsonConvert.DeserializeObject<Vehiculo>(stringResponse);
                    }
                }
                //returning the item to view  
                return View(vehiculo);
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]Vehiculo vehiculo)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(BASE_URL);

                //Define request data format  
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(60);

                //Sending request to find web api REST service resource GetAll using HttpClient  
                HttpResponseMessage response = await client.PostAsJsonAsync($"api/v1/vehiculo", vehiculo);

                //Checking the response is successful or not which is sent using HttpClient  
                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var stringResponse = response.Content.ReadAsStringAsync().Result;
                    if (stringResponse != string.Empty)
                    {
                        //Deserializing the response recieved from web api and storing into the list  
                        vehiculo = JsonConvert.DeserializeObject<Vehiculo>(stringResponse);
                    }
                }
                //returning to list  
                return RedirectToAction("Index");
                //return View(vehiculo);
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Vehiculo vehiculo)
        {
           
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(BASE_URL);

                //Define request data format  
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(60);

                //Sending request to find web api REST service resource GetAll using HttpClient  
                var vehiculoRequest = new VehiculoRequest { IdVehiculo = vehiculo.IdVehiculo, NumeroOrden = vehiculo.NumeroOrden, Placa = vehiculo.Placa };
                HttpResponseMessage response = await client.PutAsJsonAsync($"api/v1/vehiculo", vehiculoRequest);

                //Checking the response is successful or not which is sent using HttpClient  
                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var stringResponse = response.Content.ReadAsStringAsync().Result;
                    if (stringResponse != string.Empty)
                    {
                        //Deserializing the response recieved from web api and storing into the list  
                        vehiculo = JsonConvert.DeserializeObject<Vehiculo>(stringResponse);
                    }
                }
                //returning to list  
                return RedirectToAction("Index");
                //return View(vehiculo);
            }

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
