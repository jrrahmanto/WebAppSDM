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
    public class TDivisisController : Controller
    {
        private readonly ApplicationDBContext _context;

        public TDivisisController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: TDivisis
        public async Task<IActionResult> Index()
        {
              return View(await _context.ViewDivisi.ToListAsync());
        }

        // GET: TDivisis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TDivisi == null)
            {
                return NotFound();
            }

            var tDivisi = await _context.TDivisi
                .FirstOrDefaultAsync(m => m.id == id);
            if (tDivisi == null)
            {
                return NotFound();
            }

            return View(tDivisi);
        }

        // GET: TDivisis/Create
        public IActionResult Create()
        {
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.ToList();
            ViewData["KaryawanList"] = KaryawanList;

            List<MDivisi> DivisiList = _context.MDivisi.Where(x=>x.is_delete == 0).OrderBy(x=>x.divisi_name).ToList();
            ViewData["DivisiList"] = DivisiList;

            return View();
        }

        // POST: TDivisis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,id_divisi,nip,createdate,isdelete")] TDivisi tDivisi)
        {
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.ToList();
            ViewData["KaryawanList"] = KaryawanList;

            List<MDivisi> DivisiList = _context.MDivisi.Where(x => x.is_delete == 0).OrderBy(x => x.divisi_name).ToList();
            ViewData["DivisiList"] = DivisiList;

            tDivisi.isdelete = 0;
            tDivisi.createdate = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(tDivisi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tDivisi);
        }

        // GET: TDivisis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TDivisi == null)
            {
                return NotFound();
            }

            var tDivisi = await _context.TDivisi.FindAsync(id);
            if (tDivisi == null)
            {
                return NotFound();
            }
            return View(tDivisi);
        }

        // POST: TDivisis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,id_divisi,nip,createdate,isdelete")] TDivisi tDivisi)
        {
            if (id != tDivisi.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tDivisi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TDivisiExists(tDivisi.id))
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
            return View(tDivisi);
        }

        // GET: TDivisis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TDivisi == null)
            {
                return NotFound();
            }

            var tDivisi = await _context.TDivisi
                .FirstOrDefaultAsync(m => m.id == id);
            if (tDivisi == null)
            {
                return NotFound();
            }

            return View(tDivisi);
        }

        // POST: TDivisis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TDivisi == null)
            {
                return Problem("Entity set 'ApplicationDBContext.TDivisi'  is null.");
            }
            var tDivisi = await _context.TDivisi.FindAsync(id);
            if (tDivisi != null)
            {
                _context.TDivisi.Remove(tDivisi);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TDivisiExists(int id)
        {
          return _context.TDivisi.Any(e => e.id == id);
        }
    }
}
