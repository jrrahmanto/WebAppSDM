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
    [Models.Authorize]
    public class TAbsenKhususController : Controller
    {
        private readonly ApplicationDBContext _context;

        public TAbsenKhususController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: TAbsenKhusus
        public async Task<IActionResult> Index()
        {
            TempData["activeAbsensi"] = "active";
            return View(await _context.TAbsenKhusus.ToListAsync());
        }

        // GET: TAbsenKhusus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            TempData["activeAbsensi"] = "active";
            if (id == null || _context.TAbsenKhusus == null)
            {
                return NotFound();
            }

            var tAbsenKhusus = await _context.TAbsenKhusus
                .FirstOrDefaultAsync(m => m.id == id);
            if (tAbsenKhusus == null)
            {
                return NotFound();
            }

            return View(tAbsenKhusus);
        }

        // GET: TAbsenKhusus/Create
        public IActionResult Create()
        {
            TempData["activeAbsensi"] = "active";
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.OrderBy(x => x.nama).ToList();
            ViewData["KaryawanList"] = KaryawanList;
            return View();
        }

        // POST: TAbsenKhusus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nip,periode_start,periode_end,keterangan,isdelete")] TAbsenKhusus tAbsenKhusus)
        {
            tAbsenKhusus.isdelete = 0;
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.OrderBy(x => x.nama).ToList();
            ViewData["KaryawanList"] = KaryawanList;
            TempData["activeAbsensi"] = "active";
            if (ModelState.IsValid)
            {
                _context.Add(tAbsenKhusus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tAbsenKhusus);
        }

        // GET: TAbsenKhusus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.OrderBy(x => x.nama).ToList();
            ViewData["KaryawanList"] = KaryawanList;
            TempData["activeAbsensi"] = "active";
            if (id == null || _context.TAbsenKhusus == null)
            {
                return NotFound();
            }

            var tAbsenKhusus = await _context.TAbsenKhusus.FindAsync(id);
            if (tAbsenKhusus == null)
            {
                return NotFound();
            }
            return View(tAbsenKhusus);
        }

        // POST: TAbsenKhusus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nip,periode_start,periode_end,keterangan,isdelete")] TAbsenKhusus tAbsenKhusus)
        {
            tAbsenKhusus.isdelete = 0;
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.OrderBy(x => x.nama).ToList();
            ViewData["KaryawanList"] = KaryawanList;
            TempData["activeAbsensi"] = "active";
            if (id != tAbsenKhusus.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tAbsenKhusus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TAbsenKhususExists(tAbsenKhusus.id))
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
            return View(tAbsenKhusus);
        }

        // GET: TAbsenKhusus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.OrderBy(x => x.nama).ToList();
            ViewData["KaryawanList"] = KaryawanList;
            TempData["activeAbsensi"] = "active";
            if (id == null || _context.TAbsenKhusus == null)
            {
                return NotFound();
            }

            var tAbsenKhusus = await _context.TAbsenKhusus
                .FirstOrDefaultAsync(m => m.id == id);
            if (tAbsenKhusus == null)
            {
                return NotFound();
            }

            return View(tAbsenKhusus);
        }

        // POST: TAbsenKhusus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.OrderBy(x => x.nama).ToList();
            ViewData["KaryawanList"] = KaryawanList;
            TempData["activeAbsensi"] = "active";
            if (_context.TAbsenKhusus == null)
            {
                return Problem("Entity set 'ApplicationDBContext.TAbsenKhusus'  is null.");
            }
            var tAbsenKhusus = await _context.TAbsenKhusus.FindAsync(id);
            if (tAbsenKhusus != null)
            {
                _context.TAbsenKhusus.Remove(tAbsenKhusus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TAbsenKhususExists(int id)
        {
            TempData["activeAbsensi"] = "active";
            return _context.TAbsenKhusus.Any(e => e.id == id);
        }
    }
}
