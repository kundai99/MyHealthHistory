using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalDatabase.Data;
using MedicalDatabase.Models;
using System.Numerics;
using MedicalApi.ViewModel;

namespace MedicalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly MedicalDatabaseContext _context;

        public DoctorsController(MedicalDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Doctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            if (_context.Doctors == null)
            {
                return NotFound();
            }
            return await _context.Doctors.ToListAsync();
        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(long id)
        {
            if (_context.Doctors == null)
            {
                return NotFound();
            }
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        // PUT: api/Doctors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(long id, Doctor doctor)
        {
            if (id != doctor.DId)
            {
                return BadRequest();
            }

            _context.Entry(doctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
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

        // POST: api/Doctors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Doctor>> PostDoctor(Doctor doctor)
        {
            if (_context.Doctors == null)
            {
                return Problem("Entity set 'MedicalDatabaseContext.Doctors'  is null.");
            }
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoctor", new { id = doctor.DId }, doctor);
        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(long id)
        {
            if (_context.Doctors == null)
            {
                return NotFound();
            }
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("itemsCategory")]
        public async Task<ActionResult<IEnumerable<Patient>>> GetCurrentPatients(long Did)
        {
            if (_context.Patients == null)
            {
                return NotFound();
            }

            var Doctor = await _context.Doctors.FindAsync(Did);
            var MedVisits = await _context.MedicalVisits.Where(o => o.DId == Doctor.DId).ToListAsync();

            if (MedVisits == null)
            {
                return NotFound();
            }
            if (Doctor == null)
            {
                return NotFound();
            }
            List<Patient> finlist = new List<Patient>();
            foreach (var Md in MedVisits)
            {

                if (Md.DId== Doctor.DId)
                {
                    var Pnt = await _context.Patients.FindAsync(Md.PId);
                    finlist.Add(Pnt);
                }
            }

            return finlist;
        }

        [HttpGet("itemsCategoryBYID")]
        public async Task<ActionResult<IEnumerable<Patientrecord>>> GetCurrentPatientsbyID(long Did)
        {
            if (_context.Patients == null)
            {
                return NotFound();
            }

            var Doctor = await _context.Doctors.FindAsync(Did);
            var MedVisits = await _context.MedicalVisits.Where(o => o.DId == Doctor.DId).ToListAsync();

            if (MedVisits == null)
            {
                return NotFound();
            }
            if (Doctor == null)
            {
                return NotFound();
            }

            List<Patientrecord> finlist2 = new List<Patientrecord>();
            foreach (var Md in MedVisits)
            {

                if (Md.DId== Doctor.DId)
                {
                    var Pnt = await _context.Patientrecords.Where(o => o.Idnumber == Md.PatientIdnumber).FirstOrDefaultAsync();
                    if (!finlist2.Contains(Pnt))
                    {
                        finlist2.Add(Pnt);
                    }

                }
            }

            return finlist2;
        }

        [HttpGet("DocCategory")]
        public async Task<ActionResult<Doctor>> GetPMedVisits(Name Doctor)
        {
            if (_context.Doctors == null)
            {
                return NotFound();
            }


            var Doc = await _context.Doctors.Where(o => o.Name == Doctor.FName && o.Surname == Doctor.SName).FirstOrDefaultAsync();

            if (Doc == null)
            {
                return NotFound();
            }



            //List<MedicalVisit> medicalVisits = MedVisits.ToList();
            return Doc;
        }

        private bool DoctorExists(long id)
        {
            return (_context.Doctors?.Any(e => e.DId == id)).GetValueOrDefault();
        }
    }
}
