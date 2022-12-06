using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppSDM.Data;
using WebAppSDM.Models;

namespace WebAppSDM.Controllers
{
    [Models.Authorize]
    public class MEmployeesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public MEmployeesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: MEmployees
        public async Task<IActionResult> Index()
        {
            TempData["activeEmployee"] = "active";
              return View(await _context.MEmployee.Where(x=>x.isdelete ==0).OrderByDescending(x=>x.emp_aktif).ThenBy(x=>x.nama).ToListAsync());
        }

        // GET: MEmployees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            TempData["activeEmployee"] = "active";
            if (id == null || _context.MEmployee == null)
            {
                return NotFound();
            }

            var mEmployee = await _context.MEmployee.Where(x => x.id == id).ToListAsync();
            if (mEmployee == null)
            {
                return NotFound();
            }

            return View(mEmployee);
        }

        // GET: MEmployees/Create
        public IActionResult Create()
        {
            List<DropdownList.GradeList> gradelist = _context.GradeList.ToList();
            List<DropdownList.JabatanList> jabatanlist = _context.JabatanList.ToList();
            ViewData["GradeList"] = gradelist;
            ViewData["JabatanList"] = jabatanlist;
            TempData["activeEmployee"] = "active";
            return View();
        }

        // POST: MEmployees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nip,nama,emp_pob,emp_dob,emp_sex,emp_address_1,emp_address_2,emp_phone_1,emp_phone_2,emp_cellphone_1,emp_cellphone_2,emp_religion,emp_NPWP,emp_KTP,emp_passport,emp_passportexp,emp_licenseA,emp_licenseAexp,emp_licenseB,emp_licenseBexp,emp_licenseC,emp_licenseCexp,emp_marital,emp_dom,emp_email,emp_aktif,emp_photo,emp_ptkp,emp_norek,emp_bank,emp_an,emp_status,emp_darah,grade_id,jabatan_id,salary,permanent_date,contract_date,date_of_entry,isdelete,fasilitas_mobil")] MEmployee mEmployee)
        {
            try
            {
                List<DropdownList.GradeList> gradelist = _context.GradeList.ToList();
                List<DropdownList.JabatanList> jabatanlist = _context.JabatanList.ToList();
                ViewData["GradeList"] = gradelist;
                ViewData["JabatanList"] = jabatanlist;
                TempData["activeEmployee"] = "active";
                mEmployee.isdelete = 0;
                if (ModelState.IsValid)
                {
                    _context.Add(mEmployee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(mEmployee);
            }
            catch (Exception x)
            {

                throw;
            }
         
        }

        // GET: MEmployees/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            List<DropdownList.GradeList> gradelist = _context.GradeList.ToList();
            List<DropdownList.JabatanList> jabatanlist = _context.JabatanList.ToList();
            ViewData["GradeList"] = gradelist;
            ViewData["JabatanList"] = jabatanlist;
            TempData["activeEmployee"] = "active";
            if (id == null || _context.MEmployee == null)
            {
                return NotFound();
            }

            var mEmployee = await _context.MEmployee.FindAsync(id);
            if (mEmployee == null)
            {
                return NotFound();
            }
            return View(mEmployee);
        }

        // POST: MEmployees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("id,nip,nama,emp_pob,emp_dob,emp_sex,emp_address_1,emp_address_2,emp_phone_1,emp_phone_2,emp_cellphone_1,emp_cellphone_2,emp_religion,emp_NPWP,emp_KTP,emp_passport,emp_passportexp,emp_licenseA,emp_licenseAexp,emp_licenseB,emp_licenseBexp,emp_licenseC,emp_licenseCexp,emp_marital,emp_dom,emp_email,emp_aktif,emp_photo,emp_ptkp,emp_norek,emp_bank,emp_an,emp_status,emp_darah,grade_id,jabatan_id,salary,permanent_date,contract_date,date_of_entry,fasilitas_mobil")] MEmployee mEmployee)
        {
            TempData["activeEmployee"] = "active";
            List<DropdownList.GradeList> gradelist = _context.GradeList.ToList();
            List<DropdownList.JabatanList> jabatanlist = _context.JabatanList.ToList();
            ViewData["GradeList"] = gradelist;
            ViewData["JabatanList"] = jabatanlist;
            mEmployee.isdelete = 0;
            if (id != mEmployee.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MEmployeeExists(Convert.ToInt32(mEmployee.id)))
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
            return View(mEmployee);
        }

        // GET: MEmployees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            TempData["activeEmployee"] = "active";
            if (id == null || _context.MEmployee == null)
            {
                return NotFound();
            }

            var mEmployee = await _context.MEmployee
                .FirstOrDefaultAsync(m => m.id == id);
            if (mEmployee == null)
            {
                return NotFound();
            }

            return View(mEmployee);
        }

        // POST: MEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            TempData["activeEmployee"] = "active";
            if (_context.MEmployee == null)
            {
                return Problem("Entity set 'ApplicationDBContext.MEmployee'  is null.");
            }
            var mEmployee = await _context.MEmployee.FindAsync(id);
            if (mEmployee != null)
            {
                mEmployee.isdelete = 1;
                _context.Update(mEmployee);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool MEmployeeExists(int id)
        {
            TempData["activeEmployee"] = "active";
            return _context.MEmployee.Any(e => e.id == id);
        }
    }
}
