using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using aspcrud1.Models;

namespace aspcrud1.Controllers
{
    public class HomeController : Controller
    {
        string ganon = "https://localhost:44352/api/principal/";
        public ActionResult Index() 
        {
            return View();
        }
        
        public JsonResult TablaPersonas(int Filtro)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ganon + "table");

                var data = new
                {
                    Tabla = Filtro
                };

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var request = client.PostAsync(client.BaseAddress, content);
                request.Wait();

                var respuesta = request.Result.Content.ReadAsStringAsync().Result;
                var dsRespuesta = JObject.Parse(respuesta)["Mensaje"];

                var persona = dsRespuesta["json"].Value<string>();
                var extractResp = JsonConvert.DeserializeObject<List<mTabla>>(persona);

                return Json(extractResp);
            }
            
        }

        public JsonResult TablaPersonasbusqueda(string Busqueda)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ganon + "search");

                var data = new
                {
                    busqueda = Busqueda,
                };

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var request = client.PostAsync(client.BaseAddress, content);
                request.Wait();

                var respuesta = request.Result.Content.ReadAsStringAsync().Result;
                var dsRespuesta = JObject.Parse(respuesta)["Mensaje"];

                var persona = dsRespuesta["json"].Value<string>();

                if (persona == null)
                {
                    return Json("");
                }

                else
                {
                    var extractResp = JsonConvert.DeserializeObject<List<mTabla>>(persona);

                    return Json(extractResp);
                }

                
            }
        }

        public JsonResult GuardarPersona(mPersonas newPersona)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ganon + "create");

                var data = new
                {
                    Nombres = newPersona.Nombre,
                    ApellidoP = newPersona.ApellidoP,
                    ApellidoM = newPersona.ApellidoM,
                    Direccion = newPersona.Direccion,
                    Telefono = newPersona.Telefono,
                };

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var request = client.PostAsync(client.BaseAddress, content);
                request.Wait();

                var respuesta = request.Result.Content.ReadAsStringAsync().Result;
                var dsRespuesta = JObject.Parse(respuesta)["Err"];

                bool extractResp = true;

                return Json(extractResp);
            }
            
        }

        public JsonResult DetallesPersona(int Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ganon + "demo2");

                var data = new
                {
                    IdJson = Id,
                    DatJson = "Hola"
                };

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var request = client.PostAsync(client.BaseAddress, content);
                request.Wait();

                var respuesta = request.Result.Content.ReadAsStringAsync().Result;
                var dsRespuesta = JObject.Parse(respuesta)["Mensaje"];

                var persona = dsRespuesta["json"].Value<string>();
                var extractResp = JsonConvert.DeserializeObject<List<mDatos>>(persona);

                return Json(extractResp);
            }
        }

        public JsonResult Editar(mPersonas newPersona, int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ganon + "update");

                var data = new
                {
                    Id = id,
                    Nombres = newPersona.Nombre,
                    ApellidoP = newPersona.ApellidoP,
                    ApellidoM = newPersona.ApellidoM,
                    Direccion = newPersona.Direccion,
                    Telefono = newPersona.Telefono,
                };

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var request = client.PostAsync(client.BaseAddress, content);
                request.Wait();

                var respuesta = request.Result.Content.ReadAsStringAsync().Result;
                var dsRespuesta = JObject.Parse(respuesta)["Mensaje"];

                var validacion = dsRespuesta["Err"].Value<string>();

                bool extractResp = true;

                if (validacion != "1")
                {
                    extractResp = false;
                }

                return Json(extractResp);
            }
        }

        public JsonResult Eliminar(int Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ganon + "deactivate");

                var data = new
                {
                    Id = Id,
                };

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var request = client.PostAsync(client.BaseAddress, content);
                request.Wait();

                var respuesta = request.Result.Content.ReadAsStringAsync().Result;
                var dsRespuesta = JObject.Parse(respuesta)["Err"];

                bool extractResp = true;

                return Json(extractResp);
            }
        }

        public JsonResult Reactivar(int Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ganon + "deactivate");

                var data = new
                {
                    Id = Id,
                };

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var request = client.PostAsync(client.BaseAddress, content);
                request.Wait();

                var respuesta = request.Result.Content.ReadAsStringAsync().Result;
                var dsRespuesta = JObject.Parse(respuesta)["Err"];

                bool extractResp = true;

                return Json(extractResp);
            }
        }

        public JsonResult CrearClientes(mPersonas newPersona)
        {

            mPersonas Persona = new mPersonas();
            var x = Persona.insertPersona(newPersona);
            return Json(x);
        }
    }
}