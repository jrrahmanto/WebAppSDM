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
            TempData["activeTransaksi"] = "active";
            return View(await _context.ViewTMutasi.ToListAsync());
        }

        // GET: TMutasis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            TempData["activeTransaksi"] = "active";
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
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.OrderBy(x => x.nama).ToList();
            List<DropdownList.GradeList> gradelist = _context.GradeList.ToList();
            List<DropdownList.JabatanList> jabatanlist = _context.JabatanList.ToList();
            ViewData["GradeList"] = gradelist;
            ViewData["JabatanList"] = jabatanlist;
            ViewData["KaryawanList"] = KaryawanList;
            TempData["activeTransaksi"] = "active";
            return View();
        }

        // POST: TMutasis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TMutasi tMutasi)
        {
            TempData["activeTransaksi"] = "active";

            if (ModelState.IsValid)
            {
                tMutasi.isdelete = 0;
                tMutasi.createdate = DateTime.Now;
                _context.Add(tMutasi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.OrderBy(x => x.nama).ToList();
            List<DropdownList.GradeList> gradelist = _context.GradeList.ToList();
            List<DropdownList.JabatanList> jabatanlist = _context.JabatanList.ToList();
            ViewData["GradeList"] = gradelist;
            ViewData["JabatanList"] = jabatanlist;
            ViewData["KaryawanList"] = KaryawanList;
            TempData["activeTransaksi"] = "active";
            return View(tMutasi);
        }

        // GET: TMutasis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            TempData["activeTransaksi"] = "active";
            if (id == null || _context.TMutasi == null)
            {
                return NotFound();
            }

            var tMutasi = await _context.TMutasi.FindAsync(id);
            if (tMutasi == null)
            {
                return NotFound();
            }
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.OrderBy(x => x.nama).ToList();
            List<DropdownList.GradeList> gradelist = _context.GradeList.ToList();
            List<DropdownList.JabatanList> jabatanlist = _context.JabatanList.ToList();
            ViewData["GradeList"] = gradelist;
            ViewData["JabatanList"] = jabatanlist;
            ViewData["KaryawanList"] = KaryawanList;
            TempData["activeTransaksi"] = "active";
            return View(tMutasi);
        }

        // POST: TMutasis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TMutasi tMutasi)
        {
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.OrderBy(x => x.nama).ToList();
            List<DropdownList.GradeList> gradelist = _context.GradeList.ToList();
            List<DropdownList.JabatanList> jabatanlist = _context.JabatanList.ToList();
            ViewData["GradeList"] = gradelist;
            ViewData["JabatanList"] = jabatanlist;
            ViewData["KaryawanList"] = KaryawanList;
            TempData["activeTransaksi"] = "active";

            if (ModelState.IsValid)
            {
                try
                {
                    _context.TMutasi.Update(tMutasi);
                    _context.SaveChanges();
                    TempData["ResultOk"] = "Data Updated Successfully !";
                    return RedirectToAction("Index");
                }
                catch (Exception x)
                {
                    TempData["ResultOk"] = x.Message;
                    return RedirectToAction("Index");
                }
     
            }
            
            return View(tMutasi);
        }

        // GET: TMutasis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            TempData["activeTransaksi"] = "active";
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
            TempData["activeTransaksi"] = "active";
            if (_context.TMutasi == null)
            {
                return Problem("Entity set 'ApplicationDBContext.TMutasi'  is null.");
            }
            //var tMutasi = await _context.TMutasi.FindAsync(id);
            var tMutasi = _context.TMutasi.FirstOrDefault(u => u.id == id);
            if (tMutasi != null)
            {
                
                tMutasi.isdelete = 1;
                _context.SaveChanges();

                TempData["ResultOk"] = "Data Deleted Successfully !";
                return RedirectToAction("Index");
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TMutasiExists(int id)
        {
            TempData["activeTransaksi"] = "active";
            return _context.TMutasi.Any(e => e.id == id);
        }
    }
}
