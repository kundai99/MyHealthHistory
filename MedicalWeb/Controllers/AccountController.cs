using MedicalWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MedicalWeb.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PDetails()
        {
           
            string mydata = TempData["Pdata"] as string;
            Patientrecord Lu = JsonConvert.DeserializeObject<Patientrecord>(mydata);
          
              
                return View(Lu);


        }

        public async Task<IActionResult> DocDetails()
        {
            string mydata = TempData["userdata"] as string;
            LoggedInUser Lu = JsonConvert.DeserializeObject<LoggedInUser>(mydata);

            HttpClient client = new HttpClient();
            client.BaseAddress= new Uri("http://localhost:70");
            HttpResponseMessage response = await client.GetAsync($"/api/Doctors/{Lu.DId}");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var finalcontent = JsonConvert.DeserializeObject<Doctor>(content);
                string Name = finalcontent.Name+" "+finalcontent.Surname;
                HttpResponseMessage response2 = await client.GetAsync($"/api/Patients/PatientPrescriptionDOC?Pid={Name}");
                if (response2.IsSuccessStatusCode)
                {
                    string content2 = await response.Content.ReadAsStringAsync();

                    var finalcontent2 = JsonConvert.DeserializeObject<Doctorinfo>(content2);
                    return View(finalcontent2);
                }
               

            }
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Username,Password")] Login Pt)
        {
            HttpClient client = new HttpClient();
             Account a = new Account();
            a.Username = Pt.Username;
            a.Password = Pt.Password;
            if(Pt.Username== null )
            {
                return View();
            }
            if (Pt.Username== "")
            {
                return View();
            }

            if (Pt.Password== null)
            {
                return View();
            }
            if (Pt.Password== "")
            {
                return View();
            }
            string baseurl = "http://localhost:70/api/login";
            client.BaseAddress= new Uri("http://localhost:70");
            // client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue ("application/json"));
            string content = System.Text.Json.JsonSerializer.Serialize(a);
            var content2 = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(baseurl, content2);

            if (response.IsSuccessStatusCode)
            {
                LoggedInUser Lu = new LoggedInUser();

                HttpResponseMessage r2 = await client.GetAsync($"/api/login/itemsCategory?uName={a.Username}");
                if (r2.IsSuccessStatusCode)
                {
                    string contnt = await r2.Content.ReadAsStringAsync();
                    long x4 = JsonConvert.DeserializeObject<long>(contnt);


                    Lu.Id=x4;

                    HttpResponseMessage r3 = await client.GetAsync($"/api/login/Acctype?uName={a.Username}");
                    if (r3.IsSuccessStatusCode)
                    {
                        string contnt2 = await r3.Content.ReadAsStringAsync();

                        Lu.AccType= contnt2;

                       
                    }

                }

                string Cat = Lu.AccType.ToUpper();
                if (Cat.Equals("ADMIN"))
                {

                    
                        return RedirectToAction("Index", "PrivatePractice");
                   
                }
                else if (Cat.Equals("CAD")) { return RedirectToAction("Index", "Clinic"); }
                else if (Cat.Equals("HAD")) { return RedirectToAction("Index", "Hospital"); }
                else if (Cat.Equals("PPAD")) { return RedirectToAction("Index", "PrivatePractice"); }
                else
                {
                    HttpResponseMessage r4 = await client.GetAsync($"api/Login/DidBYaccID?AccId={Lu.Id}");
                    if (r4.IsSuccessStatusCode)
                    {
                        string contnt = await r4.Content.ReadAsStringAsync();
                        long x5 = JsonConvert.DeserializeObject<long>(contnt);
                        Lu.DId =x5;
                        string myTempdata = JsonConvert.SerializeObject(Lu);
                        TempData["userdata"] = myTempdata;
                        return RedirectToAction("Index", "Doctor");
                    }
                    else
                    {
                        return View(Pt);
                    }

                }


            }
            else
            {
                TempData["Error"]= "Invalid login details";
                return View(Pt);
            }

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Username")] string Username, [Bind("Password")] string Password, [Bind("Category")] string Category)
        {
            LoggedInUser Lu = new LoggedInUser();
            AccWithCategory account = new AccWithCategory();
            account.Username = Username;
            account.Password = Password;
            account.Category = Category;

            Account account1 = new Account();
            account1.Username = Username;
            account1.Password = Password;
             
            HttpClient client = new HttpClient();
            string baseurl = "http://localhost:70/api/Accounts";
            client.BaseAddress= new Uri("http://localhost:70");
            // client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue ("application/json"));
            string content = System.Text.Json.JsonSerializer.Serialize(account);
            var content2 = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(baseurl, content2);

            if (response.IsSuccessStatusCode)
            {
                
               
              
                    HttpResponseMessage r3 = await client.GetAsync($"/api/login/AccID?uName={account.Username}");
                if (r3.IsSuccessStatusCode)
                {
                    string con= await r3.Content.ReadAsStringAsync();

                    long accid = JsonConvert.DeserializeObject<long>(con);
                    Lu.Id = accid;
                    string myTempdata = JsonConvert.SerializeObject(Lu);
                    TempData["userdata"] = myTempdata;

                    string Cat = account.Category.ToUpper();
                    if (Cat.Equals("PATIENT"))
                    {

                        return RedirectToAction("Create", "Patient");


                    }
                   
                    else
                    {
                        
                        return RedirectToAction("Create", "Doctor");

                    }
                }

                


                return RedirectToAction("Index");
            }
            return View(account);

        }
    }
}
