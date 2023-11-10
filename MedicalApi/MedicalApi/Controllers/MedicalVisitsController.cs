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
    public class MedicalVisitsController : ControllerBase
    {
        private readonly MedicalDatabaseContext _context;

        public MedicalVisitsController(MedicalDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/MedicalVisits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalVisit>>> GetMedicalVisits()
        {
            if (_context.MedicalVisits == null)
            {
                return NotFound();
            }
            return await _context.MedicalVisits.ToListAsync();
        }

        // GET: api/MedicalVisits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalVisit>> GetMedicalVisit(long id)
        {
            if (_context.MedicalVisits == null)
            {
                return NotFound();
            }
            var medicalVisit = await _context.MedicalVisits.FindAsync(id);

            if (medicalVisit == null)
            {
                return NotFound();
            }

            return medicalVisit;
        }

        // PUT: api/MedicalVisits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicalVisit(long id, MedicalVisit medicalVisit)
        {
            if (id != medicalVisit.MId)
            {
                return BadRequest();
            }

            _context.Entry(medicalVisit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicalVisitExists(id))
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

        // POST: api/MedicalVisits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MedicalVisit>> PostMedicalVisit(MedicalVisit medicalVisit)
        {
            if (_context.MedicalVisits == null)
            {
                return Problem("Entity set 'MedicalDatabaseContext.MedicalVisits'  is null.");
            }
            _context.MedicalVisits.Add(medicalVisit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedicalVisit", new { id = medicalVisit.MId }, medicalVisit);
        }

        // DELETE: api/MedicalVisits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalVisit(long id)
        {
            if (_context.MedicalVisits == null)
            {
                return NotFound();
            }
            var medicalVisit = await _context.MedicalVisits.FindAsync(id);
            if (medicalVisit == null)
            {
                return NotFound();
            }

            _context.MedicalVisits.Remove(medicalVisit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("itemsCategory")]
        public async Task<ActionResult<IEnumerable<MedicalVisit>>> GetDMedVisits(long Did)
        {
            if (_context.Patients == null)
            {
                return NotFound();
            }


            var MedVisits = await _context.MedicalVisits.Where(o => o.DId == Did).ToListAsync();

            if (MedVisits == null)
            {
                return NotFound();
            }



            //List<MedicalVisit> medicalVisits = MedVisits.ToList();
            return MedVisits.ToList();
        }

        [HttpGet("PatientCategory")]
        public async Task<ActionResult<IEnumerable<MedicalVisit>>> GetPMedVisits(long Pid)
        {
            if (_context.Patients == null)
            {
                return NotFound();
            }


            var MedVisits = await _context.MedicalVisits.Where(o => o.PId == Pid).ToListAsync();

            if (MedVisits == null)
            {
                return NotFound();
            }



            //List<MedicalVisit> medicalVisits = MedVisits.ToList();
            return MedVisits.ToList();
        }

        [HttpGet("PatientCategoryBYIDNumber")]
        public async Task<ActionResult<IEnumerable<MedicalVisit>>> GetPMedVisitsbyID(string Pid)
        {
            if (_context.Patients == null)
            {
                return NotFound();
            }


            var MedVisits = await _context.MedicalVisits.Where(o => o.PatientIdnumber == Pid).ToListAsync();

            if (MedVisits == null)
            {
                return NotFound();
            }



            //List<MedicalVisit> medicalVisits = MedVisits.ToList();
            return MedVisits.ToList();
        }


        [HttpGet("PatientCategoryBYIDNumberandDID")]
        public async Task<ActionResult<IEnumerable<MedicalVisit>>> GetPMedVisitsbyIDandDID(string Pid, long Did)
        {
            if (_context.Patients == null)
            {
                return NotFound();
            }


            var MedVisits = await _context.MedicalVisits.Where(o => o.PatientIdnumber == Pid && o.DId == Did).ToListAsync();

            if (MedVisits == null)
            {
                return NotFound();
            }



            //List<MedicalVisit> medicalVisits = MedVisits.ToList();
            return MedVisits.ToList();
        }

        [HttpGet("PatientCategoryBYIDNumberandDIDList")]
        public async Task<ActionResult<IEnumerable<Doctorinfo>>> GetPMedVisitsbyIDandDIDList(string Pid, long Did)
        {
            if (_context.Patients == null)
            {
                return NotFound();
            }


            var MedVisits = await _context.MedicalVisits.Where(o => o.PatientIdnumber == Pid && o.DId != Did).ToListAsync();

            if (MedVisits == null)
            {
                return NotFound();
            }
            List<Doctorinfo> Docs = new List<Doctorinfo>();
            foreach (var item in MedVisits)
            {
                var doctor = await _context.Doctors.FindAsync(item.DId);
                Doctorinfo di = new Doctorinfo();
                di.Did = doctor.DId;
                di.SName= doctor.Surname;
                di.FName= doctor.Name;
                di.ProfessionalNum=doctor.ProfessionalNum;
                di.Speciality= doctor.Speciality;
                var hospital = await _context.Hospitals.FindAsync(doctor.HId);
                di.HospitalName= hospital.HospitalName;
                var Tempdoc = Docs.Find(d => d.Did == di.Did);
                if (Tempdoc== null)
                {
                    Docs.Add(di);
                }


            }



            //List<MedicalVisit> medicalVisits = MedVisits.ToList();
            return Docs;
        }

        private bool MedicalVisitExists(long id)
        {
            return (_context.MedicalVisits?.Any(e => e.MId == id)).GetValueOrDefault();
        }
    }
}
