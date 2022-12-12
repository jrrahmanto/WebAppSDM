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
    public class MTunjanganPersonsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public MTunjanganPersonsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: MTunjanganPersons
        public async Task<IActionResult> Index()
        {
            TempData["activeTunjangan"] = "active";

            return View(await _context.ViewMTunjanganPerson.ToListAsync());
        }

        // GET: MTunjanganPersons/Create
        public IActionResult Create()
        {
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.OrderBy(x => x.nama).ToList();
            List<MListTunjangan> TunjanganList = _context.MListTunjangan.Where(x => x.keterangan == "person" && x.isdelete == 0).ToList();
            ViewData["TunjanganList"] = TunjanganList;
            ViewData["KaryawanList"] = KaryawanList;

            return View();
        }

        // POST: MTunjanganPersons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nip,nama_tunjangan,nominal,isdelete,createddate")] MTunjanganPerson mTunjanganPerson)
        {
            TempData["activeTunjangan"] = "active";
            mTunjanganPerson.isdelete = 0;
            mTunjanganPerson.createddate = DateTime.Now;
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.OrderBy(x => x.nama).ToList();
            List<MListTunjangan> TunjanganList = _context.MListTunjangan.Where(x => x.keterangan == "person" && x.isdelete == 0).ToList();
            ViewData["KaryawanList"] = KaryawanList;
            ViewData["TunjanganList"] = TunjanganList;

            if (ModelState.IsValid)
            {
                _context.Add(mTunjanganPerson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mTunjanganPerson);
        }

        // GET: MTunjanganPersons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            TempData["activeTunjangan"] = "active";
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.OrderBy(x => x.nama).ToList();
            List<MListTunjangan> TunjanganList = _context.MListTunjangan.Where(x => x.keterangan == "person" && x.isdelete == 0).ToList();
            ViewData["KaryawanList"] = KaryawanList;
            ViewData["TunjanganList"] = TunjanganList;

            if (id == null || _context.MTunjanganPerson == null)
            {
                return NotFound();
            }

            var mTunjanganPerson = await _context.MTunjanganPerson.FindAsync(id);
            if (mTunjanganPerson == null)
            {
                return NotFound();
            }
            return View(mTunjanganPerson);
        }

        // POST: MTunjanganPersons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nip,nama_tunjangan,nominal,isdelete,createddate")] MTunjanganPerson mTunjanganPerson)
        {
            TempData["activeTunjangan"] = "active";
            mTunjanganPerson.isdelete = 0;
            mTunjanganPerson.createddate = DateTime.Now;
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.OrderBy(x => x.nama).ToList();
            ViewData["KaryawanList"] = KaryawanList;
            if (id != mTunjanganPerson.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mTunjanganPerson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MTunjanganPersonExists(mTunjanganPerson.id))
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
            return View(mTunjanganPerson);
        }

        // GET: MTunjanganPersons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            TempData["activeTunjangan"] = "active";
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.OrderBy(x => x.nama).ToList();
            ViewData["KaryawanList"] = KaryawanList;
            if (id == null || _context.MTunjanganPerson == null)
            {
                return NotFound();
            }

            var mTunjanganPerson = await _context.MTunjanganPerson
                .FirstOrDefaultAsync(m => m.id == id);
            if (mTunjanganPerson == null)
            {
                return NotFound();
            }

            return View(mTunjanganPerson);
        }

        // POST: MTunjanganPersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.OrderBy(x => x.nama).ToList();
            ViewData["KaryawanList"] = KaryawanList;
            TempData["activeTunjangan"] = "active";
            if (_context.MTunjanganPerson == null)
            {
                return Problem("Entity set 'ApplicationDBContext.MTunjanganPerson'  is null.");
            }
            var mTunjanganPerson = await _context.MTunjanganPerson.FindAsync(id);
            if (mTunjanganPerson != null)
            {
                _context.MTunjanganPerson.Remove(mTunjanganPerson);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MTunjanganPersonExists(int id)
        {
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.OrderBy(x => x.nama).ToList();
            ViewData["KaryawanList"] = KaryawanList;
            return _context.MTunjanganPerson.Any(e => e.id == id);
        }
    }
}
