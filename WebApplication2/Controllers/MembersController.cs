using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using System.Data;
using System.Configuration;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly memberopContext _context;

        public MembersController(memberopContext context)
        {
            _context = context;
        }

        // GET: api/Members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            return await _context.Members.ToListAsync();
        }
#if false
        // GET: api/Members/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            var member = await _context.Members.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }
#endif

        [HttpGet("{tablename}")]
        public async Task<ActionResult<IEnumerable<Member>>> GetMemberSelect([FromRoute] string tableName, [FromQuery] string para1, [FromQuery] string para2, [FromQuery] string type)
        {
            List<Member> members = null;
            string timeFrom;
            string timeTo;
            DateTime dateFrom;
            DateTime dateTo;

            if ( string.IsNullOrEmpty(para2) )
            {
                if (!string.IsNullOrEmpty(para1) && DateTime.TryParse(para1, out dateFrom))
                {
                    try
                    {
                        //DateTime date = DateTime.Parse(para1);
                        //members = await _context.Members.Where(x => date <= DateTime.Parse( x.Birth )).ToListAsync();

                        timeFrom = dateFrom.ToString();
                        members = await _context.Members.Where(x => timeFrom.CompareTo(x.Birth) <= 0).ToListAsync();

                        //var list = (from m in _context.Members
                        //                  select m).AsQueryable();
                        //members = await list.Where(x => date <= DateTime.Parse(x.Birth)).ToListAsync();

                        //string sql = "SELECT * FROM Members WHERE Birth > '2000/01/01'";
                        //var result = _context.Members.FromSqlRaw(sql);
                        //members = await result.ToListAsync();
                    }
                    catch (Exception ex)
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    members = await _context.Members.ToListAsync();
                }
            }
            else if ( DateTime.TryParse( para2, out dateTo ) )
            {
                if ( string.IsNullOrEmpty(para1) )
                {
                    try
                    {
                        timeTo = dateTo.ToString();
                        members = await _context.Members.Where(x => timeTo.CompareTo(x.Birth) >= 0).ToListAsync();
                    }
                    catch (Exception ex)
                    {
                        return BadRequest();
                    }
                }
                else if ( DateTime.TryParse(para1, out dateFrom) )
                {
                    try
                    {
                        timeFrom = dateFrom.ToString();
                        timeTo = dateTo.ToString();
                        members = await _context.Members.Where(x => timeFrom.CompareTo(x.Birth) <= 0 && timeTo.CompareTo(x.Birth) >= 0).ToListAsync();
                    }
                    catch (Exception ex)
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

            if ( !string.IsNullOrEmpty( type ) && type.CompareTo( "csv" ) == 0 )
            {
                var csvString = CsvWriter.CreateCsv( members );
                var fileName = DateTime.Now.ToString("yyyyMMdd") + "_member.csv";
                var csvData = Encoding.UTF8.GetBytes(csvString);

                // CSVファイルをダウンロード
                return File(csvData, "text/csv", fileName);
            }

            return members;
        }

        // PUT: api/Members/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(int id, Member member)
        {
            if (id != member.Id)
            {
                return BadRequest();
            }

            _context.Entry(member).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
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

        // POST: api/Members
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Member>> PostMember(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMember", new { id = member.Id }, member);
        }

        // DELETE: api/Members/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.Id == id);
        }
    }
}
