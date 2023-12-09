using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;

namespace CH.DVDCentral.UI.Controllers
{
    public class FormatController : Controller
    {
       

        #region "Pre-WebAPI"
        public IActionResult Index()
        {
            ViewBag.Title = "List of Formats";
            return View(FormatManager.Load());
        }

        public IActionResult Details(int id)
        {
            return View(FormatManager.LoadById(id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Format format)
        {
            try
            {
                int result = FormatManager.Insert(format);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult Edit(int id)
        {
            return View(FormatManager.LoadById(id));
        }

        [HttpPost]
        public IActionResult Edit(int id, Format format, bool rollback = false)
        {
            try
            {
                int result = FormatManager.Update(format, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(format);

            }
        }

        public IActionResult Delete(int id)
        {
            return View(FormatManager.LoadById(id));
        }

        [HttpPost]
        public IActionResult Delete(int id, Format format, bool rollback = false)
        {
            try
            {
                int result = FormatManager.Delete(id, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(format);

            }
        }

        #endregion




        #region "Web-API"
        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            //change the port # find it by running swagger
            client.BaseAddress = new Uri("https://localhost:7197/api/");
            return client;

        }

        public IActionResult Get()
        {
            ViewBag.Title = "List of All Formats";
            HttpClient client = InitializeClient();

            //Call the API
            HttpResponseMessage response = client.GetAsync("Format").Result;

            // Parse the result
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic items = (JArray)JsonConvert.DeserializeObject(result);
            List<Format> programs = items.ToObject<List<Format>>();

            return View(nameof(Index), programs);
        }

        public IActionResult GetOne(int id)
        {
            ViewBag.Title = "Format Details";
            HttpClient client = InitializeClient();

            //Call the API
            HttpResponseMessage response = client.GetAsync("Format/" + id).Result;

            // Parse the result
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic item = JsonConvert.DeserializeObject(result);
            Format format = item.ToObject<Format>();

            return View(nameof(Details), format);

        }

        public IActionResult Insert()
        {
            ViewBag.Title = "Create";
            HttpClient client = InitializeClient();

            //Call the API
            HttpResponseMessage response = client.GetAsync("Format").Result;
            // Parse the result
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic items = (JArray)JsonConvert.DeserializeObject(result);
            //List<DegreeType> degreeTypes = items.ToObject<List<DegreeType>>();

            //ProgramVM programVM = new ProgramVM();
            //programVM.DegreeTypes = degreeTypes;
            //programVM.Program = new BL.Models.Program();
            Format format = new Format();

            return View(nameof(Create), format);
        }


        [HttpPost]
        public IActionResult Insert(Format format)
        {
            try
            {
                HttpClient client = InitializeClient();
                

                string serializeObject = JsonConvert.SerializeObject(format);
                var content = new StringContent(serializeObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                // Call the API
                //The name of controller is in the double quotes
                HttpResponseMessage response = client.PostAsync("Format", content).Result;
                return RedirectToAction(nameof(Get));

            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View(nameof(Create), format);
            }
        }

        public IActionResult Update(int id)
        {
            ViewBag.Title = "Update";
            HttpClient client = InitializeClient();

            //Call the API
            HttpResponseMessage response = client.GetAsync("Format/" + id).Result;

            // Parse the result
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic item = JsonConvert.DeserializeObject(result);
            Format format = item.ToObject<Format>();

            //response = client.GetAsync("Format").Result;

            // Parse the result
            //result = response.Content.ReadAsStringAsync().Result;
            //dynamic items = (JArray)JsonConvert.DeserializeObject(result);
            //List<Format> degreeTypes = items.ToObject<List<Format>>();

            //Format format = new Format();
            
            

            return View(nameof(Edit), format);
        }

        [HttpPost]
        public IActionResult Update(int id, Format format)
        {
            try
            {
                HttpClient client = InitializeClient();
               

                string serializeObject = JsonConvert.SerializeObject(format);
                var content = new StringContent(serializeObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                // Call the API
                //The name of controller is in the double quotes
                HttpResponseMessage response = client.PutAsync("Format/" + id, content).Result;
                return RedirectToAction(nameof(Get));

            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View(nameof(Edit), format);
            }
        }

        public IActionResult Remove(int id)
        {

            HttpClient client = InitializeClient();
            //Call the API
            HttpResponseMessage response = client.GetAsync("Format/" + id).Result;

            // Parse the result
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic item = JsonConvert.DeserializeObject(result);
            Format format = item.ToObject<Format>();

            return View(nameof(Delete), format);

        }

        [HttpPost]
        public IActionResult Remove(int id, Format format)
        {
            try
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.DeleteAsync("Format/" + id).Result;

                return RedirectToAction(nameof(Get));
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View(nameof(Delete), format);
            }
        }













        #endregion


    }
}
