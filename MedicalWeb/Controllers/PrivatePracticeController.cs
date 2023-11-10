using MedicalWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MedicalWeb.Controllers
{
    public class PrivatePracticeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("HospitalName")] string HospitalName, [Bind("Location")] string Location)
        {
            Hospital H = new Hospital();
            H.HospitalName= HospitalName;
            H.Location= Location;
            H.Type=3;
            HttpClient client = new HttpClient();
            string baseurl = "http://localhost:70/api/Hospitals";
            // client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue ("application/json"));
            string content = System.Text.Json.JsonSerializer.Serialize(H);
            var content2 = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(baseurl, content2);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(H);

        }
        public async Task<IActionResult> DocCreate(string id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress= new Uri("http://localhost:70");
            string baseurl = "http://localhost:70";
            HttpResponseMessage response = await client.GetAsync($"/api/Hospitals/{id}");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var finalcontent = JsonConvert.DeserializeObject<Hospital>(content);
                List<Hospital> hospitals = new List<Hospital>();
                hospitals.Add(finalcontent);
                Tempdata3 t2 = new Tempdata3();
                t2.Hospitals= hospitals;
                return View(t2);
            }
            else
            {
                return StatusCode((int)response.StatusCode);

            }

        }

        [HttpPost]
        public async Task<IActionResult> DocCreate([Bind("Name,Surname,ProfessionalNum,Speciality,Username,Password")] TempData4 Pt, int Hospital)
        {
            Doctor d = new Doctor();
            d.Name = Pt.Name;
            d.Surname = Pt.Surname;
            d.ProfessionalNum = Pt.ProfessionalNum;
            d.Speciality = Pt.Speciality;
            d.HId=0;
            d.AccId=0;
            d.HId= Hospital;
            Account D = new Account();
            D.Username= Pt.Username;
            D.Password= Pt.Password;
            D.Category="DOCTOR";


            HttpClient client = new HttpClient();
            string baseurl = "http://localhost:70/api/Accounts";
            client.BaseAddress= new Uri("http://localhost:70");

            string content = System.Text.Json.JsonSerializer.Serialize(D);
            var content2 = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(baseurl, content2);
            if (response.IsSuccessStatusCode)
            {
                string AccR = await response.Content.ReadAsStringAsync();

                var AccR2 = JsonConvert.DeserializeObject<Account>(AccR);
                d.AccId=AccR2.AccId;
                string baseurl2 = "http://localhost:70/api/Doctors";
                string content4 = System.Text.Json.JsonSerializer.Serialize(d);
                var content5 = new StringContent(content4, Encoding.UTF8, "application/json");
                HttpResponseMessage response2 = await client.PostAsync(baseurl2, content5);

                if (response2.IsSuccessStatusCode)
                {

                    return RedirectToAction("ViewHospitals", "Hospital");
                }
            }



            return View(d);

        }

        public async Task<IActionResult> ViewDocs(long id)
        {


            HttpClient client = new HttpClient();
            client.BaseAddress= new Uri("http://localhost:70");
            string baseurl = "http://localhost:70";


            HttpResponseMessage response = await client.GetAsync($"/api/Hospitals/PatientCategoryBYHID?Hid={id}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var finalcontent = JsonConvert.DeserializeObject<List<Doctorinfo>>(content);


                return View(finalcontent);
            }
            return StatusCode((int)response.StatusCode);

        }
        public async Task<IActionResult> ViewHospitals()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress= new Uri("http://localhost:70");
            HttpResponseMessage response = await client.GetAsync($"/api/Hospitals/GetPrivatePractice");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var finalcontent = JsonConvert.DeserializeObject<List<Hospital>>(content);
                return View(finalcontent);

            }
            return View();
        }
    }
}

