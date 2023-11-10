using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalDatabase.Data;
using MedicalDatabase.Models;
using MedicalApi.ViewModel;

namespace MedicalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalsController : ControllerBase
    {
        private readonly MedicalDatabaseContext _context;

        public HospitalsController(MedicalDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Hospitals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hospital>>> GetHospitals()
        {
            if (_context.Hospitals == null)
            {
                return NotFound();
            }
            return await _context.Hospitals.ToListAsync();
        }

        // GET: api/Hospitals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hospital>> GetHospital(long id)
        {
            if (_context.Hospitals == null)
            {
                return NotFound();
            }
            var hospital = await _context.Hospitals.FindAsync(id);

            if (hospital == null)
            {
                return NotFound();
            }

            return hospital;
        }

        // PUT: api/Hospitals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospital(long id, Hospital hospital)
        {
            if (id != hospital.HId)
            {
                return BadRequest();
            }

            _context.Entry(hospital).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Hospitals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hospital>> PostHospital(Hospital hospital)
        {
            if (_context.Hospitals == null)
            {
                return Problem("Entity set 'MedicalDatabaseContext.Hospitals'  is null.");
            }
            _context.Hospitals.Add(hospital);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospital", new { id = hospital.HId }, hospital);
        }

        // DELETE: api/Hospitals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHospital(long id)
        {
            if (_context.Hospitals == null)
            {
                return NotFound();
            }
            var hospital = await _context.Hospitals.FindAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }

            _context.Hospitals.Remove(hospital);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("PatientCategoryBYHID")]
        public async Task<ActionResult<IEnumerable<Doctorinfo>>> GetDocsBYHID(long Hid)
        {
            if (_context.Patients == null)
            {
                return NotFound();
            }


            var Docs = await _context.Doctors.Where(o => o.HId ==  Hid).ToListAsync();

            if (Docs == null)
            {
                return NotFound();
            }
            List<Doctorinfo> Docs2 = new List<Doctorinfo>();
            foreach (var item in Docs)
            {

                Doctorinfo di = new Doctorinfo();
                di.Did = item.DId;
                di.SName= item.Surname;
                di.FName= item.Name;
                di.ProfessionalNum=item.ProfessionalNum;
                di.Speciality= item.Speciality;
                var hospital = await _context.Hospitals.FindAsync(item.HId);
                di.HospitalName= hospital.HospitalName;

                Docs2.Add(di);
            }




            return Docs2;
        }

        [HttpGet("GetClinic")]
        public async Task<ActionResult<IEnumerable<Hospital>>> GetClinics()
        {
            if (_context.Hospitals == null)
            {
                return NotFound();
            }
            return await _context.Hospitals.Where(H=> H.Type == 2).ToListAsync();
        }

        [HttpGet("GetPrivatePractice")]
        public async Task<ActionResult<IEnumerable<Hospital>>> GetPrivatePractice()
        {
            if (_context.Hospitals == null)
            {
                return NotFound();
            }
            return await _context.Hospitals.Where(H => H.Type == 3).ToListAsync();
        }

        [HttpGet("GetHospital")]
        public async Task<ActionResult<IEnumerable<Hospital>>> Gethospital()
        {
            if (_context.Hospitals == null)
            {
                return NotFound();
            }
            return await _context.Hospitals.Where(H => H.Type == 1).ToListAsync();
        }


        private bool HospitalExists(long id)
        {
            return (_context.Hospitals?.Any(e => e.HId == id)).GetValueOrDefault();
        }
    }
}
