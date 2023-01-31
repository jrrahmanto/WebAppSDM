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
    public class MEmpEducationsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public MEmpEducationsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: MEmpEducations
        public async Task<IActionResult> Index()
        {
            TempData["activeEmployee"] = "active";
            
            string role = HttpContext.Session.GetString("role");
            if (role == "1")
            {
                return View(await _context.ViewEdu.ToListAsync());
            }
            else
            {
                string nip = HttpContext.Session.GetString("nip");
                return View(await _context.ViewEdu.Where(x => x.nip.ToString() == nip).ToListAsync());
            }
        }

        // GET: MEmpEducations/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.MEmpEducations == null)
            {
                return NotFound();
            }

            var mEmpEducations = await _context.MEmpEducations
                .FirstOrDefaultAsync(m => m.edu_id == id);
            if (mEmpEducations == null)
            {
                return NotFound();
            }

            return View(mEmpEducations);
        }

        // GET: MEmpEducations/Create
        public IActionResult Create()
        {
            TempData["activeEmployee"] = "active";
            string role = HttpContext.Session.GetString("role");
            if (role == "1")
            {
                List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.ToList();
                ViewData["KaryawanList"] = KaryawanList;
            }
            else
            {
                string nip = HttpContext.Session.GetString("nip");
                List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.Where(x => x.NIP.ToString() == nip).ToList();
                ViewData["KaryawanList"] = KaryawanList;
            }
            return View();
        }

        // POST: MEmpEducations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("edu_id,nip,edu_year,edu_place,edu_strata,edu_faculty,edu_major,emp_gpa,emp_scholarship,edu_final")] MEmpEducations mEmpEducations)
        {
            TempData["activeEmployee"] = "active";
            string role = HttpContext.Session.GetString("role");
            if (role == "1")
            {
                List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.ToList();
                ViewData["KaryawanList"] = KaryawanList;
            }
            else
            {
                string nip = HttpContext.Session.GetString("nip");
                List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.Where(x => x.NIP.ToString() == nip).ToList();
                ViewData["KaryawanList"] = KaryawanList;
            }
            if (ModelState.IsValid)
            {
                _context.Add(mEmpEducations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mEmpEducations);
        }

        // GET: MEmpEducations/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            TempData["activeEmployee"] = "active";
            string role = HttpContext.Session.GetString("role");
            if (role == "1")
            {
                List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.ToList();
                ViewData["KaryawanList"] = KaryawanList;
            }
            else
            {
                string nip = HttpContext.Session.GetString("nip");
                List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.Where(x => x.NIP.ToString() == nip).ToList();
                ViewData["KaryawanList"] = KaryawanList;
            }
            if (id == null || _context.MEmpEducations == null)
            {
                return NotFound();
            }

            var mEmpEducations = await _context.MEmpEducations.FindAsync(id);
            if (mEmpEducations == null)
            {
                return NotFound();
            }
            return View(mEmpEducations);
        }

        // POST: MEmpEducations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("edu_id,nip,edu_year,edu_place,edu_strata,edu_faculty,edu_major,emp_gpa,emp_scholarship,edu_final")] MEmpEducations mEmpEducations)
        {
            TempData["activeEmployee"] = "active";
            string role = HttpContext.Session.GetString("role");
            if (role == "1")
            {
                List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.ToList();
                ViewData["KaryawanList"] = KaryawanList;
            }
            else
            {
                string nip = HttpContext.Session.GetString("nip");
                List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.Where(x => x.NIP.ToString() == nip).ToList();
                ViewData["KaryawanList"] = KaryawanList;
            }
            if (id != mEmpEducations.edu_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mEmpEducations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MEmpEducationsExists(mEmpEducations.edu_id))
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
            return View(mEmpEducations);
        }

        // GET: MEmpEducations/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.MEmpEducations == null)
            {
                return NotFound();
            }

            var mEmpEducations = await _context.MEmpEducations
                .FirstOrDefaultAsync(m => m.edu_id == id);
            if (mEmpEducations == null)
            {
                return NotFound();
            }

            return View(mEmpEducations);
        }

        // POST: MEmpEducations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.MEmpEducations == null)
            {
                return Problem("Entity set 'ApplicationDBContext.MEmpEducations'  is null.");
            }
            var mEmpEducations = await _context.MEmpEducations.FindAsync(id);
            if (mEmpEducations != null)
            {
                _context.MEmpEducations.Remove(mEmpEducations);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MEmpEducationsExists(long id)
        {
          return _context.MEmpEducations.Any(e => e.edu_id == id);
        }
    }
}
