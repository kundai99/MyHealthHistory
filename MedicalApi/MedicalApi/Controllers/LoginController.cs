using MedicalApi.ViewModel;
using MedicalDatabase.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace MedicalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly MedicalDatabaseContext _context;
        public LoginController(MedicalDatabaseContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            var user = await _context.Accounts.FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);

            if (user!= null)
            {
                return Ok(new { Message = "Login successful" });
            }
            return Unauthorized(new { Message = "Invalid credentials" });
        }

        [HttpGet("itemsCategory")]
        public async Task<ActionResult<long>> GetID(string uName)
        {

            var user = await _context.Accounts.FirstOrDefaultAsync(u => u.Username == uName);

            if (user== null)
            {
                return NotFound();
            }




            return user.AccId;


        }

        [HttpGet("Acctype")]
        public async Task<ActionResult<string>> GetAccType(string uName)
        {

            var user = await _context.Accounts.FirstOrDefaultAsync(u => u.Username == uName);

            if (user== null)
            {
                return NotFound();
            }




            return user.Category;


        }


        [HttpGet("AccID")]
        public async Task<ActionResult<long>> GetAccID(string uName)
        {

            var user = await _context.Accounts.FirstOrDefaultAsync(u => u.Username == uName);

            if (user== null)
            {
                return NotFound();
            }




            return user.AccId;


        }

        [HttpGet("PidBYaccID")]
        public async Task<ActionResult<long>> GetPID(long AccId)
        {

            var user = await _context.Patients.FirstOrDefaultAsync(u => u.AccId == AccId);

            if (user== null)
            {
                return NotFound();
            }




            return user.PId;


        }

        [HttpGet("DidBYaccID")]
        public async Task<ActionResult<long>> GetDID(long AccId)
        {

            var user = await _context.Doctors.FirstOrDefaultAsync(u => u.AccId == AccId);

            if (user== null)
            {
                return NotFound();
            }




            return user.DId;


        }
    }
}
