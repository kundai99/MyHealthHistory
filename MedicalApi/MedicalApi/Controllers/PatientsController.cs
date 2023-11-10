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
    public class PatientsController : ControllerBase
    {
        private readonly MedicalDatabaseContext _context;

        public PatientsController(MedicalDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            if (_context.Patients == null)
            {
                return NotFound();
            }
            return await _context.Patients.ToListAsync();
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(long id)
        {
            if (_context.Patients == null)
            {
                return NotFound();
            }
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        // PUT: api/Patients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(long id, Patient patient)
        {
            if (id != patient.PId)
            {
                return BadRequest();
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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

        // POST: api/Patients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
            if (_context.Patients == null)
            {
                return Problem("Entity set 'MedicalDatabaseContext.Patients'  is null.");
            }
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatient", new { id = patient.PId }, patient);
        }

        [HttpGet("PatientPrescriptions")]
        public async Task<ActionResult<IEnumerable<PatientPrescription>>> GetPrescriptions(long Pid)
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
            List<PatientPrescription> prescriptions = new List<PatientPrescription>();
            foreach (var md in MedVisits)
            {
                PatientPrescription P = new PatientPrescription();
                P.Prescription= md.Prescription;
                P.Date = md.Date;
                var Doc = await _context.Doctors.Where(o => o.DId == md.DId).FirstOrDefaultAsync();
                P.DoctorName=Doc.Name+" "+Doc.Surname;
                P.Description=md.Diagnosis;
                prescriptions.Add(P);
            }


            //List<MedicalVisit> medicalVisits = MedVisits.ToList();
            return prescriptions.ToList();
        }


        [HttpGet("PatientPrescriptionsBYID")]
        public async Task<ActionResult<IEnumerable<PatientPrescription>>> GetPrescriptionsBYID(string Pid)
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
            List<PatientPrescription> prescriptions = new List<PatientPrescription>();
            foreach (var md in MedVisits)
            {
                PatientPrescription P = new PatientPrescription();
                P.Prescription= md.Prescription;
                P.Date = md.Date;
                var Doc = await _context.Doctors.Where(o => o.DId == md.DId).FirstOrDefaultAsync();
                P.DoctorName=Doc.Name+" "+Doc.Surname;
                prescriptions.Add(P);
            }


            //List<MedicalVisit> medicalVisits = MedVisits.ToList();
            return prescriptions.ToList();
        }


        [HttpGet("PatientPrescriptionDOC")]
        public async Task<ActionResult<Doctorinfo>> GetDocBYPrescrip(string Pid)
        {
            if (_context.Patients == null)
            {
                return NotFound();
            }

            var Docs = await _context.Doctors.ToListAsync();
            foreach (var doc in Docs)
            {
                string D = doc.Name+" "+doc.Surname;
                if (D.Equals(Pid))
                {
                    var Hosp = await _context.Hospitals.Where(H => H.HId == doc.HId).FirstOrDefaultAsync();
                    if (Hosp != null)
                    {
                        Doctorinfo Di = new Doctorinfo();
                        Di.Did= doc.DId;
                        Di.ProfessionalNum= doc.ProfessionalNum;
                        Di.HospitalName= Hosp.HospitalName;
                        Di.FName=doc.Name;
                        Di.SName=doc.Surname;
                        Di.Speciality=doc.Speciality;
                        return Di;
                    }
                }
            }

            return NotFound();

        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(long id)
        {
            if (_context.Patients == null)
            {
                return NotFound();
            }
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("PatientCategory")]
        public async Task<ActionResult<long>> GetPMedVisits(string FName, String SName)
        {
            if (_context.Patients == null)
            {
                return NotFound();
            }


            var Doc = await _context.Patients.FirstOrDefaultAsync(o => o.Name == FName && o.Surname == SName);

            if (Doc == null)
            {
                return NotFound();
            }



            //List<MedicalVisit> medicalVisits = MedVisits.ToList();
            return Doc.PId;
        }

        private bool PatientExists(long id)
        {
            return (_context.Patients?.Any(e => e.PId == id)).GetValueOrDefault();
        }
    }
}
