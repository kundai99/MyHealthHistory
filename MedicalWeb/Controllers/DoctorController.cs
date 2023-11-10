using MedicalWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace MedicalWeb.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Create()
        {
            HttpClient client = new HttpClient();
            string baseurl = "http://localhost:70/api/Items";
            HttpResponseMessage response = await client.GetAsync(baseurl);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var finalcontent = JsonConvert.DeserializeObject<List<Hospital>>(content);
                Tempdata2 t2 = new Tempdata2();
                t2.Hospitals= finalcontent;
                return View(t2);
            }
            else
            {
                return StatusCode((int)response.StatusCode);

            }
            
        }

        public async Task<IActionResult> Details(string id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress= new Uri("http://localhost:70");
            HttpResponseMessage response = await client.GetAsync($"/api/Patients/PatientPrescriptionDOC?Pid={id}");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var finalcontent = JsonConvert.DeserializeObject<Doctorinfo>(content);
                return View(finalcontent);

            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Surname,ProfessionalNum,Speciality")] TempData Pt, int Hospital)
        {
            Doctor d = new Doctor();
            d.Name = Pt.Name;
            d.Surname = Pt.Surname;
            d.ProfessionalNum = Pt.ProfessionalNum;
            d.Speciality = Pt.Speciality;
            d.HId=0;
            d.AccId=0;
            d.HId= Hospital;
            string mydata = TempData["userdata"] as string;
            LoggedInUser Lu = JsonConvert.DeserializeObject<LoggedInUser>(mydata);
            d.AccId= Lu.Id;
            HttpClient client = new HttpClient();
            string baseurl = "http://localhost:70/api/Doctors";
            // client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue ("application/json"));
            string content = System.Text.Json.JsonSerializer.Serialize(d);
            var content2 = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(baseurl, content2);

            if (response.IsSuccessStatusCode)
            {
                string myTempdata = JsonConvert.SerializeObject(Lu);
                TempData["userdata"] = myTempdata;
                return RedirectToAction("Index","Account");
            }
            return View(d);

        }


        public async Task< IActionResult> ViewPatients()
        {
            string mydata = TempData["userdata"] as string;
            LoggedInUser Lu = JsonConvert.DeserializeObject<LoggedInUser>(mydata);
            HttpClient client = new HttpClient();
            client.BaseAddress= new Uri("http://localhost:70");
            string baseurl = "http://localhost:70";
           
            HttpResponseMessage response = await client.GetAsync($"/api/Doctors/itemsCategoryBYID?Did={Lu.DId}");
           
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var finalcontent = JsonConvert.DeserializeObject<List<Patientrecord>>(content);
                string myTempdata = JsonConvert.SerializeObject(Lu);
                TempData["userdata"] = myTempdata;
                return View(finalcontent);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
                //  return View(finalcontent);
            }
          
        }

       

        public IActionResult AddLabReport(int id) 
        {
            string myTempdata = JsonConvert.SerializeObject(id);
            TempData["currentMedicalVisit"] = myTempdata;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddLabReport(UploadingFile F)
        {

            string mydata = TempData["currentMedicalVisit"] as string;
            long mid = JsonConvert.DeserializeObject<long>(mydata);
            LabResult Lb = new LabResult();

            Lb.MId = mid;
            Lb.DocumentName = F.DocumentName;
            var fileName = Path.GetFileName(F.file.FileName);
            var uniqueFileName = $"{Guid.NewGuid().ToString()}_{fileName}";
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "LabReulst");
            if (!Directory.Exists(uploadPath))
            { Directory.CreateDirectory(uploadPath); }
            var filePath = Path.Combine(uploadPath, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create)) { await F.file.CopyToAsync(fileStream); }
            Lb.DocumentPath = uniqueFileName.ToString();
            HttpClient client = new HttpClient();
            string baseurl = "http://localhost:70/api/LabResults";
            // client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue ("application/json"));
            string content = System.Text.Json.JsonSerializer.Serialize(Lb);
            var content2 = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(baseurl, content2);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(F);

          
        }

        public async Task<IActionResult> ViewLabReports(int id)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress= new Uri("http://localhost:70");
            string baseurl = "http://localhost:70";
            HttpResponseMessage response = await client.GetAsync($"/api/LabResults/LabResultsByMID?mid={id}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var finalcontent = JsonConvert.DeserializeObject<List<LabResult>>(content);
                return View(finalcontent);
            }
            return View();
        }

        public async Task<IActionResult> PMedicalVisitOtherD(string id)
        {
            string mydata = TempData["userdata"] as string;
            LoggedInUser Lu = JsonConvert.DeserializeObject<LoggedInUser>(mydata);
            long DiD = Lu.DId;
           
                HttpClient client = new HttpClient();
            client.BaseAddress= new Uri("http://localhost:70");
            string baseurl = "http://localhost:70";


            HttpResponseMessage response = await client.GetAsync($"/api/MedicalVisits/PatientCategoryBYIDNumberandDIDList?Pid={id}&Did={DiD}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
               
                var finalcontent = JsonConvert.DeserializeObject<List<Doctorinfo>>(content);
                string myTempdata = JsonConvert.SerializeObject(Lu);
                TempData["userdata"] = myTempdata;
				string myTempdata2 = JsonConvert.SerializeObject(id);
				TempData["PatientID"] = myTempdata2;
                TempData["PatientID2"] = myTempdata2;
                return View(finalcontent);
            }
            return StatusCode((int)response.StatusCode);

        }
        public async Task<IActionResult> PMedicalVisit(string id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress= new Uri("http://localhost:70");
            string baseurl = "http://localhost:70";

            string mydata = TempData["userdata"] as string;
            LoggedInUser Lu = JsonConvert.DeserializeObject<LoggedInUser>(mydata);
            long DiD = Lu.DId;
            HttpResponseMessage response = await client.GetAsync($"/api/MedicalVisits/PatientCategoryBYIDNumberandDID?Pid={id}&Did={DiD}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var finalcontent = JsonConvert.DeserializeObject<List<MedicalVisit>>(content);
                string myTempdata = JsonConvert.SerializeObject(Lu);
                TempData["userdata"] = myTempdata;
                string myTempdata2 = JsonConvert.SerializeObject(id);
                TempData["PatientID"] = myTempdata2;
                TempData["PatientID2"] = myTempdata2;
                return View(finalcontent);
            }
            return StatusCode((int)response.StatusCode);
        }


        public async Task<IActionResult> PMedicalVisit2(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress= new Uri("http://localhost:70");
            string baseurl = "http://localhost:70";

            string mydata = TempData["PatientID"] as string;
            string PID = JsonConvert.DeserializeObject<string>(mydata);
            HttpResponseMessage response = await client.GetAsync($"/api/MedicalVisits/PatientCategoryBYIDNumberandDID?Pid={PID}&Did={id}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                string myTempdata2 = JsonConvert.SerializeObject(id);
                TempData["DocID"] = myTempdata2;
                var finalcontent = JsonConvert.DeserializeObject<List<MedicalVisit>>(content);
               
                return View(finalcontent);
            }
            return StatusCode((int)response.StatusCode);
        }
    }
}
