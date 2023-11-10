using MedicalWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MedicalWeb.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

       


        public IActionResult PatientLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PatientLogin([Bind("Username")] PLogin Pt)
        {
            HttpClient client = new HttpClient();
            Account a = new Account();
            a.Username = Pt.Username;
           
            if (Pt.Username== null)
            {
                return View();
            }
            if (Pt.Username== "")
            {
                return View();
            }

           
            client.BaseAddress= new Uri("http://localhost:70");
           
                LoggedInUser Lu = new LoggedInUser();
                
                HttpResponseMessage r2 = await client.GetAsync($"/api/Patientrecords/ID{a.Username}");
                if (r2.IsSuccessStatusCode)
                {
                    string contnt = await r2.Content.ReadAsStringAsync();
                    Patientrecord x4 = JsonConvert.DeserializeObject<Patientrecord>(contnt);

                    string myTempdata = JsonConvert.SerializeObject(x4);
                    TempData["Pdata"] = myTempdata;
                    return RedirectToAction("PatientPrescriptions", "Patient");
                   

                   

                }

               
                else
                {

                    TempData["Error"]= "Non existing ID";
                    return View(Pt);
                }


           

        }


        public async Task<IActionResult> PatientPrescriptions()
        {
            string mydata = TempData["Pdata"] as string;
            Patientrecord Lu = JsonConvert.DeserializeObject<Patientrecord>(mydata);
            HttpClient client = new HttpClient();
            client.BaseAddress= new Uri("http://localhost:70");
            string baseurl = "http://localhost:70";
            HttpResponseMessage response = await client.GetAsync($"/api/Patients/PatientPrescriptionsBYID?Pid={Lu.Idnumber}");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var finalcontent = JsonConvert.DeserializeObject<List<PatientPrescription>>(content);
                string myTempdata = JsonConvert.SerializeObject(Lu);
                TempData["Pdata"] = myTempdata;
                return View(finalcontent);
            }
            return StatusCode((int)response.StatusCode);

        }

        public async Task<IActionResult> PatientMedicalVisits()
        {
            string mydata = TempData["userdata"] as string;
            LoggedInUser Lu = JsonConvert.DeserializeObject<LoggedInUser>(mydata);
            HttpClient client = new HttpClient();
            client.BaseAddress= new Uri("http://localhost:70");
            string baseurl = "http://localhost:70";
            HttpResponseMessage response = await client.GetAsync($"/api/MedicalVisits/PatientCategory?Pid={Lu.PId}");
          
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var finalcontent = JsonConvert.DeserializeObject<List<MedicalVisit>>(content);
                string myTempdata = JsonConvert.SerializeObject(Lu);
                TempData["userdata"] = myTempdata;
                return View(finalcontent);
            }
            return StatusCode((int)response.StatusCode);
            
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Surname,Id,Sex,Race,Age")] TempAcc Pt)
        {
            string mydata = TempData["userdata"] as string;
            LoggedInUser Lu = JsonConvert.DeserializeObject<LoggedInUser>(mydata);
            Patient patient = new Patient();
            patient.Name = Pt.Name;
            patient.Surname = Pt.Surname;
            patient.Sex = Pt.Sex;
            patient.Age = Pt.Age;
            patient.Race= Pt.Race;
            patient.Id = Pt.Id;
            patient.AccId=0;
            patient.AccId= Lu.Id;
            HttpClient client = new HttpClient();
            string baseurl = "http://localhost:70/api/Patients";
            // client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue ("application/json"));
            string content = System.Text.Json.JsonSerializer.Serialize(patient);
            var content2 = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(baseurl, content2);

            if (response.IsSuccessStatusCode)
            {
                string myTempdata = JsonConvert.SerializeObject(Lu);
                TempData["userdata"] = myTempdata;
                return RedirectToAction("Index", "Account");
            }
            return View(patient);

        }


    }
}
