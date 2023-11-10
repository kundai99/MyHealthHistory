using MedicalWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MedicalWeb.Controllers
{
    public class MedicalVisitController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress= new Uri("http://localhost:70");
            HttpResponseMessage response = await client.GetAsync($"/api/MedicalVisits/{id}");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var finalcontent = JsonConvert.DeserializeObject<MedicalVisit>(content);
              return View(finalcontent);

            }
            return View();
        }

        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FName,SName,Age,Date,Stay,PatientIdnumber,PatientTemperature,PatientWeight,BloodPressure")] TempMedicalVisit Pt)
        {
            MedicalVisit patient = new MedicalVisit();
            patient.BloodPressure = Pt.BloodPressure;
            patient.PatientTemperature = Pt.PatientTemperature;
            patient.PatientWeight = Pt.PatientWeight;
           
            patient.PatientIdnumber = Pt.PatientIdnumber;
            patient.Stay = Pt.Stay;
            patient.Date = Pt.Date.ToString();
     
            patient.HId= 0;
            patient.PId=0;
            patient.DId= 0;
            patient.MId=0;
           
         

            Patientrecord patientrecord = new Patientrecord();
            patientrecord.Prid=0;
            patientrecord.Name = Pt.FName;
            patientrecord.Surname = Pt.SName;
            patientrecord.Age= Pt.Age;
            patientrecord.Idnumber=patient.PatientIdnumber;
            string ptr = System.Text.Json.JsonSerializer.Serialize(patientrecord);
            var ptr2 = new StringContent(ptr, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            string mydata = TempData["userdata"] as string;
            LoggedInUser Lu = JsonConvert.DeserializeObject<LoggedInUser>(mydata);

            client.BaseAddress= new Uri("http://localhost:70");
            string baseurl = "http://localhost:70/api/MedicalVisits";
            string baseurl2 = "http://localhost:70/api/Patientrecords";

            HttpResponseMessage r2 = await client.PostAsync(baseurl2,ptr2);
            if (r2.IsSuccessStatusCode)
            {
                string myTempdata = JsonConvert.SerializeObject(patient);
                TempData["PatientData"] = myTempdata;
                return RedirectToAction("Create2", "MedicalVisit");

            }
            else
            {
                return View(Pt);
            }
            

        }

        public IActionResult Create2()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create2([Bind("Symptoms,Diagnosis,Prescription")] TempMedicalVisit2 Pt)
        {
            string mydata2 = TempData["PatientData"] as string;
            MedicalVisit patient = JsonConvert.DeserializeObject<MedicalVisit>(mydata2);

            patient.Symptoms = Pt.Symptoms;
            patient.Diagnosis = Pt.Diagnosis;
           
            patient.Prescription= Pt.Prescription;
           



            HttpClient client = new HttpClient();
            string mydata = TempData["userdata"] as string;
            LoggedInUser Lu = JsonConvert.DeserializeObject<LoggedInUser>(mydata);

            client.BaseAddress= new Uri("http://localhost:70");
            string baseurl = "http://localhost:70/api/MedicalVisits";
           

            
                patient.DId= Lu.DId;
                HttpResponseMessage r3 = await client.GetAsync($"/api/Doctors/{patient.DId}");
                if (r3.IsSuccessStatusCode)
                {
                    string contnt2 = await r3.Content.ReadAsStringAsync();
                    Doctor Doc = JsonConvert.DeserializeObject<Doctor>(contnt2);
                    patient.HId= Doc.HId;

                    string content = System.Text.Json.JsonSerializer.Serialize(patient);
                    var content2 = new StringContent(content, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(baseurl, content2);

                    if (response.IsSuccessStatusCode)
                    {
                        string myTempdata = JsonConvert.SerializeObject(Lu);
                        TempData["userdata"] = myTempdata;
                        return RedirectToAction("Index", "Doctor");
                    }
                    return View(Pt);
                }

                else
                {
                    return View(Pt);
                }

            


        }
    }



}
