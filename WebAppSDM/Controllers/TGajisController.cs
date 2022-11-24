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
    [Models.Authorize]
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
            return View(await _context.ViewGaji.OrderByDescending(x=>x.periode_year).OrderByDescending(y=>y.periode_month).OrderBy(a=>a.Nama).ToListAsync());
        }

        // GET: TGajis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ViewGaji == null)
            {
                return NotFound();
            }

            var tGaji = await _context.ViewGaji
                .FirstOrDefaultAsync(m => m.id == id);
            if (tGaji == null)
            {
                return NotFound();
            }

            return View(tGaji);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("periodestart,periodeend")] SPGenerateGaji tGaji)
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
                        new SqlParameter { ParameterName = "@year", Value = tGaji.periodeend.Year },
                        new SqlParameter { ParameterName = "@month", Value = tGaji.periodeend.ToString("MM") },
                        new SqlParameter { ParameterName = "@periodestart", Value = tGaji.periodestart },
                        new SqlParameter { ParameterName = "@periodeend", Value = tGaji.periodeend },
                    };
                    _context.Database.ExecuteSqlRaw("EXEC GenerateGaji @nip,@year,@month,@periodestart,@periodeend", parms.ToArray());
                }
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Index", "TGajis");
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
        public async Task<IActionResult> Edit(int id, [Bind("id,NIP,periode_year,periode_month,gapok,tunjangan_tetap,tunjangan_harian,tunjangan_lain,bpjs_ks,bpjs_tk,dplk,pph21,potongan_koperasi,potongan_lain,thp1,thp2,nominal_upah,update_date,potongan")] TGaji tGaji)
        {
            if (id != tGaji.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tGaji.update_date = DateTime.Now;
                    tGaji.nominal_upah = tGaji.thp2 - (tGaji.potongan + tGaji.potongan_koperasi + tGaji.potongan_lain);
                    _context.Update(tGaji);
                    await _context.SaveChangesAsync();
                    TempData["ResultOk"] = "Data Updated Successfully !";
                }
                catch (Exception x)
                {
                    TempData["ResultOk"] = "Data Updated Error !"+x.Message;
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
