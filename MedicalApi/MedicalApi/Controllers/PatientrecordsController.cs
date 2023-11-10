using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalDatabase.Data;
using MedicalDatabase.Models;
using System.Security.Cryptography;
using Microsoft.CodeAnalysis.Scripting;
using Org.BouncyCastle.Crypto.Generators;
using System.Text;

namespace MedicalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientrecordsController : ControllerBase
    {
        private readonly MedicalDatabaseContext _context;
        private static Random random = new Random();
        private static Dictionary<int, char> indexToChar = new Dictionary<int, char>();

        public PatientrecordsController(MedicalDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Patientrecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patientrecord>>> GetPatientrecords()
        {
            if (_context.Patientrecords == null)
            {
                return NotFound();
            }
            return await _context.Patientrecords.ToListAsync();
            /*
            var ptrs = await _context.Patientrecords.ToListAsync();
            foreach( var ptr in ptrs )
            {
                string temps = Unshuffle(ptr.Name);
                string temps2 = Unshuffle(ptr.Surname);
                ptr.Name=temps;
                ptr.Surname=temps2;
            }
            return ptrs;
            */
        }

        // GET: api/Patientrecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patientrecord>> GetPatientrecord(long id)
        {
            if (_context.Patientrecords == null)
            {
                return NotFound();
            }
            var patientrecord = await _context.Patientrecords.FindAsync(id);

            if (patientrecord == null)
            {
                return NotFound();
            }
            /*
            string temps = Unshuffle(patientrecord.Name);
            string temps2 = Unshuffle(patientrecord.Surname);
            patientrecord.Name=temps;
            patientrecord.Surname=temps2;
            */
            return patientrecord;
        }


        [HttpGet("ID{id}")]
        public async Task<ActionResult<Patientrecord>> GetPatientrecordbyID(string id)
        {
            if (_context.Patientrecords == null)
            {
                return NotFound();
            }


            var patientrecord = await _context.Patientrecords.Where(s => s.Idnumber == id).FirstOrDefaultAsync();

            if (patientrecord == null)
            {
                return NotFound();
            }

            return patientrecord;
        }

        // PUT: api/Patientrecords/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatientrecord(long id, Patientrecord patientrecord)
        {
            if (id != patientrecord.Prid)
            {
                return BadRequest();
            }

            _context.Entry(patientrecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientrecordExists(id))
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

        // POST: api/Patientrecords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Patientrecord>> PostPatientrecord(Patientrecord patientrecord)
        {
            if (_context.Patientrecords == null)
            {
                return Problem("Entity set 'MedicalDatabaseContext.Patientrecords'  is null.");
            }

            var pt = await _context.Patientrecords.Where(p => p.Idnumber==patientrecord.Idnumber).FirstOrDefaultAsync();
            if (pt == null)
            {
                /*
                 string newName = Shuffle(patientrecord.Name);
                 string newSName = Shuffle(patientrecord.Surname);
                 patientrecord.Name = newName;
                 patientrecord.Surname= newSName;

                 */

                _context.Patientrecords.Add(patientrecord);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPatientrecord", new { id = patientrecord.Prid }, patientrecord);
            }
            else
            {
                return CreatedAtAction("GetPatientrecord", new { id = patientrecord.Prid }, patientrecord);
            }


        }

        // DELETE: api/Patientrecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatientrecord(long id)
        {
            if (_context.Patientrecords == null)
            {
                return NotFound();
            }
            var patientrecord = await _context.Patientrecords.FindAsync(id);
            if (patientrecord == null)
            {
                return NotFound();
            }

            _context.Patientrecords.Remove(patientrecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientrecordExists(long id)
        {
            return (_context.Patientrecords?.Any(e => e.Prid == id)).GetValueOrDefault();
        }

        private string HashW(string s)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            // Create a new instance of Rfc2898DeriveBytes and hash the password
            var pbkdf2 = new Rfc2898DeriveBytes(s, salt, 10000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(20); // 20 bytes of key material

            // Combine the salt and hash and convert to Base64
            byte[] saltPlusHash = new byte[36];
            Array.Copy(salt, 0, saltPlusHash, 0, 16);
            Array.Copy(hash, 0, saltPlusHash, 16, 20);

            string hashedName = Convert.ToBase64String(saltPlusHash);
            return hashedName;
        }

        public static string Shuffle(string input)
        {
            char[] characters = input.ToCharArray();
            int length = characters.Length;
            List<int> indices = new List<int>();

            for (int i = 0; i < length; i++)
            {
                indices.Add(i);
            }

            for (int i = length - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                // Swap characters[i] and characters[j]
                char temp = characters[i];
                characters[i] = characters[j];
                characters[j] = temp;

                // Swap indices[i] and indices[j]
                int tempIndex = indices[i];
                indices[i] = indices[j];
                indices[j] = tempIndex;
            }

            // Create a dictionary to map shuffled indices to original characters

            for (int i = 0; i < length; i++)
            {
                indexToChar[indices[i]] = characters[i];
            }

            // Combine shuffled characters and indices into a single string
            StringBuilder scrambled = new StringBuilder();
            foreach (int index in indices)
            {
                scrambled.Append(indexToChar[index]);
            }

            return scrambled.ToString();
        }


        private string Unshuffle(string scrambled)
        {
            char[] characters = scrambled.ToCharArray();
            int length = characters.Length;

            // We'll reverse the Fisher-Yates shuffle to recover the original word
            for (int i = 0; i < length - 1; i++)
            {
                int j = random.Next(i, length);
                // Swap characters[i] and characters[j]
                char temp = characters[i];
                characters[i] = characters[j];
                characters[j] = temp;
            }

            return new string(characters);
        }


    }
}
