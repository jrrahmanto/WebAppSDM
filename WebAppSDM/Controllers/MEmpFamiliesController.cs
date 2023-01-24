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
    public class MEmpFamiliesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public MEmpFamiliesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: MEmpFamilies
        public async Task<IActionResult> Index()
        {
            TempData["activeEmployee"] = "active";
            return View(await _context.ViewEmpFamily.ToListAsync());
        }

        // GET: MEmpFamilies/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            TempData["activeEmployee"] = "active";
            if (id == null || _context.MEmpFamily == null)
            {
                return NotFound();
            }

            var mEmpFamily = await _context.MEmpFamily
                .FirstOrDefaultAsync(m => m.fam_id == id);
            if (mEmpFamily == null)
            {
                return NotFound();
            }

            return View(mEmpFamily);
        }

        // GET: MEmpFamilies/Create
        public IActionResult Create()
        {
            TempData["activeEmployee"] = "active";
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.ToList();
            ViewData["KaryawanList"] = KaryawanList;
            return View();
        }

        // POST: MEmpFamilies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("fam_id,nip,fam_relationship,fam_name,fam_sex,fam_pob,fam_dob,fam_KTP,fam_contact,isdelete,createddate")] MEmpFamily mEmpFamily)
        {
            TempData["activeEmployee"] = "active";
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.ToList();
            ViewData["KaryawanList"] = KaryawanList;
            mEmpFamily.isdelete = 0;
            mEmpFamily.createddate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(mEmpFamily);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mEmpFamily);
        }

        // GET: MEmpFamilies/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            TempData["activeEmployee"] = "active";
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.ToList();
            ViewData["KaryawanList"] = KaryawanList;
            if (id == null || _context.MEmpFamily == null)
            {
                return NotFound();
            }

            var mEmpFamily = await _context.MEmpFamily.FindAsync(id);
            if (mEmpFamily == null)
            {
                return NotFound();
            }
            return View(mEmpFamily);
        }

        // POST: MEmpFamilies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("fam_id,nip,fam_relationship,fam_name,fam_sex,fam_pob,fam_dob,fam_KTP,fam_contact,isdelete,createddate")] MEmpFamily mEmpFamily)
        {
            TempData["activeEmployee"] = "active";
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.ToList();
            ViewData["KaryawanList"] = KaryawanList;
            if (id != mEmpFamily.fam_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mEmpFamily);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MEmpFamilyExists(mEmpFamily.fam_id))
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
            return View(mEmpFamily);
        }

        // GET: MEmpFamilies/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            TempData["activeEmployee"] = "active";
            if (id == null || _context.MEmpFamily == null)
            {
                return NotFound();
            }

            var mEmpFamily = await _context.MEmpFamily
                .FirstOrDefaultAsync(m => m.fam_id == id);
            if (mEmpFamily == null)
            {
                return NotFound();
            }

            return View(mEmpFamily);
        }

        // POST: MEmpFamilies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            TempData["activeEmployee"] = "active";
            if (_context.MEmpFamily == null)
            {
                return Problem("Entity set 'ApplicationDBContext.MEmpFamily'  is null.");
            }
            var mEmpFamily = await _context.MEmpFamily.FindAsync(id);
            if (mEmpFamily != null)
            {
                mEmpFamily.isdelete = 1;
                _context.Update(mEmpFamily);
                await _context.SaveChangesAsync();
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MEmpFamilyExists(long id)
        {
            TempData["activeEmployee"] = "active";
            return _context.MEmpFamily.Any(e => e.fam_id == id);
        }
    }
}
