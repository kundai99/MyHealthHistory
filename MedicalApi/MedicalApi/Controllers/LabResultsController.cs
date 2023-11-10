using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalDatabase.Data;
using MedicalDatabase.Models;

namespace MedicalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabResultsController : ControllerBase
    {
        private readonly MedicalDatabaseContext _context;

        public LabResultsController(MedicalDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/LabResults
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LabResult>>> GetLabResults()
        {
            if (_context.LabResults == null)
            {
                return NotFound();
            }
            return await _context.LabResults.ToListAsync();
        }

        // GET: api/LabResults/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LabResult>> GetLabResult(long id)
        {
            if (_context.LabResults == null)
            {
                return NotFound();
            }
            var labResult = await _context.LabResults.FindAsync(id);

            if (labResult == null)
            {
                return NotFound();
            }

            return labResult;
        }

        // PUT: api/LabResults/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLabResult(long id, LabResult labResult)
        {
            if (id != labResult.LbId)
            {
                return BadRequest();
            }

            _context.Entry(labResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LabResultExists(id))
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

        // POST: api/LabResults
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LabResult>> PostLabResult(LabResult labResult)
        {
            if (_context.LabResults == null)
            {
                return Problem("Entity set 'MedicalDatabaseContext.LabResults'  is null.");
            }
            _context.LabResults.Add(labResult);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLabResult", new { id = labResult.LbId }, labResult);
        }

        // DELETE: api/LabResults/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLabResult(long id)
        {
            if (_context.LabResults == null)
            {
                return NotFound();
            }
            var labResult = await _context.LabResults.FindAsync(id);
            if (labResult == null)
            {
                return NotFound();
            }

            _context.LabResults.Remove(labResult);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("LabResultsByMID")]
        public async Task<ActionResult<IEnumerable<LabResult>>> GetLabResults(long mid)
        {



            var MedVisits = await _context.LabResults.Where(o => o.MId == mid).ToListAsync();

            if (MedVisits == null)
            {
                return NotFound();
            }

            return MedVisits.ToList();
        }

        private bool LabResultExists(long id)
        {
            return (_context.LabResults?.Any(e => e.LbId == id)).GetValueOrDefault();
        }
    }
}
