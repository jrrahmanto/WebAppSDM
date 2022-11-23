using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebAppSDM.Data;
using WebAppSDM.Models;

namespace WebAppSDM.Controllers
{
    public class TGajisController : Controller
    {
        private readonly ApplicationDBContext _context;

        public TGajisController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: TGajis
        public async Task<IActionResult> Index()
        {
            return View(await _context.ViewGaji.ToListAsync());
        }

        // GET: TGajis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TGaji == null)
            {
                return NotFound();
            }

            var tGaji = await _context.TGaji
                .FirstOrDefaultAsync(m => m.id == id);
            if (tGaji == null)
            {
                return NotFound();
            }

            return View(tGaji);
        }

        // GET: TGajis/Create
        public IActionResult Create()
        {
            try
            {
                IEnumerable<MKaryawan> objCatlist = _context.MKaryawan.Where(x => x.isdelete == 0).ToList();
                foreach (var item in objCatlist)
                {
                    List<SqlParameter> parms = new List<SqlParameter>
                    { 
                        // Create parameters    
                        new SqlParameter { ParameterName = "@nip", Value = item.NIP },
                        new SqlParameter { ParameterName = "@year", Value = "2022" },
                        new SqlParameter { ParameterName = "@month", Value = "11" },
                        new SqlParameter { ParameterName = "@periodestart", Value = "2022-10-15" },
                        new SqlParameter { ParameterName = "@periodeend", Value = "2022-11-16" },
                    };
                    _context.Database.ExecuteSqlRaw("EXEC GenerateGaji @nip,@year,@month,@periodestart,@periodeend", parms.ToArray());


                }
            }
            catch (Exception x)
            {

                throw;
            }
            return RedirectToAction("Index", "TGajis");
        }

        // POST: TGajis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,NIP,periode_year,periode_month,gapok,tunjangan_tetap,tunjangan_harian,Gapok,tunjangan_lain,bpjs_ks,bpjs_tk,dplk,pph21,potongan_koperasi,potongan_lain,thp1,thp2,nominal_upah,update_date")] TGaji tGaji)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tGaji);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tGaji);
        }

        // GET: TGajis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TGaji == null)
            {
                return NotFound();
            }

            var tGaji = await _context.TGaji.FindAsync(id);
            if (tGaji == null)
            {
                return NotFound();
            }
            return View(tGaji);
        }

        // POST: TGajis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,NIP,periode_year,periode_month,gapok,tunjangan_tetap,tunjangan_harian,Gapok,tunjangan_lain,bpjs_ks,bpjs_tk,dplk,pph21,potongan_koperasi,potongan_lain,thp1,thp2,nominal_upah,update_date")] TGaji tGaji)
        {
            if (id != tGaji.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tGaji);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TGajiExists(tGaji.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tGaji);
        }

        // GET: TGajis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TGaji == null)
            {
                return NotFound();
            }

            var tGaji = await _context.TGaji
                .FirstOrDefaultAsync(m => m.id == id);
            if (tGaji == null)
            {
                return NotFound();
            }

            return View(tGaji);
        }

        // POST: TGajis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TGaji == null)
            {
                return Problem("Entity set 'ApplicationDBContext.TGaji'  is null.");
            }
            var tGaji = await _context.TGaji.FindAsync(id);
            if (tGaji != null)
            {
                _context.TGaji.Remove(tGaji);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TGajiExists(int id)
        {
            return _context.TGaji.Any(e => e.id == id);
        }
    }
}
