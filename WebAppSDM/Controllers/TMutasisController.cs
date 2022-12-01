using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppSDM.Data;
using WebAppSDM.Models;

namespace WebAppSDM.Controllers
{
    public class TMutasisController : Controller
    {
        private readonly ApplicationDBContext _context;

        public TMutasisController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: TMutasis
        public async Task<IActionResult> Index()
        {
            TempData["activelink"] = "active";
            return View(await _context.TMutasi.ToListAsync());
        }

        // GET: TMutasis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            TempData["activelink"] = "active";
            if (id == null || _context.TMutasi == null)
            {
                return NotFound();
            }

            var tMutasi = await _context.TMutasi
                .FirstOrDefaultAsync(m => m.id == id);
            if (tMutasi == null)
            {
                return NotFound();
            }

            return View(tMutasi);
        }

        // GET: TMutasis/Create
        public IActionResult Create()
        {
            TempData["activelink"] = "active";
            return View();
        }

        // POST: TMutasis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,NIP,grade_old,grade_new,jabatan_old,jabatan_new,date_mutasi,no_sk,keterangan,isdelete,createdate")] TMutasi tMutasi)
        {
            TempData["activelink"] = "active";
            if (ModelState.IsValid)
            {
                _context.Add(tMutasi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tMutasi);
        }

        // GET: TMutasis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            TempData["activelink"] = "active";
            if (id == null || _context.TMutasi == null)
            {
                return NotFound();
            }

            var tMutasi = await _context.TMutasi.FindAsync(id);
            if (tMutasi == null)
            {
                return NotFound();
            }
            return View(tMutasi);
        }

        // POST: TMutasis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,NIP,grade_old,grade_new,jabatan_old,jabatan_new,date_mutasi,no_sk,keterangan,isdelete,createdate")] TMutasi tMutasi)
        {
            TempData["activelink"] = "active";
            if (id != tMutasi.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tMutasi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TMutasiExists(tMutasi.id))
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
            return View(tMutasi);
        }

        // GET: TMutasis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            TempData["activelink"] = "active";
            if (id == null || _context.TMutasi == null)
            {
                return NotFound();
            }

            var tMutasi = await _context.TMutasi
                .FirstOrDefaultAsync(m => m.id == id);
            if (tMutasi == null)
            {
                return NotFound();
            }

            return View(tMutasi);
        }

        // POST: TMutasis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            TempData["activelink"] = "active";
            if (_context.TMutasi == null)
            {
                return Problem("Entity set 'ApplicationDBContext.TMutasi'  is null.");
            }
            var tMutasi = await _context.TMutasi.FindAsync(id);
            if (tMutasi != null)
            {
                _context.TMutasi.Remove(tMutasi);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TMutasiExists(int id)
        {
            TempData["activelink"] = "active";
            return _context.TMutasi.Any(e => e.id == id);
        }
    }
}
